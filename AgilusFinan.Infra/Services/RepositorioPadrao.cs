using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioPadrao<T> : IRepositorioPadrao<T> where T : class
    {

        Contexto db = new Contexto();

        public void Incluir(T obj)
        {
            db.Set<T>().Add(obj);
            db.SaveChanges();
        }

        public void Alterar(T obj)
        {
            db.Entry<T>(obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void Excluir(T obj)
        {
            db.Entry<T>(obj).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }

        public void ExcluirPorId(int id)
        {
            T obj = BuscarPorId(id);
            Excluir(obj);
        }

        public IEnumerable<T> Listar()
        {
            return db.Set<T>().ToList();
        }

        public T BuscarPorId(int id)
        {
            return db.Set<T>().Find(id);
        }
    }
}
