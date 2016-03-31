using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;

namespace AgilusFinan.Web.Controllers
{
    public class ResumoTituloCategoriaController : Controller
    {
        // GET: ResumoTituloCategoria
        public PartialViewResult Index(DateTime dataInicial, DateTime dataFinal)
        {
            //Parametros do cache
            var parametros = new Dictionary<string, string>();
            parametros.Add("empresaId", UsuarioLogado.EmpresaId.ToString());
            parametros.Add("dataInicial", dataInicial.ToString());
            parametros.Add("dataFinal", dataFinal.ToString());
            var pagina = (PartialViewResult)Cache.Busca("resumotitulo", parametros);

            if (pagina == null)
            {
                var identadas = from c in Util.CategoriasIdentadas(null, 0)
                                join g in GeradorResumoTituloCategoria.ChamarProcedimentoResumoCategoria(dataInicial, dataFinal) on c.Key equals g.CategoriaId
                                select new { g.CategoriaId, c.Value, g.Cor, g.CategoriaPaiId, g.ValorPrevisto, g.ValorRealizado };

                var lista = new List<ResumoTituloCategoria>();

                foreach (var c in identadas)
                {
                    lista.Add(new ResumoTituloCategoria()
                    {
                        CategoriaId = c.CategoriaId,
                        CategoriaPaiId = c.CategoriaPaiId,
                        Cor = c.Cor,
                        Nome = c.Value,
                        ValorPrevisto = c.ValorPrevisto,
                        ValorRealizado = c.ValorRealizado
                    });
                }

                pagina = PartialView("~/Views/ResumoTituloCategoria/_Index.cshtml", lista);
                Cache.Insere("resumotitulo", parametros, pagina);
            }

            return pagina;
        }
    }
}