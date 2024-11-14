using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowFactory.DBLayer;
using WindowFactory.Model;
using WindowFactory.View.WorkingGrids.Dialogs.Filters;
using WindowFactory.ViewModel.WorkingGrids.Dialogs.Filters;
using WindowFactory.ViewModel.WorkingGrids.Dialogs.Filters.FilterModel;

namespace WindowFactory.ViewModel.WorkingGrids
{
    class WG_Orders : NotifyObject.NotificationObject
    {
        private int _employeeID;

        private WindowFactoryEntities _windowFactoryEntities;
        private int _selectedGoodIndex;

        private OrderFilterWindow goodsFilterWindow;
        private OrdersFilterInit goodsFilterModel;

        private OrderCreateUpdate _dialogWindow;

        public ObservableCollection<OrderModel> Orders { get; private set; }
        public OrderModel Order { get; private set; }

        public ICommand RefreshCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }

        public ICommand AddGoodsCommand { get; private set; }

        public int SelectedGoodIndex
        {
            get => _selectedGoodIndex;
            set
            {
                _selectedGoodIndex = value;
                if (value >= 0 && value < Orders.Count)
                {
                    Order = Orders[value];
                    OnPropertyChanged(nameof(Order));
                }
            }
        }

        public WG_Goods(UnitOfWork unitOfWork, int userID)
        {
            _employeeID = userID;

            _windowFactoryEntities = unitOfWork;

            Orders = new ObservableCollection<OrderModel>();

            RefreshCommand = new DelegateCommand(LoadOrdersAsync);
            FilterCommand = new DelegateCommand(ShowFilter);
            AddGoodsCommand = new DelegateCommand(AddOrders);

            LoadGoodsAsync();
        }

        private async void LoadOrdersAsync()
        {
            Orders.Clear();

            var goods = await Task.Run(() => _windowFactoryEntities.Orders
                .ToList());

            foreach (var good in goods)
            {
                Orders.Add(Mapper.Map(good));
            }

            SelectedGoodIndex = Orders.Count - 1;
        }

        private void ShowFilter()
        {
            if (goodsFilterWindow != null)
            {
                CloseWindow();
            }

            goodsFilterModel = new OrdersFilterInit(ApplyFilter, CloseWindow);

            goodsFilterWindow = new OrderFilterWindow() { DataContext = goodsFilterModel };
            goodsFilterWindow.Closed += (o, e) => CloseWindow();
            goodsFilterWindow.Show();
        }

        private void ApplyFilter(OrderFilter filter)
        {
            //LoadGoodsAsync();
            Orders = new ObservableCollection<OrderModel>(filter.ApplyFilter(Orders.ToList()));
            OnPropertyChanged(nameof(Orders));
            CloseWindow();
        }

        private void CloseWindow()
        {
            goodsFilterWindow.Close();
            goodsFilterWindow = null;
        }

        private void AddOrders()
        {
            var selectedOrder = Orders.Where(e => e.IsSelected).ToList();

            var contractModel = new ContractModel();
            var suppliers = LoadSuppliers();

            contractModel.EmployeeId = _employeeID;
            contractModel.StartDate = DateTime.Now;
            contractModel.EndDate = DateTime.Now.Add(TimeSpan.FromDays(10));
            contractModel.Status = "Активний";
            contractModel.Invoices = new List<InvoiceModel>();
            contractModel.Invoices.Add(new InvoiceModel()
            {
                Orders = selectedOrders,
                ScheduledDate = DateTime.Now.Add(TimeSpan.FromDays(10))
            });

            _dialogWindow ??= new ContractCreateUpdate()
            {
                DataContext = new Dialogs.ContractCreateUpdate(SaveContract, CloseDialog, suppliers, _employeeID, contractModel)
            };
            _dialogWindow.Closed += (o, e) => CloseDialog();
            _dialogWindow.Show();
        }

        private void CloseDialog()
        {
            _dialogWindow.Close();
            _dialogWindow = null;
        }

        private void SaveContract(ContractModel model)
        {
            CDEntityService.AddContractProcedure(_windowFactoryEntities, MapContractFromModel(model));
            CloseDialog();
        }

        private DeliveryContract MapContractFromModel(ContractModel model)
        {
            var contract = Mapper.Map(model);
            List<Invoice> invoices = Mapper.Map(model.Invoices);

            for (int i = 0; i < invoices.Count; i++)
            {
                invoices[i].InvoiceNotations = Mapper.Map(model.Invoices[i].Goods);
            }
            contract.Invoices = invoices;
            return contract;
        }

    }
}
