using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.DTOs.EmployeeDtos;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly APIDbContext _dbContext;

        public EmployeeRepository(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddContract(ContractDto contractDto)
        {
            _dbContext.Contracts.Add(new Contract()
            {
                Name = contractDto.Name,
                EmployeeId = contractDto.EmployeeId,
                StartDate = contractDto.StartDate,
            });
            await _dbContext.SaveChangesAsync();
        }

     

        public async Task AddEmployee(EmployeeDto employeeDto)
        {
            _dbContext.Employees.Add(new Employee()
            {
                FullName = employeeDto.FullName,
                isActive = true
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ContractDto>> GetContractForEmployee(string employeeName)
        {
            var employee = await _dbContext.Employees.
                Include(x => x.Contracts).
                FirstOrDefaultAsync(employee => employee.FullName == employeeName);

            if(employee == null)
            {
                throw new InvalidOperationException("Not found!");
            }


            var contractsDto = employee.Contracts.Select(contract => new ContractDto
            {
                EmployeeId = contract.EmployeeId,
                Name = contract.Name,
                StartDate = contract.StartDate,
            }).ToList();

            return contractsDto;
        }


        public async Task<List<Employee>> GetEmplyees()
        {
            var employees = await _dbContext.Employees
                                                .Where(x => x.isActive)
                                                .ToListAsync();

            return employees;
        }
        public async Task<List<Contract>> GetContracts()
        {
            var contracts = await _dbContext.Contracts.ToListAsync();

            return contracts;
        }

        public async Task UpdateContract(int id,UpdateContract updatedContract)
        {
            var contract = await _dbContext.Contracts.FirstOrDefaultAsync(x => x.Id == id);
            
            if(contract == null)
            {
                throw new InvalidOperationException("Not Found!");
            }

            contract.Name = updatedContract.Name;
            contract.StartDate = updatedContract.StartDate;

            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<ContractDto>> GetContractsWithDate(DateTime contractdate)
        {
            var contracts = await _dbContext.Contracts.Where(x => x.StartDate == contractdate).ToListAsync();

            if(contracts == null || contracts.Count == 0)
            {
                throw new InvalidOperationException("Not found");

            }

            return contracts.Select(contract => new ContractDto
            {
                Name = contract.Name,
                StartDate = contract.StartDate,
                EmployeeId = contract.EmployeeId,

            }).ToList();

        }

        public async Task<List<Contract>> GetContractsForToday()
        {
            var today = DateTime.Today;
            var contracts = await _dbContext.Contracts
                                            .Where(x => x.StartDate.Date == today)
                                            .ToListAsync();

            return contracts;
        }
    }
}
