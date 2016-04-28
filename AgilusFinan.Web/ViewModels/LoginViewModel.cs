using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgilusFinan.Web.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}