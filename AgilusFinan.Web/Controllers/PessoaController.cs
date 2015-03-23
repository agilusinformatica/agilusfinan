using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Controllers
{
    public class PessoaController : ControllerPadrao<Pessoa, RepositorioPessoa>
    {
        protected override void PreInclusao()
        {
            base.PreInclusao();
            ViewBag.ListaBancos = new RepositorioBanco().Listar();
        }

        protected override void PreAlteracao()
        {
            base.PreAlteracao();
            ViewBag.ListaBancos = new RepositorioBanco().Listar();
        }
      
    }
}