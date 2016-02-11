using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioTituloRecorrente : RepositorioPadrao<TituloRecorrente>, IRepositorioTituloRecorrente
    {
        public override void PreAlteracao(TituloRecorrente obj)
        {
            base.PreAlteracao(obj);

            var repositorio = new RepositorioBoletoGerado();
            var boletogerado = repositorio.Listar(b => b.TituloRecorrenteId == obj.Id).FirstOrDefault();
            boletogerado.DataVencimento = new DateTime(boletogerado.DataVencimento.Value.Year, boletogerado.DataVencimento.Value.Month, obj.DiaVencimento);
            repositorio.Alterar(boletogerado);
        }
        
    }
}
