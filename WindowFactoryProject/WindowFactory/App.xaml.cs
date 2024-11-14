using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WindowFactory.DBLayer;
using WindowFactory.Service;

namespace WindowFactory
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        int UserID;
        WindowFactoryEntities WFE;
        Window CurrentWindow;

        public void OnStartup(object sender, StartupEventArgs e)
        {
            var WFE = new WindowFactoryEntities();
            CurrentWindow = new View.Windows.Welcome();
            var DataContext = new ViewModel.Windows.Welcome(new LoginService(WFE), OnLoginSucceed);

            CurrentWindow.DataContext = DataContext;
            
            CurrentWindow.Show();
        }

        private void OnLoginSucceed(string login)
        {
            CurrentWindow.Hide();
            var tmp = CurrentWindow;
            CurrentWindow = new View.Windows.UserWorkPlace();
            CurrentWindow.Show();
            tmp.Close();

            WFE = new WindowFactoryEntities();
            UserID = GetIDByLogin(login);

            var DataContext = new ViewModel.Windows.UserWorkPlace(
                UserID,
                WFE,
                ShowOrders
                //ShowProfile,
                //ShowSuppliers,
                //ShowContracts,
                //ShowEmployees
                );

            CurrentWindow.DataContext = DataContext;

            CurrentWindow.Show();
        }

        private int GetIDByLogin(string login)
        {
            return WFE.Workers.Where(e => e.Person.Surname == login).FirstOrDefault().Id;
        }

        public void OnStartup_Debug(object sender, StartupEventArgs e)
        {
            WFE = new WindowFactoryEntities();

            UserID = 68;

            CurrentWindow = new View.Windows.UserWorkPlace();

            var DataContext = new ViewModel.Windows.UserWorkPlace(
                UserID,
                WFE,
                ShowOrders
                //ShowProfile,
                //ShowSuppliers,
                //ShowContracts,
                //ShowEmployees
                );

            CurrentWindow.DataContext = DataContext;

            CurrentWindow.Show();
        }

        private void ShowOrders()
        {
            if (CurrentWindow is View.Windows.UserWorkPlace window &&
                window.UseWorkPlaceContent.Content?.GetType() != typeof(View.WorkingGrids.WG_Orders))
                window.UseWorkPlaceContent.Content = new View.WorkingGrids.WG_Orders() { DataContext = new ViewModel.WorkingGrids.WG_Orders(WFE, UserID) };
        }
        //private void ShowProfile()
        //{
        //    if (CurrentWindow is View.Windows.UserWorkPlace window &&
        //        window.UseWorkPlaceContent.Content?.GetType() != typeof(View.WorkingGrids.WG_Profile))
        //        window.UseWorkPlaceContent.Content = new View.WorkingGrids.WG_Profile() { DataContext = new ViewModel.WorkingGrids.WG_Profile(WFE, UserID) };
        //}
        //private void ShowSuppliers()
        //{
        //    if (CurrentWindow is View.Windows.UserWorkPlace window &&
        //        window.UseWorkPlaceContent.Content?.GetType() != typeof(View.WorkingGrids.WG_Suppliers))
        //        window.UseWorkPlaceContent.Content = new View.WorkingGrids.WG_Suppliers() { DataContext = new ViewModel.WorkingGrids.WG_Suppliers(WFE) };
        //}
        //private void ShowContracts()
        //{
        //    if (CurrentWindow is View.Windows.UserWorkPlace window &&
        //        window.UseWorkPlaceContent.Content?.GetType() != typeof(View.WorkingGrids.WG_Contracts))
        //        window.UseWorkPlaceContent.Content = new View.WorkingGrids.WG_Contracts() { DataContext = new ViewModel.WorkingGrids.WG_Contracts(WFE, UserID) };
        //}
        //private void ShowEmployees()
        //{
        //    if (CurrentWindow is View.Windows.UserWorkPlace window &&
        //        window.UseWorkPlaceContent.Content?.GetType() != typeof(View.WorkingGrids.WG_Employees))
        //        window.UseWorkPlaceContent.Content = new View.WorkingGrids.WG_Employees() { DataContext = new ViewModel.WorkingGrids.WG_Employees(WFE) };
        //}
    }
}


