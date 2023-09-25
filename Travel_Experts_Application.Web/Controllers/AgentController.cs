using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel_Experts_Application.BLL.Interfaces;
using Travel_Experts_Application.Lib.Models;

namespace Travel_Experts_Application.Web.Controllers
{
    public class AgentController : Controller
    {
        private readonly IAgentRepository _agentRepository;

        public AgentController(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }
        // GET: AgentController
        public async Task<ActionResult> Index()
        {
            IList<Agent> agents = await _agentRepository.GetAllAgent(); 
            return View(agents);
        }

    }
}
