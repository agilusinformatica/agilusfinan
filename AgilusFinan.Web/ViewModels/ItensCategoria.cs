using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilusFinan.Web.ViewModels
{
    public class ItensCategoria
    {

        public int? Id { get; set; }
        public Dictionary<int,String> Lista { get; set; }
    }
}
