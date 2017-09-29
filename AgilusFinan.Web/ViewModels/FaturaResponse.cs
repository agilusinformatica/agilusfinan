using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgilusFinan.Web.ViewModels
{
    public class FaturaResponse
    {
        public string id { get; set; }
        public string due_date { get; set; }
        public int items_total_cents { get; set; }
        public string secure_id { get; set; }
        public string secure_url { get; set; }
    }

}