﻿using System.Web.Mvc;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Domain.Entities;
using System;
using System.Collections.Generic;

namespace AgilusFinan.Web.Bases
{
    public class ControllerPadrao<T, R> : Controller 
        where T : Padrao, new() 
        where R : IRepositorioPadrao<T>, new()
    {
        protected R repo = new R();

        protected virtual IEnumerable<T> Dados()
        {
            return repo.Listar();
        }

        [Permissao]
        public virtual ActionResult Index()
        {
            PreListagem();
            return FolderViewName() == String.Empty ? View(Dados()) : View("~/Views/" + FolderViewName() + "/Index.cshtml", Dados());
        }

        [HttpGet]
        [Permissao]
        public virtual ActionResult Create()
        {
            PreInclusao();
            ViewBag.TipoOperacao = "Incluindo";
            return FolderViewName() == String.Empty ? View(new T()) : View("~/Views/" + FolderViewName() + "/Create.cshtml", new T());

        }

        [HttpPost]
        [Permissao]
        public virtual ActionResult Create(T model)
        {
            if (ModelState.IsValid)
            {
                repo.Incluir(model);
                TempData["Alerta"] = new Alerta() { Mensagem = "Registro gravado com sucesso", Tipo = "success" };

                if (Request.Form["novo"] != null && Request.Form["novo"].Equals("1"))
                {
                    return RedirectToAction("Create");
                }

                return RedirectToAction("Index");
            }
            PreInclusao();
            return FolderViewName() == String.Empty ? View(model) : View("~/Views/" + FolderViewName() + "/Create.cshtml", model);

        }

        [HttpGet]
        [Permissao]
        public virtual ActionResult Edit(int id)
        {
            T model = repo.BuscarPorId(id);
            PreAlteracao(model);
            ViewBag.TipoOperacao = "Alterando";
            return FolderViewName() == String.Empty ? View(model) : View("~/Views/" + FolderViewName() + "/Edit.cshtml", model);

        }

        [HttpPost]
        [Permissao]
        public virtual ActionResult Edit(T model)
        {
            if (ModelState.IsValid)
            {
                repo.Alterar(model);
                TempData["Alerta"] = new Alerta() { Mensagem = "Registro alterado com sucesso", Tipo = "success" };
                return RedirectToAction("Index");
            }
            PreAlteracao(model);
            return FolderViewName() == String.Empty ? View(model) : View("~/Views/" + FolderViewName() + "/Edit.cshtml", model);

        }

        [HttpGet]
        [Permissao]
        public virtual ActionResult Delete(int id)
        {
            PreExclusao();
            T model = repo.BuscarPorId(id);
            return FolderViewName() == String.Empty ? View(model) : View("~/Views/" + FolderViewName() + "/Delete.cshtml", model);

        }

        [HttpPost, ActionName("Delete")]
        [Permissao]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            repo.ExcluirPorId(id);
            TempData["Alerta"] = new Alerta() { Mensagem = "Registro excluído com sucesso", Tipo = "success" };
            return RedirectToAction("Index");
        }

        protected virtual void PreAlteracao(T model)
        {
            
        }

        protected virtual void PreInclusao()
        {
            
        }

        protected virtual void PreExclusao()
        {

        }

        protected virtual void PreListagem()
        {
            
        }

        protected virtual string FolderViewName()
        {
            return string.Empty;
        }
    }
}

