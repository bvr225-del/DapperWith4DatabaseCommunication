using DapperWith4DatabaseCommunication.Models;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartments();
        Task<Department> GetDepartmentById(int deptid);
        Task<int> AddDepartment(Department deptdetail);
        Task<string>DeleteDepartment(int departmentid);
        Task<string>UpdateDepartment(Department deptdetail);
    }
}
