using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;

namespace AgilusFinan.Domain.Utils
{
    public class Email
    {
        private string senha = "ac@78902";
        private string smtp = "smtp.agilus.com.br";
        private string usuario = "rafael@agilus.com.br";
        private string emailDestinatario;
        private string mensagem;
        private string emailRemetente;
        private string assunto;
        private List<String> anexos = new List<String>();
        private List<Stream> anexosStream = new List<Stream>();
        private List<String> nomesArquivos = new List<String>();

        public string Assunto { get; set; }

        public Email(string EmailDestinatario, string Mensagem, string Assunto, string EmailRemetente)
        {
            this.emailDestinatario = EmailDestinatario;
            this.emailRemetente = "financeiro@agilus.com.br"; //EmailRemetente;
            this.mensagem = Mensagem;
            this.assunto = Assunto;
        }

        public Email(string EmailDestinatario, string Mensagem, string Assunto, string EmailRemetente, List<string> Anexos) : this(EmailDestinatario, Mensagem, Assunto, EmailRemetente) 
        {
            this.anexos = Anexos;
        }

        public Email(string EmailDestinatario, string Mensagem, string Assunto, string EmailRemetente, List<Stream> AnexosStream, List<string> NomesArquivos) : this(EmailDestinatario, Mensagem, Assunto, EmailRemetente)
        {
            this.anexosStream = AnexosStream;
            this.nomesArquivos = NomesArquivos;
        }

        public void DispararMensagem()
        {
            
            //Cria objeto com dados do e-mail.
            using (MailMessage objEmail = new MailMessage())
            {
                //Define o Campo From e ReplyTo do e-mail.
                objEmail.From = new System.Net.Mail.MailAddress("AGILUS FINAN " + "<" + emailRemetente + ">");

                //Define os destinatários do e-mail.
                objEmail.To.Add(emailDestinatario);

                //Define a prioridade do e-mail.
                objEmail.Priority = System.Net.Mail.MailPriority.Normal;

                //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
                objEmail.IsBodyHtml = true;

                //Define título do e-mail.
                objEmail.Subject = assunto;

                List<LinkedResource> linkedImagens = new List<LinkedResource>();

                //Encontrar imagens na mensagem
                while (true)
                {
                    int posicaoImagem = mensagem.IndexOf("<img src=\"data");

                    if (posicaoImagem == -1) 
                        break;

                    int ultimaPosicao = mensagem.IndexOf(">", posicaoImagem + 1);
                    string imagem = mensagem.Substring(posicaoImagem, ultimaPosicao - posicaoImagem + 1);
                    string contentId = Guid.NewGuid().ToString();

                    var streamimagem = base64ToStream(imagem);
                    var linkedImagem = geraLinkImagem(streamimagem, contentId);
                    mensagem = mensagem.Replace(imagem, @"<img src='cid:" + linkedImagem.ContentId + @"'/>");
                    linkedImagens.Add(linkedImagem);
                }
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(mensagem, null, MediaTypeNames.Text.Html);

                foreach (var item in linkedImagens)
                {
                    alternateView.LinkedResources.Add(item);
                }

                objEmail.AlternateViews.Add(alternateView);

                //Define o corpo do e-mail.
                //objEmail.Body = mensagem;

                //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
                objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

                //Adiciona a lista de anexos
                foreach (var anexo in anexos)
                {
                    objEmail.Attachments.Add(new Attachment(anexo));
                }

                for (int i = 0; i < anexosStream.Count; i++)
                {
                    objEmail.Attachments.Add(new Attachment(anexosStream[i], nomesArquivos[i]));
                }

                //Cria objeto com os dados do SMTP
                System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient();

                //Alocamos o endereço do host para enviar os e-mails  
                objSmtp.Credentials = new System.Net.NetworkCredential(usuario, senha);
                objSmtp.Host = smtp;
                objSmtp.Port = 587;

                //Caso utilize conta de email do exchange da locaweb deve habilitar o SSL
                //objSmtp.EnableSsl = true;

                //Enviamos o e-mail através do método .send()
                try
                {
                    objSmtp.Send(objEmail);
                }
                catch (Exception ex)
                {
                    //Envio de email relatando erro a pessoa que convidou.
                    throw new Exception("Ocorreram problemas no envio do e-mail. Erro = " + ex.Message);
                }
            }
        }

        private Stream base64ToStream(string base64String)
        {
            // Convert Base64 String to byte[]
            var posicaoInicial = base64String.IndexOf(',');
            var posicaoFinal = base64String.IndexOf('"', posicaoInicial + 1);
            var x = base64String.Substring(posicaoInicial + 1, posicaoFinal - posicaoInicial - 1);
            byte[] imageBytes = Convert.FromBase64String(x);
            return new MemoryStream(imageBytes, 0, imageBytes.Length);
        }

        private LinkedResource geraLinkImagem(Stream image, string contentId) 
        {
            LinkedResource inline = new LinkedResource(image);
            inline.ContentId = contentId;

            return inline;
        }
    }
}

