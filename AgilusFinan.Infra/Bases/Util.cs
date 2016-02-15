using AgilusFinan.Infra.Context;
using AgilusFinan.Infra.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AgilusFinan.Infra.Bases
{
    public static class Util
    {
        public static DateTime AjustarVencimento(DateTime dataVencimento, int categoriaId)
        {
            var direcao = new RepositorioCategoria().BuscarPorId(categoriaId).DirecaoVencimentoDiaNaoUtil;

            using (Contexto context = new Contexto())
            {
                return context.Database.SqlQuery<DateTime>("select dbo.fn_ajusta_vencimento (@data_base, @direcao_vencimento)",
                            new SqlParameter("@data_base", dataVencimento),
                            new SqlParameter("@direcao_vencimento", direcao)).Single();
            }
        }
    }
}
