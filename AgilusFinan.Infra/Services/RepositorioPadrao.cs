using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Infra.Context;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioPadrao<T> : IRepositorioPadrao<T> where T : Padrao
    {

        Contexto db = new Contexto();

        public void Incluir(T obj)
        {
            db.Set<T>().Add(obj);
            obj.EmpresaId = db.EmpresaId; 
            db.SaveChanges();
        }

        public void Alterar(T obj)
        {
            db.Entry<T>(obj).State = EntityState.Modified;
            obj.EmpresaId = db.EmpresaId; 
            db.SaveChanges();
        }

        public void Excluir(T obj)
        {
            db.Entry<T>(obj).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public void ExcluirPorId(int id)
        {
            T obj = BuscarPorId(id);
            Excluir(obj);
        }

        public IEnumerable<T> Listar()
        {
            return db.Set<T>().Where(e => e.EmpresaId == db.EmpresaId);
        }

        public T BuscarPorId(int id)
        {
            return db.Set<T>().Find(id);
        }

        public List<T> Listar(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate).Where(e => e.EmpresaId == db.EmpresaId).ToList();
        }
    }
}
