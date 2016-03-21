using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using AgilusFinan.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Controllers
{
    public class SaldoController : Controller
    {
        // GET: Saldo
        public ActionResult Index(DateTime data, int? contaId)
        {
            var parametros = new Dictionary<string, string>();
            parametros.Add("empresaId", UsuarioLogado.EmpresaId.ToString());
            parametros.Add("data", data.ToString());
            var pagina = (ViewResult)Cache.Busca("saldo", parametros);

            if (pagina == null)
            {
                var saldos = new List<SaldoViewModel>();
                foreach (var conta in new RepositorioConta().Listar())
                {
                    saldos.Add(new SaldoViewModel() { Conta = conta, Saldo = GeradorSaldo.ChamarProcedimentoSaldo(data, conta.Id) });
                }
                pagina = View(saldos);
                Cache.Insere("saldo", parametros, pagina);
            }

            return pagina;
        }
    }
}