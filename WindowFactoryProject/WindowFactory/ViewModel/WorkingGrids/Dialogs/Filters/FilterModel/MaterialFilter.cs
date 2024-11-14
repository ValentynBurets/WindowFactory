using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowFactory.Model;

namespace WindowFactory.ViewModel.WorkingGrids.Dialogs.Filters.FilterModel
{
    class MaterialFilter : FilterBase<MaterialModel>
    {
        public MaterialFilter AddPriceFilter(decimal from, decimal to)
        {
            AddFilter("AddPriceFilter", (model) => model.Price >= from && model.Price < to);
            return this;
        }

        public MaterialFilter AddQuantityFilter(int from, int to)
        {
            AddFilter("AddQuantityFilter", (model) => model.Quantity >= from && model.Quantity < to);
            return this;
        }

        public MaterialFilter AddNameFilter(string name)
        {
            AddFilter("AddNameFilter", (model) => model.Name.Contains(name));
            return this;
        }

        public MaterialFilter AddTypeFilter(string type)
        {
            AddFilter("AddTypeFilter", (model) => model.MaterialType.Contains(type));
            return this;
        }
    }
}