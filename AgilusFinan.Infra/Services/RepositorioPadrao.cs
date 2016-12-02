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

        private void Salvar()
        {
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                string message = String.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        // Capturando uma nova exception, na transação do banco de dados
                        // Alterando a mensagem original da exception, pela mensagem mais descritiva
                        message += string.Format("{0}: {1}\n", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                    }
                }
                throw new InvalidOperationException(message, dbEx);
            }  
        }

        public void Incluir(T obj)
        {
            PreInclusao(obj);
            db.Set<T>().Add(obj);
            obj.EmpresaId = db.EmpresaId;
            Salvar();
        }

        public void Alterar(T obj)
        {
            PreAlteracao(obj);
            db.Entry<T>(obj).State = EntityState.Modified;
            obj.EmpresaId = db.EmpresaId;
            if (BuscarPorId(obj.Id) == null)
            {
                throw new Exception("Objeto não encontrado");
            }
            Salvar();
        }

        public virtual void PreInclusao(T obj)
        {

        }

        public virtual void PreAlteracao(T obj)
        {
            
        }

        public void ExcluirPorId(int id)
        {
            T obj = BuscarPorId(id);
            db.Entry<T>(obj).State = EntityState.Deleted;
            Salvar();
        }

        public virtual IEnumerable<T> Listar()
        {
            return db.Set<T>().Where(e => e.EmpresaId == db.EmpresaId).AsNoTracking();
        }

        public T BuscarPorId(int id)
        {
            return Listar(x => x.Id == id).FirstOrDefault();
        }

        public virtual List<T> Listar(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate).Where(e => e.EmpresaId == db.EmpresaId).AsNoTracking().ToList();
        }
    }
}
