using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace AgilusFinan.Domain.Utils
{
    public class Criptografia
    {
        private static string chave = "ahfosfhoidj@plkwmrewibdidofkdvg&";
        private static string vetorInicializacao = "jutdffkflojdh537";

        private static Rijndael CriarInstanciaRijndael()
        {
            if (!(chave != null && (chave.Length == 16 || chave.Length == 24 || chave.Length == 32)))
            {
                throw new Exception("A chave de criptografia deve possuir 16, 24 ou 32 caracteres.");
            }
            if (vetorInicializacao == null || vetorInicializacao.Length != 16)
            {
                throw new Exception("O vetor de inicialização deve possuir 16 caracteres.");
            }

            Rijndael algoritmo = Rijndael.Create();
            algoritmo.Key = Encoding.ASCII.GetBytes(chave);
            algoritmo.IV = Encoding.ASCII.GetBytes(vetorInicializacao);
            return algoritmo;
        }

        public static string Encriptar(string textoNormal)
        {
            if (String.IsNullOrWhiteSpace(textoNormal))
            {
                throw new Exception("O conteúdo a ser encriptado não pode ser uma string vazia.");
            }
            using (Rijndael algoritmo = CriarInstanciaRijndael())
            {
                ICryptoTransform encryptor = algoritmo.CreateEncryptor(algoritmo.Key, algoritmo.IV);
                using (MemoryStream streamResultado = new MemoryStream())
                {
                    using (CryptoStream csStream = new CryptoStream(streamResultado, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(csStream))
                        {
                            writer.Write(textoNormal);
                        }
                    }
                    return ArrayBytesToHexString(streamResultado.ToArray());
                }
            }
        }

        private static string ArrayBytesToHexString(byte[] conteudo)
        {
            string[] arrayHex = Array.ConvertAll(conteudo, b => b.ToString("X2")); //Converte cada byte em uma string hex
            return string.Concat(arrayHex);
        }

        public static string Decriptar(string textoEncriptado)
        {
            if (String.IsNullOrWhiteSpace(textoEncriptado))
            {
                throw new Exception("O conteúdo a ser decriptado não pode ser uma string vazia.");
            }
            if (textoEncriptado.Length % 2 != 0)
            {
                throw new Exception("O conteúdo a ser decriptado é inválido.");
            }
            using (Rijndael algoritmo = CriarInstanciaRijndael())
            {
                ICryptoTransform decryptor = algoritmo.CreateDecryptor(algoritmo.Key, algoritmo.IV);
                string textoDecriptografado = null;
                using (MemoryStream streamTextoEncriptado = new MemoryStream(HexStringToArrayBytes(textoEncriptado)))
                {
                    using (CryptoStream csStream = new CryptoStream(streamTextoEncriptado, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(csStream))
                        {
                            textoDecriptografado = reader.ReadToEnd();
                        }
                    }
                }
                return textoDecriptografado;
            }
        }

        private static byte[] HexStringToArrayBytes(string conteudo)
        {
            int qtdeBytesEncriptados = conteudo.Length / 2;
            byte[] arrayConteudoEncriptado = new byte[qtdeBytesEncriptados];
            for (int i = 0; i < qtdeBytesEncriptados; i++)
            {
                arrayConteudoEncriptado[i] = Convert.ToByte(conteudo.Substring(i * 2, 2), 16);
            }
            return arrayConteudoEncriptado;
        }
    }
}
