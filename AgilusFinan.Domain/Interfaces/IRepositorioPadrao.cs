using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Interfaces
{
    public interface IRepositorioPadrao<T> where T : class
    {
        void Incluir(T obj);
        void Alterar(T obj);
        void Excluir(T obj);
        void ExcluirPorId(int id);
        IEnumerable<T> Listar();
        T BuscarPorId(int id);
    }
}
