using AgilusFinan.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Services
{
    public static class ConsultaCache
    {
        public static bool Existe(string nome, Dictionary<string, string> parametros)
        {
            string comando = @"select count(*) from cache c
                where nome = '"+nome+"'";

            foreach (var item in parametros)
	        {
		        comando += @" and exists( select 1
                              from parametrocache
			                  where CacheId = c.Id
			                  and nome = '"+item.Key+@"'
			                  and valor = '"+item.Value+"')";
	        }
               
            var db = new Contexto();

            return db.Database.SqlQuery<int>(comando).Single() > 0;
        }

        public static void Gravar(string nome, Dictionary<string, string> parametros)
        {
            string comando = @"declare @id int
                              insert into Cache(nome) values('"+nome+@"')
                              set @id = @@IDENTITY";

            foreach (var item in parametros)
            {
                comando += @" insert into ParametroCache(CacheId, nome, valor)
                           values(@id,'"+ item.Key +"' ,'"+item.Value+"')";                             
            }

            var db = new Contexto();
            db.Database.ExecuteSqlCommand(comando);
        }
    }
}
