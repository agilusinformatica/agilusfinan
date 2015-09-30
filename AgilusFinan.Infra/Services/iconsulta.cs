using AgilusFinan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public interface IConsulta<C> where C : ConsultaPadrao
    {
        List<C> ChamarProcedimento(Filtro filtro);
        Filtro DefineFiltro();
        
    }
}
