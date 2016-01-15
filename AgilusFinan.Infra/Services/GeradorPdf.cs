using System;
using System.Collections.Generic;
using System.Linq;
using NReco.PdfGenerator;

namespace AgilusFinan.Infra.Services
{
    public static class GeradorPdf
    {
        public static void HtmlParaPdf(string html, string nomeArquivo)
        {
            var htmltopdf = new NReco.PdfGenerator.HtmlToPdfConverter();
           
            //Configurando pasta aonde será executado o programa que converte em pdf.
            string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            path = path.Remove(path.LastIndexOf("\\"));
            path = path.Remove(path.LastIndexOf("\\") + 1);
            htmltopdf.PdfToolPath = path += "AgilusFinan.Infra\\Programs"; //Alterando a propriedade do NReco, de onde ele extrai o programa. (na pasta bin não pode, senão ele restarta o asp.net)

            htmltopdf.GeneratePdf(html, null, nomeArquivo);
        }
    }
}
