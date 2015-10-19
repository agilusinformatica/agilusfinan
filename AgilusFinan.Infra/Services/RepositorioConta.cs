using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioConta : RepositorioPadrao<Conta>, IRepositorioConta
    {

        public override void PreInclusao(Conta obj)
        {
            base.PreInclusao(obj);
            if (obj.Padrao)
            {
                foreach (var item in Listar(x => x.Id != obj.Id))
                {
                    item.Padrao = false;
                }
            }
        }

        public override void PreAlteracao(Conta obj)
        {
            base.PreAlteracao(obj);
            if (obj.Padrao)
            {
                foreach (var item in Listar(x => x.Id != obj.Id))
                {
                    item.Padrao = false;
                }
            }
        }
    }
}
