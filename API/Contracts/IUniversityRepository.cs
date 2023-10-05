using API.Models;

namespace API.Contracts
{   // Interface untuk UniversityRepository atau Contracts
    public interface IUniversityRepository : IGeneralRepository<University>
    {   
        University? GetUniversityByCode(string code);
    }
}
