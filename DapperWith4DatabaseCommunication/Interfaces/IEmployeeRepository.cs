using DapperWith4DatabaseCommunication.Models;

namespace DapperWith4DatabaseCommunication.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int empid);
        Task<int> AddEmployees(Employee empdetail);
        Task<bool> DeleteEmployeeById(int empid);
        Task<bool> UpdateEmployee(Employee empdetail);

    }
}
