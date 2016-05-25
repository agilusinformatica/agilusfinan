using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Areas.Api.Bases;
using AgilusFinan.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Areas.Api.Controllers
{
    public class PagamentoController : ControllerViewModelApiPadrao<Titulo, RepositorioPagamento, TituloViewModel>
    {
        [AllowAnonymous]
        public string ListarPeriodo(string token, string dataInicial, string dataFinal)
        {
            try
            {
                TokenController.ValidarToken(token);
                List<TituloViewModel> lista = new List<TituloViewModel>();

                DateTime dI, dF;
                dI = Convert.ToDateTime(dataInicial);
                dF = Convert.ToDateTime(dataFinal);

                foreach (var item in repo.Listar(p => p.DataVencimento >= dI && p.DataVencimento <= dF))
                {
                    TituloViewModel viewModel = new TituloViewModel();
                    viewModel.FromModel(item);
                    lista.Add(viewModel);
                }

                string json = JsonConvert.SerializeObject(lista, settings);
                return json;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                UsuarioLogado.ExpiraCookie();
            }

        }
        [AllowAnonymous]
        public override string Listar(string token)
        {
            return "Utilize ListarPeriodo";
        }
    }
}