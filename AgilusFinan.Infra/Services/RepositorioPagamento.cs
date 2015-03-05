using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioPagamento : RepositorioPadrao<Pagamento>, IRepositorioPagamento
    {

        public void Baixar(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
