using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Threading.Tasks;
using upLiftUnity_API.Models;
using upLiftUnity_API.Repositories;




namespace upLiftUnity_API.Controllers
{
    [Route("api/rules")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly IRulesRepository _rulesRepository;

        public RulesController(IRulesRepository rulesRepository)
        {
            _rulesRepository = rulesRepository ?? throw new ArgumentNullException(nameof(rulesRepository));
        }

        [HttpGet]
        [Route("GetRules")]

        public async Task<IActionResult> Get()
        {
            var rules = await _rulesRepository.GetRules();
            return Ok(rules);
        }

        [HttpGet]
        [Route("GetRuleById/{id}")]
       
        public async Task<IActionResult> GetRuleById(int id)
        {
            var rule = await _rulesRepository.GetRuleById(id);
            if (rule == null)
            {
                return NotFound();
            }
            return Ok(rule);
        }

        [HttpPost]
        [Route("AddRule")]
 
        public async Task<IActionResult> AddRule(Rules rule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _rulesRepository.InsertRule(rule);
            if (result == null || result.RuleId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add the rule");
            }
            return Ok("Rule added successfully");
        }

        [HttpPut]
        [Route("UpdateRule/{id}")]
        public async Task<IActionResult> UpdateRule(int id, Rules rule)
        {
            if (id != rule.RuleId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedRule = await _rulesRepository.UpdateRule(rule);

            if (updatedRule == null)
            {
                return NotFound();
            }

            return Ok("Rule updated successfully");
        }

        [HttpDelete]
        [Route("DeleteRule/{id}")]
        public async Task<IActionResult> DeleteRule(int id)
        {
            var ruleToDelete = await _rulesRepository.GetRuleById(id);
            if (ruleToDelete == null)
            {
                return NotFound();
            }

            await _rulesRepository.DeleteRule(id);
            return Ok("Rule deleted successfully");
        }
    }
}
