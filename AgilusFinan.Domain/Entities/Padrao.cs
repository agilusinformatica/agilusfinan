using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public abstract class Padrao
    {
        [ScaffoldColumn(false)]
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
