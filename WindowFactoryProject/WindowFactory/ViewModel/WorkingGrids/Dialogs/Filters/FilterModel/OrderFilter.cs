using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowFactory.Model;

namespace WindowFactory.ViewModel.WorkingGrids.Dialogs.Filters.FilterModel
{
    class OrderFilter : FilterBase<OrderModel>
    {
        public OrderFilter AddPriceFilter(decimal from, decimal to)
        {
            AddFilter("AddPriceFilter", (model) => model.TotalPrice >= from && model.TotalPrice < to);
            return this;
        }

        public OrderFilter AddNumberFilter(int from, int to)
        {
            AddFilter("AddQuantityFilter", (model) => model.OrderNumber >= from && model.OrderNumber < to);
            return this;
        }

        public OrderFilter AddDateByCreateFilter(DateTime from, DateTime to)
        {
            AddFilter("AddQuantityFilter", (model) => model.DateCreate >= from && model.DateCreate < to);
            return this;
        }

        public OrderFilter AddDateByCompliteFilter(DateTime from, DateTime to)
        {
            AddFilter("AddQuantityFilter", (model) => model.DateComplite >= from && model.DateComplite < to);
            return this;
        }

        public OrderFilter AddPaidFilter()
        {
            AddFilter("AddNameFilter", (model) => ( model.Paid == model.TotalPrice));
            return this;
        }

    }
}