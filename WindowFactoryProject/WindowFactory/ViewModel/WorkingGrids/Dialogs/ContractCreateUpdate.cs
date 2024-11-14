using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowFactory.Command;
using WindowFactory.Model;

namespace WindowFactory.ViewModel.WorkingGrids.Dialogs
{
    class ContractCreateUpdate : NotifyObject.NotificationObject
    {
        private int _employeeID;

        private Action _onClose;
        private Action<OrderModel> _onSave;

        private int _selectedGood;
        private int _selectedInvoice;

        public OrderModel Good { get; set; }
        public InvoiceModel Invoice { get; set; }
        public OrderModel Contract { get; private set; }

        public List<CustomerModel> Customers { get; private set; }

        public int SelectedSupplier { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand RandomizeCommand { get; private set; }
        public ICommand AddInvoiceCommand { get; private set; }
        public ICommand AddPositionCommand { get; private set; }

        public ObservableCollection<GoodModel> Goods { get; private set; }
        public ObservableCollection<InvoiceModel> Invoices { get; private set; }

        public ContractCreateUpdate(Action<ContractModel> onSave, Action onClose, List<SupplierModel> suppliers, int employeeID, ContractModel contract = null)
        {
            _onSave = onSave;
            _onClose = onClose;

            _employeeID = employeeID;

            Good = new OrderModel();
            Invoice = new InvoiceModel();

            if (contract == null)
            {
                Contract = new ContractModel();
                Invoices = new ObservableCollection<InvoiceModel>();
            }
            else
            {
                Contract = contract;
                Invoices = new ObservableCollection<InvoiceModel>(Contract.Invoices);
                SelectedInvoice = Invoices.Count - 1;
            }

            Suppliers = suppliers;
            Contract.EmployeeId = _employeeID;

            SaveCommand = new DelegateCommand(Save);
            CloseCommand = new DelegateCommand(Close);
            AddInvoiceCommand = new DelegateCommand(AddInvoice);
            AddPositionCommand = new DelegateCommand(AddPosition);
            RandomizeCommand = new DelegateCommand(GenerateRandom);
        }

        public int SelectedGood
        {
            get
            {
                return _selectedGood;
            }

            set
            {
                if (value >= 0 && value < Goods.Count)
                {
                    _selectedGood = value;
                    Good = Goods[value];
                    OnPropertyChanged(nameof(Good));
                }
            }
        }
        public int SelectedInvoice
        {
            get
            {
                return _selectedInvoice;
            }

            set
            {
                if (value >= 0 && value < Invoices.Count)
                {
                    _selectedInvoice = value;
                    Invoice.Goods = Goods;
                    Invoice = Invoices[value];
                    Goods = new ObservableCollection<GoodModel>(Invoices[value].Goods);
                    OnPropertyChanged(nameof(Goods));
                }
            }
        }

        private void AddInvoice()
        {
            Invoice.Id = Invoices.Count;
            Invoice.Goods = new List<GoodModel>();
            Invoices.Add(Invoice);
            Invoice = new InvoiceModel();
            OnPropertyChanged(nameof(Invoice));
        }
        private void AddPosition()
        {
            Goods.Add(Good);
            Good = new GoodModel();
            OnPropertyChanged(nameof(Good));
        }
        private void GenerateRandom()
        {
            var rand = new Random();

            Contract.Status = "Активний";
            Contract.StartDate = RandomDate(2020, 2022);
            Contract.Invoices = InvoiceSeeder.Generate(rand.Next(2, 10));
            Contract.EndDate = Contract.StartDate.AddMonths(rand.Next(6, 25));

            int i = 0;
            foreach (var invoice in Contract.Invoices)
            {
                invoice.Id = i++;
            }

            SelectedSupplier = rand.Next(Suppliers.Count);

            Invoices = new ObservableCollection<InvoiceModel>(Contract.Invoices);

            SelectedInvoice = 0;

            OnPropertyChanged(nameof(Contract));
            OnPropertyChanged(nameof(Invoices));
            OnPropertyChanged(nameof(SelectedSupplier));
        }

        private void Save()
        {
            Contract.SupplierId = Suppliers[SelectedSupplier].ID;

            _onSave?.Invoke(Contract);
        }

        private void Close()
        {
            _onClose?.Invoke();
        }

        private static DateTime RandomDate(int from, int to)
        {
            Random gen = new Random();

            DateTime end = new DateTime(to, 1, 1);
            DateTime start = new DateTime(from, 1, 1);

            return start.AddDays(gen.Next((end - start).Days));
        }
    }
}
