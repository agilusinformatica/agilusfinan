using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public class Filtro
    {
        public List<ParametroFiltro> Parametros { get; set; }
        public Filtro()
        {
            Parametros = new List<ParametroFiltro>();
            
        }

        public dynamic ValorPorNome(string nome)
        {
            var parametro = Parametros.Find(p => p.Nome == nome);
            return parametro.Valor;
        }
    }

    public enum TipoFiltro
    {
        texto, valor, monetario, data, tabela 
    }

    public enum TabelasParametros
    {
        conta, centrocusto, categoria, pessoa, tipopessoa, tipotelefone, usuario, perfil, banco
    }
    public class ParametroFiltro
    {
        public string Nome { get; set; }
        public string Label { get; set; }
        public TipoFiltro Tipo { get; set; }
        public dynamic Valor { get; set; }
    }

}
