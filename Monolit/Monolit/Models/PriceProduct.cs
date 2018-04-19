using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Monolit.Models
{
    public class PriceProduct
    {
        public string Name { get; set; }
        public string NameGroup { get; set; }
        public decimal Price { get; set; }
        public string IE { get; set; }
        public string Currency { get; set; }
        public Int32 PositionName { get; set; }
        public Int32 PositionGroupName { get; set; }
    }
}