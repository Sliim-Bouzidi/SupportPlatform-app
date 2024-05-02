using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Support_Ticket_System.DTOs;
using Support_Ticket_System.Entites;
using Support_Ticket_System.Services.ProcessFlowServices;

namespace Support_Ticket_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessFlowController : ControllerBase
    {
        private readonly IProcessFlowServices _processFlowServices;
        public ProcessFlowController(IProcessFlowServices processFlowServices)
        {
            _processFlowServices    = processFlowServices;
        }
        [HttpGet("FirstLevel")]
        public ActionResult<IEnumerable<ProcessFlow>> GetFirstLevelProcessFlow()
        {
            {
                var FirstLevelProcessFlow = _processFlowServices.FirstLevelProcessFlows();
                return Ok(FirstLevelProcessFlow);
            }

        }
        [HttpGet("OtherLevels")]
        public ActionResult<IEnumerable<ProcessFlow>> Getlistofchildrenofparent(string parentProcessFlowName)
        {
            var listofchildren = _processFlowServices.GetChildrenOfParentProcessFlow(parentProcessFlowName);
            return Ok(listofchildren);
        }
        [Authorize (Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProcessFlow (AddProcessFlowDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var processflow = await _processFlowServices.AddProcessFlow(request.ProcessFlowName, request.ParentProcessFlowId, request.TenantName);
            if (processflow == null)
            {
                return BadRequest(ModelState);
            }
            return Ok("ProcessFlow Has been Added Successfully");
        }
        [Authorize (Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProcessFlow(string processFlowName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var success = await _processFlowServices.RemoveProcessFlow(processFlowName);
            if (!success)
            {
                return BadRequest(ModelState);
            }
            return Ok("ProcessFlow Has been removed");

        }
    }
}
