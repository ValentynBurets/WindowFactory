using System.Linq;
using WindowFactory.DBLayer;

namespace WindowFactory.Service
{
    class LoginService : ILoginService
    {

        private WindowFactoryEntities WFE;

        public LoginService(WindowFactoryEntities WFE)
        {
            this.WFE = WFE;
        }

        public bool Login(string surname, string password)
        {
            return WFE.People.Any((l) => l.Surname == surname && l.PhoneNumber == password);
        }

        public bool LoginIsValid(string login)
        {
            return WFE.People.Any((l) => l.Surname == login);
        }
    }
}
