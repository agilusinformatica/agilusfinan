using AgilusFinan.Domain.Entities;
using AgilusFinan.Domain.Interfaces;
using AgilusFinan.Domain.Utils;
using AgilusFinan.Infra.Services;
using AgilusFinan.Web.Areas.Api.Controllers;
using AgilusFinan.Web.Areas.Bases;
using AgilusFinan.Web.Bases.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AgilusFinan.Web.Areas.Api.Bases
{
    public class ControllerViewModelApiPadrao<T, R, V> : Controller
        where T : Padrao, new() 
        where R : IRepositorioPadrao<T>, new()
        where V : ViewModel<T>, new()
    {

        protected R repo = new R();
        JsonSerializerSettings settings = new JsonSerializerSettings();

        public ControllerViewModelApiPadrao()
        {
            settings.Formatting = Formatting.Indented;
            settings.DateFormatString = "dd/MM/yyyy";
        }

        [AllowAnonymous]
        public string Listar(string token)
        {
            try{
                TokenController.ValidarToken(token);
                List<V> lista = new List<V>();

                foreach (var item in repo.Listar())
                {
                    V viewModel = new V();
                    //ModelToViewModel(item, viewModel);
                    viewModel.FromModel(item);
                    lista.Add(viewModel);
                }

                string json = JsonConvert.SerializeObject(lista, settings);
                return json;
            }
            catch(Exception e){
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

                T model = repo.BuscarPorId(id);
                V viewModel = new V();
                //ModelToViewModel(model, viewModel);
                viewModel.FromModel(model);

                string json = JsonConvert.SerializeObject(viewModel, settings);
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

        [AllowAnonymous]
        [HttpPost]
        public string Incluir(string postedData, string token)
        {
            try
            {
                TokenController.ValidarToken(token);

                var js = new JavaScriptSerializer();
                V viewModel = js.Deserialize<V>(postedData);
                T model = new T();
                //ViewModelToModel(viewModel, model);
                model = viewModel.ToModel();
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

        [AllowAnonymous]
        [HttpPost]
        public string Alterar(string postedData, string token)
        {
            try
            {
                TokenController.ValidarToken(token);

                var js = new JavaScriptSerializer();
                V viewModel = js.Deserialize<V>(postedData);

                T model = new T();
                //ViewModelToModel(viewModel, model);
                model = viewModel.ToModel();
                repo.Alterar(model);
                return "Alteração efetuada com sucesso!";
            }
            catch (Exception e)
            {
                return "A alteração deu erro. Mensagem: "+e.Message;
            }
            finally
            {
                UsuarioLogado.ExpiraCookie();
            }

        }

        [AllowAnonymous]
        [HttpPost]
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