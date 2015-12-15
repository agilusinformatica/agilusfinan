using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace AgilusFinan.Domain.Entities
{
    public static class Parser
    {
        public static List<ConciliacaoExtrato> InterpretarOfx(FileStream arquivoOfx)
        {
            StreamReader sr = new StreamReader(arquivoOfx);
            string linha;
            List<ConciliacaoExtrato> ListaLancamentos = new List<ConciliacaoExtrato>();

            while (!sr.EndOfStream)
            {
                linha = sr.ReadLine();

                if (linha.Contains("<STMTTRN>"))
                {
                    ConciliacaoExtrato conciliacaoExtrato = new ConciliacaoExtrato();

                    while (!linha.Contains("</STMTTRN>"))
                    {
                        var texto = linha.ToString();

                        if (texto.Contains("<DTPOSTED>"))
                        {
                            conciliacaoExtrato.DataLancamento = DateTime.ParseExact(texto.Substring(10, 8), "yyyyMMdd", CultureInfo.InvariantCulture);
                        }

                        else if (texto.Contains("<TRNAMT>"))
                        {
                            conciliacaoExtrato.TipoLancamento = Convert.ToDouble(texto.Trim().Substring(8, texto.Length - 8)) < 0 ? (tipoLancamento)0 : (tipoLancamento)1;
                            conciliacaoExtrato.Valor = Convert.ToDouble(texto.Trim().Substring(8, texto.Length - 8).Replace(",", "").Replace(".", ","));
                        }
                        else if (texto.Contains("<MEMO>"))
                        {
                            conciliacaoExtrato.Descricao = texto.Substring(6, texto.Length - 6);
                        }

                        linha = sr.ReadLine();
                    }

                    if (conciliacaoExtrato != null)
                    {
                        ListaLancamentos.Add(conciliacaoExtrato);
                        conciliacaoExtrato = null;
                    }
                }
            }
            return ListaLancamentos;
        }
    }
}