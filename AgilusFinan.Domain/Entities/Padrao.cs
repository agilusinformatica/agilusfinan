using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace AgilusFinan.Domain.Entities
{
    public abstract class Padrao
    {
        [ScaffoldColumn(false)]
        [JsonIgnore]
        public int EmpresaId { get; set; }
        [JsonIgnore]
        public virtual Empresa Empresa { get; set; }
        public int Id { get; set; }
    }
}
