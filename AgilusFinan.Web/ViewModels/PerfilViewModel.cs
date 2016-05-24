using AgilusFinan.Domain.Entities;
using AgilusFinan.Infra.Context;
using AgilusFinan.Web.Bases.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgilusFinan.Web.ViewModels
{
    public class PerfilViewModel : ViewModel<Perfil>
    {
        public PerfilViewModel()
        {
            this.Acessos = new List<ItemAcesso>();
        }

        public int Id { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public List<ItemAcesso> Acessos { get; set; }

        public void FromModel(Perfil model)
        {
            this.Id = model.Id;
            this.Descricao = model.Descricao;

            var funcoes = new Contexto().Funcoes.ToList();
            foreach (var funcao in funcoes)
            {
                this.Acessos.Add(new ItemAcesso()
                {
                    FuncaoId = funcao.Id,
                    Descricao = funcao.Descricao,
                    FuncaoSuperiorId = funcao.FuncaoSuperiorId,
                    Selecionado = model.Acessos.Exists(f => f.FuncaoId == funcao.Id)
                });
            }
        }

        public Perfil ToModel()
        {
            Perfil perfil = new Perfil();
            perfil.Id = this.Id;
            perfil.Descricao = this.Descricao;

            foreach (var acesso in this.Acessos)
            {
                if (acesso.Selecionado)
                {
                    perfil.Acessos.Add(new Acesso()
                    {
                        FuncaoId = acesso.FuncaoId,
                        PerfilId = this.Id
                    });
                }
            }
            return perfil;
        }
    }

    public class ItemAcesso 
    {
        public int FuncaoId { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public bool Selecionado { get; set; }
        public int? FuncaoSuperiorId { get; set; }
    }

}