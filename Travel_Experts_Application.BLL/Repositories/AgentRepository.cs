using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Experts_Application.BLL.Interfaces;
using Travel_Experts_Application.Lib.Models;

namespace Travel_Experts_Application.BLL.Repositories
{
    public class AgentRepository: IAgentRepository
    {
        private readonly TravelExpertsDbContext _context;

        public AgentRepository(TravelExpertsDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Agent>> GetAllAgent()
        {
            return await _context.Agents.ToListAsync();
        }

    }
}
