using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WindowFactory.Command;
using WindowFactory.DBLayer;
using WindowFactory.NotifyObject;

namespace WindowFactory.ViewModel.Windows
{
    class UserWorkPlace : NotificationObject
    {
        int _userID;
        WindowFactoryEntities _windowFactoryEntities;
        
        public string LabelText { get; set; }

        public Visibility GoodsOpenVisibility { get; set; }
        //public Visibility SuppliersOpenVisibility { get; set; }
        //public Visibility ContractsOpenVisibility { get; set; }
        //public Visibility EmployeesOpenVisibility { get; set; }

        public ICommand GoodsOpenCommand { get; set; }
        //public ICommand ProfileOpenCommand { get; set; }
        //public ICommand SuppliersOpenCommand { get; set; }
        //public ICommand ContractsOpenCommand { get; set; }
        //public ICommand EmployeesOpenCommand { get; set; }

        public UserWorkPlace(
            int userID,
            WindowFactoryEntities windowFactoryEntities,
            Action onGoodsOpen
            //Action onProfileOpen,
            //Action onSuppliersOpen,
            //Action onContractsOpen,
            //Action onEmployeesOpen
            )
        {
            _userID = userID;
            _windowFactoryEntities = windowFactoryEntities;

            GoodsOpenCommand = new DelegateCommand(onGoodsOpen);
            //ProfileOpenCommand = new DelegateCommand(onProfileOpen);
            //SuppliersOpenCommand = new DelegateCommand(onSuppliersOpen);
            //ContractsOpenCommand = new DelegateCommand(onContractsOpen);
            //EmployeesOpenCommand = new DelegateCommand(onEmployeesOpen);

            Update();
        }

        private void Update()
        {
            LabelText = "Вітаємо, " +
                _windowFactoryEntities.vWorkers
                    .Where(e => e.Id == _userID)
                    .FirstOrDefault().Surname + "!";
        }
    }
}
