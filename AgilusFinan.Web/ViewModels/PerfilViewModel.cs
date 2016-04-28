using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgilusFinan.Web.ViewModels
{
    public class PerfilViewModel
    {
        public PerfilViewModel()
        {
            this.Acessos = new List<ItemAcesso>();
        }

        public int Id { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public List<ItemAcesso> Acessos { get; set; }
    }

    public class ItemAcesso 
    {
        public int FuncaoId { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public bool Selecionado { get; set; }
        public int? FuncaoSuperiorId { get; set; }
    }

}