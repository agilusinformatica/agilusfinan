﻿using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public class RepositorioUsuario : RepositorioPadrao<Usuario>, IRepositorioUsuario
    {
        public override void PreAlteracao(Usuario obj)
        {
            base.PreAlteracao(obj);
            obj.Senha = new RepositorioUsuario().BuscarPorId(obj.Id).Senha;
        }
    }
}
