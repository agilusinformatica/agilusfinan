using AgilusFinan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public enum TipoLista 
    {
        Conta, CentroCusto, Categoria, Pessoa, TipoPessoa, TipoTelefone, Usuario, Perfil, Banco
    }
    public class SelectLista
    {
        TipoLista tipoLista;
        int usuarioId;

        public SelectLista(int usuarioId, TipoLista tipoLista)
        {
            this.tipoLista = tipoLista;
            this.usuarioId = usuarioId;
        }

        public dynamic Listar()
        {
            switch (tipoLista)
            {
                case TipoLista.Conta:
                    return new RepositorioConta().Listar();
                case TipoLista.CentroCusto:
                    return new RepositorioCentroCusto().Listar();
                case TipoLista.Categoria:
                    return new RepositorioCategoria().Listar();
                case TipoLista.Pessoa:
                    return new RepositorioPessoa().Listar();
                case TipoLista.TipoPessoa:
                    return new RepositorioTipoPessoa().Listar();
                case TipoLista.TipoTelefone:
                    return new RepositorioTipoTelefone().Listar();
                case TipoLista.Usuario:
                    return new RepositorioUsuario().Listar();
                case TipoLista.Perfil:
                    return new RepositorioPerfil().Listar();
                case TipoLista.Banco:
                    return new RepositorioBanco().Listar();
                default:
                    return null;
            }
        }
    }
}
