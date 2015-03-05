using System.Security.Cryptography.X509Certificates;
using AgilusFinan.Domain.Entities;

namespace AgilusFinan.Domain.Interfaces
{
    public interface IRepositorioPagamento : IRepositorioPadrao<Pagamento>
    {
        void Baixar(int id);

    }
}
