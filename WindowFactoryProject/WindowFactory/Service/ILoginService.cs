using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowFactory.Service
{
    interface ILoginService
    {
        bool LoginIsValid(string login);
        bool Login(string login, string password);
    }
}
