using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Common
{
    public class Sorting
    {
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }

        public Sorting(string orderBy, string sortOrder)
        {
            this.OrderBy = orderBy;
            this.SortOrder = sortOrder;
        }

        public Sorting()
        {

        }
    }
}
