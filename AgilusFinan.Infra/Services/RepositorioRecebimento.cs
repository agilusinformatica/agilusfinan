using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioRecebimento : RepositorioPadrao<Titulo>, IRepositorioTitulo
    {
        List<Liquidacao> liquidacoesInseridas = new List<Liquidacao>();
        public override IEnumerable<Titulo> Listar()
        {
            return db.Set<Titulo>().Where(e => e.EmpresaId == db.EmpresaId && e.Categoria.Direcao == DirecaoCategoria.Recebimento);
        }

        public override List<Titulo> Listar(Expression<Func<Titulo, bool>> predicate)
        {
            return db.Set<Titulo>().Where(predicate).Where(e => e.EmpresaId == db.EmpresaId && e.Categoria.Direcao == DirecaoCategoria.Recebimento).ToList();
        }

        public override void PreAlteracao(Titulo obj)
        {
            base.PreAlteracao(obj);
            var liquidacoes = db.Liquidacoes.Where(l => l.TituloId == obj.Id);
            liquidacoesInseridas = obj.Liquidacoes.Where(o => !liquidacoes.Any(t => o.Id == t.Id)).ToList();

            foreach (var liquidacao in liquidacoes)
            {
                db.Liquidacoes.Remove(liquidacao);
            }

            foreach (var liquidacao in obj.Liquidacoes)
            {
                db.Liquidacoes.Add(liquidacao);
            }
        }

        private void EnviarEmailPosLiquidacao(List<Liquidacao> liquidacoes, Titulo obj)
        {
            var pessoa = new RepositorioPessoa().BuscarPorId(obj.PessoaId);
            if (pessoa.RecebeEmailLiquidacao)
            {
                Parametro parametro = new RepositorioParametro().Listar().FirstOrDefault();

                foreach (var item in liquidacoes)
                {
                    var empresa = new RepositorioEmpresa().BuscarPorId(parametro.EmpresaId);
                    var email = new Email(pessoa.EmailFinanceiro, SubstituiVariaveis(item, parametro.TextoEmailLiquidacao), parametro.AssuntoEmailLiquidacao, empresa.EmailFinanceiro);
                    email.DispararMensagem();
                }

            }
        }

        private string SubstituiVariaveis(Liquidacao liquidacao, string textoEmail)
        {
            textoEmail = textoEmail.Replace(("{{Valor}}"), string.Format("{0:C}", liquidacao.Valor + (liquidacao.JurosMulta ?? 0) - (liquidacao.Desconto ?? 0)));
            textoEmail = textoEmail.Replace(("{{DataPagamento}}"), liquidacao.Data.ToString("d"));
            textoEmail = textoEmail.Replace(("{{DataVencimento}}"), liquidacao.Titulo.DataVencimento.ToString("MM/yyyy"));
            textoEmail = textoEmail.Replace(("{{ValorTitulo}}"), string.Format("{0:C}", liquidacao.Titulo.Valor));

            return textoEmail;
        }

        public override void PosInclusao(Titulo obj)
        {
            base.PosInclusao(obj);
            EnviarEmailPosLiquidacao(obj.Liquidacoes.ToList(), obj);
        }

        public override void PosAlteracao(Titulo obj)
        {
            base.PosAlteracao(obj);
            EnviarEmailPosLiquidacao(liquidacoesInseridas, obj);
        }
    }
}
