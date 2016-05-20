using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Domain.Utils;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Areas.Bases;
using AgilusFinan.Web.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AgilusFinan.Web.Areas.Api.Controllers
{
    public class ControllerApiPadrao<T, R> : Controller
        where T : Padrao, new() 
        where R : IRepositorioPadrao<T>, new()
    {
        protected R repo = new R();
        JsonSerializerSettings settings = new JsonSerializerSettings();

        public ControllerApiPadrao()
        {
            settings.Formatting = Formatting.Indented;
        }
        [AllowAnonymous]
        // GET: Api/ControllerPadrao
        public string Listar(string token)
        {
            try
            {
                TokenController.ValidarToken(token);

                string json = JsonConvert.SerializeObject(repo.Listar(), settings);
                return json;
            }
            catch(Exception e)
            {
                return e.Message;
            }
            finally
            {
                UsuarioLogado.ExpiraCookie();
            }

        }
        [AllowAnonymous]
        public string Consultar(int id, string token)
        {
            try
            {
                TokenController.ValidarToken(token);

                string json = JsonConvert.SerializeObject(repo.BuscarPorId(id), settings);
                return json;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                UsuarioLogado.ExpiraCookie();
            }

        }
        [HttpPost]
        [AllowAnonymous]
        public string Incluir(string postedData, string token)
        {
            try
            {
                TokenController.ValidarToken(token);

                var js = new JavaScriptSerializer();
                T model = js.Deserialize<T>(postedData);
                repo.Incluir(model);
                return "Inclusão efetuada com sucesso!";
            }
            catch (Exception e)
            {
                return "A inclusão deu erro. Mensagem: " + e.Message;
            }
            finally
            {
                UsuarioLogado.ExpiraCookie();
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public string Alterar(string postedData, string token)
        {
            try
            {
                TokenController.ValidarToken(token);

                var js = new JavaScriptSerializer();
                T model = js.Deserialize<T>(postedData);
                repo.Alterar(model);
                return "Alteração efetuada com sucesso!";
            }
            catch (Exception e)
            {
                return "A alteração deu erro. Mensagem: " + e.Message;
            }
            finally
            {
                UsuarioLogado.ExpiraCookie();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public string Excluir(int id, string token)
        {
            try
            {
                TokenController.ValidarToken(token);

                repo.ExcluirPorId(id);
                return "Exclusão efetuada com sucesso!";
            }
            catch (Exception e)
            {
                return "A exclusão deu erro. Mensagem: " + e.Message;
            }
            finally
            {
                UsuarioLogado.ExpiraCookie();
            }

        }





    }
}