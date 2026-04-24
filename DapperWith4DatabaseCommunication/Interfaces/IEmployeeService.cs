using DapperWith4DatabaseCommunication.Dtos;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> GetEmployeeById(int empid);
        Task<int> AddEmployees(EmployeeDto empdetail);
        Task<bool> DeleteEmployeeById(int empid);
        Task<bool> UpdateEmployee(EmployeeDto empdetail);

    }
}
