﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;

namespace AgilusFinan.Web.Controllers
{
    public class ImportacaoController : Controller
    {
        // GET: ConciliacaoExtrato
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConciliacaoExtrato(HttpPostedFileBase file)
        {
            var vinculoLista = new List<ItemVinculoBanco>();
            var extrato = Parser.InterpretarOfx(file.InputStream);
            var dataInicial = extrato.Select(d => d.DataLancamento).Min();
            var dataFinal = extrato.Select(d => d.DataLancamento).Max();
            var titulosPendentes = GeradorTitulosPendentes.ChamarProcedimento(dataInicial, dataFinal);

            foreach (var item in extrato)
            {
                var titulos = titulosPendentes.FindAll(t => t.Valor == item.Valor);
                vinculoLista.Add(new ItemVinculoBanco { ItemExtrato = item, TitulosPendentes = titulos});    

            }

            return View("ConciliacaoExtrato", vinculoLista);
        }

    }

    public class ItemVinculoBanco
    {
        public ConciliacaoExtrato ItemExtrato { get; set; }
        public List<TituloPendente> TitulosPendentes { get; set; }
    }
}