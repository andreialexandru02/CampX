using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampX.Common.ViewModels
{
    public class ListItemModel<TText, TValue>
    {
        public TText Text { get; set; }
        public TValue Value { get; set; }
    }
}
