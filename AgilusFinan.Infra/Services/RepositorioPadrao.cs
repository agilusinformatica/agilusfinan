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

        protected Contexto db = new Contexto();

        public void Incluir(T obj)
        {
            PreInclusao(obj);
            db.Set<T>().Add(obj);
            obj.EmpresaId = db.EmpresaId;

            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                        // Capturando uma nova exception, na transação do banco de dados
                        // Alterando a mensagem original da exception, pela mensagem mais descritiva
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }  
            
        }

        public void Alterar(T obj)
        {
            PreAlteracao(obj);
            db.Entry<T>(obj).State = EntityState.Modified;
            obj.EmpresaId = db.EmpresaId;

            try
            {
                db.SaveChanges();

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // Capturando uma nova exception, na transação do banco de dados
                        // Alterando a mensagem original da exception, pela mensagem mais descritiva
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }  
        }

        public virtual void PreInclusao(T obj)
        {

        }

        public virtual void PreAlteracao(T obj)
        {
            
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

        public virtual IEnumerable<T> Listar()
        {
            return db.Set<T>().Where(e => e.EmpresaId == db.EmpresaId);
        }

        public T BuscarPorId(int id)
        {
            return db.Set<T>().Find(id);
        }

        public virtual List<T> Listar(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate).Where(e => e.EmpresaId == db.EmpresaId).ToList();
        }
    }
}
