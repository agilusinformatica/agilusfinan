using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Domain.Entities;
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
            db.Entry<T>(obj).State = System.Data.Entity.EntityState.Modified;
            obj.EmpresaId = db.EmpresaId; 
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
            return db.Set<T>().Where(e => e.EmpresaId == db.EmpresaId).ToList();
        }

        public T BuscarPorId(int id)
        {
            return db.Set<T>().Find(id);
        }

        public IEnumerable<T> Listar(Func<T, bool> predicate)
        {
            return db.Set<T>().Where(e => e.EmpresaId == db.EmpresaId).Where(predicate).ToList();
        }
    }
}
