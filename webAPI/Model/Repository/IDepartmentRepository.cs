using System.Collections.Generic;
using System.Threading.Tasks;
using webAPI.Model.Entities;

namespace webAPI.Model.Repository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartmentById(int id);
        Task<Department> InsertDepartment(Department objDepartment);
        Task<Department> UpdateDepartment(Department objDepartment);
        bool DeleteDepartment(int id);
    }
}
