using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Web.Areas.Api.Controllers
{
    public class FaturaPaga
    {
        public string id { get; set; }
        public string account_id { get; set; }
        public string status { get; set; }
        public string subscription_id { get; set; }
        public int number_of_installments { get; set; }
        public decimal amount { get; set; }
    }
}
