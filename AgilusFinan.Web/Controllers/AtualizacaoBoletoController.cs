using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Utils;
using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Bases;
using BoletoNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.Controllers
{
    public class AtualizacaoBoletoController : Controller
    {
        // GET: AtualizacaoBoleto
        [AllowAnonymous]
        public string Index(int tokenBoleto)
        {
            Contexto db = new Contexto();
            //decodificar o boleto
            //var idBoletoGerado = Criptografia.Decriptar(tokenBoleto);
            //BoletoGerado boletoGerado = new RepositorioBoletoGerado().BuscarPorId(Convert.ToInt32(idBoletoGerado));
            BoletoGerado boletoGerado = db.BoletosGerado.Find(tokenBoleto);
            boletoGerado.ModeloBoleto = db.ModelosBoleto.Find(boletoGerado.ModeloBoletoId);
            boletoGerado.ModeloBoleto.Conta = db.Contas.Find(boletoGerado.ModeloBoleto.ContaId);
            boletoGerado.TituloRecorrente = db.TitulosRecorrentes.Find(boletoGerado.TituloRecorrenteId);
            boletoGerado.TituloRecorrente.Pessoa = db.Pessoas.Find(boletoGerado.TituloRecorrente.PessoaId);
            boletoGerado.TituloRecorrente.Categoria = db.Categorias.Find(boletoGerado.TituloRecorrente.CategoriaId);
            boletoGerado.TituloRecorrente.CentroCusto = db.CentrosCusto.Find(boletoGerado.TituloRecorrente.CentroCustoId);
            boletoGerado.TituloRecorrente.Titulos = db.Titulos.Where(t => t.TituloRecorrenteId == boletoGerado.TituloRecorrente.Id).ToList();
            BoletoBancario boletoBancario = new BoletoBancario();

            //gerar o boleto 
            if(boletoGerado.TituloId != null){
                boletoBancario = Util.GerarBoletoBancario((int)boletoGerado.TituloId, boletoGerado.ModeloBoletoId);
            }
            else
            {
                boletoBancario = Util.GerarBoletoBancario((int)boletoGerado.TituloRecorrenteId, (decimal)boletoGerado.TituloRecorrente.Valor, DateTime.Today, boletoGerado.ModeloBoletoId);
            }

            //retornar o boleto com a data de vencimento atualizada
            return boletoBancario.MontaHtmlEmbedded();
            
        }
    }
}