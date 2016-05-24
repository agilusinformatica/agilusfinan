using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Areas.Api.Bases;
using AgilusFinan.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Areas.Api.Controllers
{
    public class PessoaController : ControllerViewModelApiPadrao<Pessoa, RepositorioPessoa, PessoaViewModel>
    {
           
    }
}