﻿using API.Contracts;
using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.DTOs.Educations;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/accounts")] //alamat url
    [Authorize(Roles = "admin")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IEmailHandler _emailHandler;
        private readonly ITokenHandler _tokenHandler;
        // Inisialisasi untuk IAccountRepository (Contructor)
        public AccountController(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IUniversityRepository universityRepository, IEmailHandler emailHandler, IAccountRoleRepository accountRoleRepository, IRoleRepository roleRepository, ITokenHandler tokenHandler)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
            _roleRepository = roleRepository;
            _accountRoleRepository = accountRoleRepository;
            _emailHandler = emailHandler;
            _tokenHandler = tokenHandler;
        }
        // Controller Get untuk mendapatkan semua data Account
        [HttpGet] //http method
        public IActionResult GetAll()
        {
            try
            {
                var result = _accountRepository.GetAll(); // dari repository untuk getAll
                if (!result.Any()) // menegecek ada data atau tidak
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                var data = result.Select(x => (AccountDto)x);

                return Ok(new ResponseOkHandler<IEnumerable<AccountDto>>("Success to retrieve data", data));
            }
            catch (NotFoundHandler ex)
            {
                // Return Response 404 Not Found
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = ex.Message
                });
            }
        }

        // Controller Get Berdasarkan Guid /api/Account/{guid}
        [HttpGet("{guid}")] //http method
        public IActionResult GetByGuid(Guid guid)
        {
            try
            {
                var result = _accountRepository.GetByGuid(guid);
                if (result is null)
                {
                    throw new NotFoundHandler("Data Not Found"); // throw ke NotFoundHandler
                }

                return Ok(new ResponseOkHandler<AccountDto>("Success to retrieve data", (AccountDto)result));
            }
            catch (NotFoundHandler ex)
            {
                // Return Response 404 Not Found
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = ex.Message
                });
            }
        }
        // Menginput Data Account
        [HttpPost] // http method
        public IActionResult Create(CreateAccountDto createAccountDto)
        {   // menambah data account
            try
            {
                var hashedPassword = HashingHandler.HashPassword(createAccountDto.Password);
                Account toCreate = createAccountDto;
                toCreate.Password = hashedPassword;

                var result = _accountRepository.Create(toCreate);

                return Ok(new ResponseOkHandler<AccountDto>("Success to create data", (AccountDto)result));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Error = ex.Message
                });
            }
        }
        //endpoint untuk forgot password
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(string email)
        {
            try
            {   // Mendapatkan data employee berdasarkan email
                var employee = _employeeRepository.GetByEmail(email);
                if (employee is null)
                {
                    throw new NotFoundHandler("Email Not Found"); // throw ke NotFoundHandler
                }
                // medapatkan data akun
                var account = _accountRepository.GetByGuid(employee.Guid);
                if (account is null)
                {
                    throw new NotFoundHandler("Account Not Created"); // throw ke NotFoundHandler
                }
                // nomor acak untuk otp
                int randomNumber = new Random().Next(100000, 999999);
                // mengupdate data account
                account.ModifiedDate = DateTime.Now;
                account.Otp = randomNumber;
                account.IsUsed = false;
                account.ExpiredTime = DateTime.Now.AddMinutes(5);
                var result = _accountRepository.Update(account);

                // menfirim email
                _emailHandler.Send("Forgot Password", $"Your Otp is {account.Otp}", email);

                return Ok(new ResponseOkHandler<object>("Your Otp has been send to your email"));
            }
            catch (NotFoundHandler ex)
            {
                // Return Response 404 Not Found
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = ex.Message
                });
            }
            catch (ExceptionHandler ex) // catchh ExceptionHanlder dari repository jika error
            {   // Return reponse dengan 500 internal ServerError
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Error = ex.Message
                });
            }
        }
        // Endpoint untuk register
        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register(CreateRegisterDto createRegisterDto)
        {
            try
            {
                string nik = GenerateHandler.Nik(_employeeRepository.GetLastNik());
                // mendapatkan university berdasarkan code
                var university = _universityRepository.GetUniversityByCode(createRegisterDto.UniversityCode);

                // Cek Jika Kosong buat baru
                if (university is null)
                {
                    var createUniversity = new University
                    {
                        Code = createRegisterDto.UniversityCode,
                        Name = createRegisterDto.UniversityName
                    };
                    university = _universityRepository.Create(createUniversity);
                }
                // Buat Guid
                var guid = Guid.NewGuid();
                // Mendapatkan Role Guid
                var createAccountRole = new List<AccountRole>();
                foreach (var role in createRegisterDto.Roles)
                {
                    var getRole = _roleRepository.GetByName(role);
                    if (getRole != null)
                    {
                        createAccountRole.Add(new AccountRoleDto
                        {
                            AccountGuid = guid,
                            RoleGuid = getRole.Guid,
                        });
                    }
                }
                // inisialisasi data untuk register
                var toCreate = new Employee
                {
                    Guid = guid,
                    FirstName = createRegisterDto.FirstName,
                    LastName = createRegisterDto.LastName,
                    BirthDate = createRegisterDto.BirthDate,
                    Gender = createRegisterDto.Gender,
                    HiringDate = createRegisterDto.HiringDate,
                    Email = createRegisterDto.Email,
                    Nik = nik,
                    PhoneNumber = createRegisterDto.PhoneNumber,
                    Education = new CreateEducationDto()
                    {
                        Major = createRegisterDto.Major,
                        Degree = createRegisterDto.Degree,
                        Gpa = createRegisterDto.Gpa,
                        UniversityGuid = university.Guid,
                    },
                    Account = new Account()
                    {
                        Password = HashingHandler.HashPassword(createRegisterDto.Password),
                        AccountRoles = createAccountRole,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    }
                };
                // register data
                var register = _accountRepository.Register(toCreate);

                return Ok(new ResponseOkHandler<RegisterDto>("Success to Register", (RegisterDto)register));
            }
            catch (ExceptionHandler ex) // catchh ExceptionHanlder dari repository jika error
            {   // Return reponse dengan 500 internal ServerError
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Error = ex.Message
                });
            }
        }
        // Endpoint untuk login
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto loginDto)
        {
            try
            {   // Mendapatkan email dan password by email
                var result = _accountRepository.GetEmployeeAndAccountByEmail(loginDto.Email);
                // cek data kosong atau password tidak sama
                bool checkLogin = ((result is null) || !HashingHandler.verivyPassword(loginDto.Password, result.Account.Password));
                if (checkLogin)
                {   // return 401
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Status = HttpStatusCode.Unauthorized.ToString(),
                        Message = "Account or Password is invalid"
                    });
                }
                // return login success
                var claims = new List<Claim>();
                claims.Add(new Claim("Email", result.Email));
                claims.Add(new Claim("FullName", string.Concat(result.FirstName + " " + result.LastName)));

                //get role name
                var guidRoles = _accountRoleRepository.GetRoleGuidByAccountGuid(result.Guid);
                // looping untuk role name untuk dimasukkan ke payload
                foreach (var roleGuid in guidRoles)
                {
                    var role = _roleRepository.GetByGuid(roleGuid);
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }

                var generateToken = _tokenHandler.GenerateToken(claims);
                return Ok(new ResponseOkHandler<object>("Login Success", new { Token = generateToken }));
            }
            catch (ExceptionHandler ex) // catchh ExceptionHanlder dari repository jika error
            {   // Return reponse dengan 500 internal ServerError
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Error = ex.Message
                });
            }
        }

        // Mengupdate Data Account
        [HttpPut] // http method
        public IActionResult Update(AccountDto accountDto)
        {
            try
            {   // GetByGuid dari database
                var account = _accountRepository.GetByGuid(accountDto.Guid);
                if (account is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                Account toUpdate = accountDto;
                toUpdate.CreatedDate = account.CreatedDate;

                var result = _accountRepository.Update(toUpdate);

                return Ok(new ResponseOkHandler<string>("Data Updated"));

            }
            catch (NotFoundHandler ex)
            {
                // Return Response 404 Not Found
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = ex.Message
                });
            }
            catch (ExceptionHandler ex) // catchh ExceptionHanlder dari repository jika error
            {   // Return reponse dengan 500 internal ServerError
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Error = ex.Message
                });
            }
        }
        // mengubah password
        [HttpPut("change-password")]
        [AllowAnonymous]
        public IActionResult ChangePassword(CreateChangePasswordDto changePasswordDto)
        {
            try
            {   // mendapatkan employee dan account sesuai email
                var employeeAndAccount = _accountRepository.GetEmployeeAndAccountByEmail(changePasswordDto.Email);
                // cek data kosong atau Otp beda
                bool checkLogin = ((employeeAndAccount is null) || (employeeAndAccount.Account.Otp != changePasswordDto.Otp));
                if (checkLogin)
                {
                    throw new NotFoundHandler("Otp Not Found");
                }
                // sudah digunakan atau belum
                if (employeeAndAccount.Account.IsUsed)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status403Forbidden,
                        Status = HttpStatusCode.Forbidden.ToString(),
                        Message = "Otp has been used"
                    });
                } // melebihi 5 menit
                if (DateTime.Now > employeeAndAccount.Account.ExpiredTime)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status403Forbidden,
                        Status = HttpStatusCode.Forbidden.ToString(),
                        Message = "Otp has been expired"
                    });
                }
                // get account
                var account = _accountRepository.GetByGuid(employeeAndAccount.Guid);
                // account is null
                if (account is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }
                // update account
                Account toUpdate = account;
                toUpdate.CreatedDate = account.CreatedDate;
                toUpdate.ModifiedDate = DateTime.Now;
                toUpdate.IsUsed = true;
                var hashedPassword = HashingHandler.HashPassword(changePasswordDto.NewPasword);
                toUpdate.Password = hashedPassword;

                var result = _accountRepository.Update(toUpdate);

                return Ok(new ResponseOkHandler<string>("Updated Password Success"));
            }
            catch (NotFoundHandler ex)
            {
                // Return Response 404 Not Found
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = ex.Message
                });
            }
            catch (ExceptionHandler ex) // catchh ExceptionHanlder dari repository jika error
            {   // Return reponse dengan 500 internal ServerError
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Error = ex.Message
                });
            }
        }

        // Menghapus Data Account
        [HttpDelete] // http method
        public IActionResult Delete(Guid guid)
        {
            try
            {   // GetByGuid dari database
                var account = _accountRepository.GetByGuid(guid);
                if (account is null)
                {
                    throw new NotFoundHandler("Guid Not Found");
                }

                var result = _accountRepository.Delete(account);
                return Ok(new ResponseOkHandler<string>("Data Deleted"));

            }
            catch (NotFoundHandler ex)
            {
                // Return Response 404 Not Found
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = ex.Message
                });
            }
            catch (ExceptionHandler ex) // catchh ExceptionHanlder dari repository jika error
            {   // Return reponse dengan 500 internal ServerError
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Error = ex.Message
                });
            }
        }
    }
}
