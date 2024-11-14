using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowFactory.ViewModel.WorkingGrids.Dialogs.Filters.FilterModel
{
    class FilterBase<Model>
    {
        protected Dictionary<string, Func<Model, bool>> _filters;
        public FilterBase() => _filters = new Dictionary<string, Func<Model, bool>>();

        public bool IsThat(Model model)
        {
            foreach (var filter in _filters)
                if (!filter.Value(model))
                    return false;

            return true;
        }

        public List<Model> ApplyFilter(List<Model> models)
        {
            var result = new List<Model>();

            foreach (var model in models)
                if (IsThat(model))
                    result.Add(model);

            return result;
        }

        protected void AddFilter(string key, Func<Model, bool> filter)
        {
            if (_filters.ContainsKey(key))
            {
                _filters[key] = filter;
            }
            else
            {
                _filters.Add(key, filter);
            }
        }
    }
}
