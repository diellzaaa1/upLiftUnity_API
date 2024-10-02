using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.DTOs.EmployeeDtos;
using upLiftUnity_API.DTOs.PlanetsDto;
using upLiftUnity_API.Repositories.EmployeeRepository;

namespace upLiftUnity_API.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
       private readonly IEmployeeRepository _employeeRepository;

       public EmployeeController(IEmployeeRepository employeeRepository) 
        {
        _employeeRepository = employeeRepository;
        }

        [HttpPost("/addContracts")]
        public async Task<IActionResult> AddContract([FromBody] ContractDto contract)
        {
            await _employeeRepository.AddContract(contract);

            return Ok(contract);
        }

        [HttpPost("/addEmployees")]

        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
        {
            await _employeeRepository.AddEmployee(employeeDto);

            return Ok(employeeDto);
        }

        [HttpGet("{employeeName}/contract")]
        public async Task<IActionResult> GetContractForEmployee(string employeeName)
        {
            return Ok(await _employeeRepository.GetContractForEmployee(employeeName));
        }



        [HttpGet("/getEmployees")]
        public async Task<IActionResult> GetEmplyees()
        {
            return Ok(await _employeeRepository.GetEmplyees());
        }

        [HttpGet("/getContracts")]
        public async Task<IActionResult> GetContracts()
        {
            return Ok(await _employeeRepository.GetContracts());
        }
        [HttpGet("/contractsForToday")]
        public async Task<IActionResult> GetContractsForToday()
        {
            var contracts = await _employeeRepository.GetContractsForToday();
            return Ok(contracts);
        }

        [HttpPut("{id:int}/updateContract")]
        public async Task<IActionResult> UpdateContract(int id,[FromBody] UpdateContract updatedContract)
        {
            await _employeeRepository.UpdateContract(id, updatedContract);

            return Ok(updatedContract);
        }

        [HttpGet("{contractdate:datetime}/contracts")]
        public async Task<IActionResult> GetContractsWithDate(DateTime contractdate)
        {
            return Ok(await _employeeRepository.GetContractsWithDate(contractdate));
        }


    }
}
