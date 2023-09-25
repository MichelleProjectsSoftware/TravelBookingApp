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
    public class PackageRepository : IPackageRepository
    {
        private readonly TravelExpertsDbContext _context;

        public PackageRepository(TravelExpertsDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Package>> GetAllPackage()
        {
            return await _context.Packages.ToListAsync();
        }
    }
}
