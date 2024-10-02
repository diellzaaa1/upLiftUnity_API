using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.DTOs.EmployeeDtos;
using upLiftUnity_API.Models;
using upLiftUnity_API.MongoModels;

namespace upLiftUnity_API.Repositories.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        Task AddEmployee(EmployeeDto employeeDto);

        Task AddContract(ContractDto contractDto);

        Task <List<Employee>> GetEmplyees();

        Task<List<Contract>> GetContracts();

        Task<List<ContractDto>> GetContractsWithDate(DateTime contractdate);

        Task <List<ContractDto>> GetContractForEmployee(string employeeName);

        Task UpdateContract(int id,UpdateContract updatedContract);

        Task<List<Contract>> GetContractsForToday();

    }
}
