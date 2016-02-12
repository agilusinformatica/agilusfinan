using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Infra.Bases;
using System;
using System.Linq;


namespace AgilusFinan.Infra.Services
{
    public class RepositorioTituloRecorrente : RepositorioPadrao<TituloRecorrente>, IRepositorioTituloRecorrente
    {
        public override void PreAlteracao(TituloRecorrente obj)
        {
            base.PreAlteracao(obj);

            var repositorio = new RepositorioBoletoGerado();
            var boletogerado = repositorio.Listar(b => b.TituloRecorrenteId == obj.Id).FirstOrDefault();

            if (boletogerado != null)
            {
                var data = new DateTime(boletogerado.DataVencimento.Value.Year, boletogerado.DataVencimento.Value.Month, obj.DiaVencimento);
                boletogerado.DataVencimento = Util.AjustarVencimento(data, obj.CategoriaId);
                repositorio.Alterar(boletogerado);
            }
        }
    }
}
