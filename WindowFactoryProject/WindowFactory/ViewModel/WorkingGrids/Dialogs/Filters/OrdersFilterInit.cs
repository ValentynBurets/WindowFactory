using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowFactory.Command;
using WindowFactory.ViewModel.WorkingGrids.Dialogs.Filters.FilterModel;

namespace WindowFactory.ViewModel.WorkingGrids.Dialogs.Filters
{
    class OrdersFilterInit : NotifyObject.NotificationObject
    {
        private Action _onClose;
        private Action<OrderFilter> _onSave;

        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }


        public int NumberFrom { get; set; }
        public int NumberTo { get; set; }


        public DateTime DateByCompliteFrom { get; set; }
        public DateTime DateByCompliteTo { get; set; }

    
        public DateTime DateByCreateFrom { get; set; }
        public DateTime DateByCreateTo { get; set; }

        public bool Paid { get; set; }
        
        public OrdersFilterInit(Action<OrderFilter> onSave, Action onClose)
        {
            _onClose = onClose;
            _onSave = onSave;

            SaveCommand = new DelegateCommand(Save);
            CloseCommand = new DelegateCommand(Close);
        }

        public OrdersFilterInit()
        {
        }

        private void Save()
        {
            var filter = new OrderFilter();

            if (Paid) filter.AddPaidFilter();
            
            if (NumberFrom > 0) filter.AddNumberFilter(NumberFrom, NumberTo > NumberFrom ? NumberTo : int.MaxValue);
            if (DateByCompliteFrom != null) filter.AddDateByCompliteFilter(DateByCompliteFrom, DateByCompliteTo > DateByCompliteFrom ? DateByCompliteTo : DateTime.MaxValue);
            if (DateByCreateFrom != null) filter.AddDateByCreateFilter(DateByCreateFrom, DateByCreateTo > DateByCreateFrom ? DateByCreateTo : DateTime.MaxValue);
            if (PriceFrom > 0) filter.AddPriceFilter(PriceFrom, PriceTo > PriceFrom ? PriceTo : decimal.MaxValue);
            
            _onSave.Invoke(filter);
        }

        private void Close()
        {
            _onClose.Invoke();
        }
    }
}
