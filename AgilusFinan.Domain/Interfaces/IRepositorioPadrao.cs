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
        IEnumerable<T> Listar(Func<T, bool> predicate );
        T BuscarPorId(int id);
    }
}
