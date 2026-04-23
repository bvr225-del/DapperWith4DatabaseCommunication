using DapperWith4DatabaseCommunication.Dtos;
using DapperWith4DatabaseCommunication.Models;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetDepartments();
        Task<DepartmentDto> GetDepartmentById(int deptid);
        Task<int> AddDepartment(DepartmentDto deptdetail);
        Task<string> DeleteDepartment(int departmentid);
        Task<string> UpdateDepartment(DepartmentDto deptdetail);

    }
}
