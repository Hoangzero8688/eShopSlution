using eShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopSolution.Application2.System
{
    public interface IUserService
    {
        Task<string> Authencate(Login request);
        Task<bool> Register(Reister request);

     }
}
