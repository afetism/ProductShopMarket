using MyApp.Data.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagmentUserPanel.Helpers
{
    class FilterItem
    {
        public Category? Category { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
