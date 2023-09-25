using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Experts_Application.Lib.Models;

namespace Travel_Experts_Application.BLL.Interfaces
{
    public interface IPackageRepository
    {
        Task<IList<Package>> GetAllPackage();
    }
}
