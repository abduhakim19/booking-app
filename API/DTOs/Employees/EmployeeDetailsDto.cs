namespace API.DTOs.Employees
{
    public class EmployeeDetailsDto
    {
        public Guid Guid { get; set; }
        public string Nik { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get;set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }

    }
}
