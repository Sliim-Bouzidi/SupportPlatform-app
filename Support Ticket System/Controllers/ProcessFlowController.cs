using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
