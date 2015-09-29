using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    //public class Filtro
    //{
    //    public DateTime DataInicial { get; set; }
    //    public DateTime DataFinal { get; set; }
    //    public Conta? Conta { get; set; }
    //    public Categoria? Categoria { get; set; }
    //}

    public class Filtro
    {
        public List<ParametroFiltro> Parametros { get; set; }
        public Filtro()
        {
            Parametros = new List<ParametroFiltro>();
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

    class Main
    {
        public void Inicializa()
        {
            var filtro = new Filtro();
            filtro.Parametros.Add( new ParametroFiltro() {Nome = "@data_inicial", Label = "Data Inicial", Tipo = TipoFiltro.data, Valor = DateTime.Today});
        }
    }
}
