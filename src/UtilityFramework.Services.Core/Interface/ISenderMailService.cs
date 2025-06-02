using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UtilityFramework.Services.Core.Interface
{
    public interface ISenderMailService
    {
        /// <summary>
        /// ENVIAR E-MAIL
        /// </summary>
        /// <param name="nome">NOME DO APP OU REMETENTE </param>
        /// <param name="email">EMAIL PRINCIPAL</param>
        /// <param name="body">HTML DO CORPO</param>
        /// <param name="subject">ASSUNTO DO EMAIL</param>
        /// <param name="titleEvent"></param>
        /// <param name="invite"></param>
        /// <param name="local"></param>
        /// <param name="inicio"></param>
        /// <param name="utc"></param>
        /// <param name="fim"></param>
        /// <param name="recorrente">INFORMA SE O EVENTO DEVE SER RECORRENTE</param>
        /// <param name="pathFile">CAMINHO ABSOLUTO DO ARQUIVO</param>
        /// <param name="ccEmails">LISTA DE EMAILS PARA COPIA</param>
        /// <param name="ccoEmails">LISTA DE E-MAILS PARA COPIA OCULTA</param>
        /// <param name="replyTo">LISTA DE E-MAILS PARA REPLY - TO</param>
        void SendMessageEmail(string nome, string email, string body, string subject, string titleEvent = null, bool invite = false,
            string local = null, DateTime? inicio = null, DateTime? utc = null, DateTime? fim = null, bool recorrente = false, string pathFile = null, List<string> ccEmails = null, List<string> ccoEmails = null, List<string> replyTo = null);
        /// <summary>
        /// ENVIO DE EMAIL
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="titleEvent"></param>
        /// <param name="invite"></param>
        /// <param name="local"></param>
        /// <param name="inicio"></param>
        /// <param name="utc"></param>
        /// <param name="fim"></param>
        /// <param name="recorrente"></param>
        /// <param name="pathFile"></param>
        /// <param name="ccoEmails"></param>
        /// <param name="replyTo"></param>
        void SendMessageEmail(string nome, List<string> email, string body, string subject, string titleEvent = null, bool invite = false,
            string local = null, DateTime? inicio = null, DateTime? utc = null, DateTime? fim = null, bool recorrente = false, string pathFile = null, List<string> ccEmails = null, List<string> ccoEmails = null, List<string> replyTo = null);
        /// <summary>
        /// ENVIO DE EMAIL COM AMAZOM
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="titleEvent"></param>
        /// <param name="invite"></param>
        /// <param name="local"></param>
        /// <param name="inicio"></param>
        /// <param name="utc"></param>
        /// <param name="fim"></param>
        /// <param name="recorrente"></param>
        /// <param name="pathFile"></param>
        /// <param name="ccoEmails"></param>
        /// <param name="replyTo"></param>
        void SendMessageEmailAmazon(string nome, List<string> email, string body, string subject, string titleEvent = null, bool invite = false,
            string local = null, DateTime? inicio = null, DateTime? utc = null, DateTime? fim = null, bool recorrente = false, string pathFile = null, List<string> ccEmails = null, List<string> ccoEmails = null, List<string> replyTo = null);
        /// <summary>
        /// ENVIO DE EMAIL ASYNC
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="titleEvent"></param>
        /// <param name="invite"></param>
        /// <param name="local"></param>
        /// <param name="inicio"></param>
        /// <param name="utc"></param>
        /// <param name="fim"></param>
        /// <param name="recorrente"></param>
        /// <param name="pathFile"></param>
        /// <param name="ccEmails"></param>
        /// <param name="ccoEmails"></param>
        /// <param name="replyTo"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task SendMessageEmailAsync(string nome, string email, string body, string subject, string titleEvent = null, bool invite = false,
            string local = null, DateTime? inicio = null, DateTime? utc = null, DateTime? fim = null, bool recorrente = false, string pathFile = null, List<string> ccEmails = null, List<string> ccoEmails = null, List<string> replyTo = null, bool configureAwait = false);
        /// <summary>
        /// ENVIO DE EMAIL ASYNC COM COPIA
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="titleEvent"></param>
        /// <param name="invite"></param>
        /// <param name="local"></param>
        /// <param name="inicio"></param>
        /// <param name="utc"></param>
        /// <param name="fim"></param>
        /// <param name="recorrente"></param>
        /// <param name="pathFile"></param>
        /// <param name="ccoEmails"></param>
        /// <param name="replyTo"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task SendMessageEmailAsync(string nome, List<string> email, string body, string subject, string titleEvent = null, bool invite = false,
            string local = null, DateTime? inicio = null, DateTime? utc = null, DateTime? fim = null, bool recorrente = false, string pathFile = null, List<string> ccEmails = null, List<string> ccoEmails = null, List<string> replyTo = null, bool configureAwait = false);
        /// <summary>
        /// ENVIO DE EMAIL ASYNC COM COPIA
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="titleEvent"></param>
        /// <param name="invite"></param>
        /// <param name="local"></param>
        /// <param name="inicio"></param>
        /// <param name="utc"></param>
        /// <param name="fim"></param>
        /// <param name="recorrente"></param>
        /// <param name="pathFile"></param>
        /// <param name="ccoEmails"></param>
        /// <param name="replyTo"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task SendMessageEmailAmazonAsync(string nome, List<string> email, string body, string subject, string titleEvent = null, bool invite = false,
            string local = null, DateTime? inicio = null, DateTime? utc = null, DateTime? fim = null, bool recorrente = false, string pathFile = null, List<string> ccEmails = null, List<string> ccoEmails = null, List<string> replyTo = null, bool configureAwait = false);
        /// <summary>
        /// GERAR HTML ATRAVEZ DE ARQUIVO [INCLUIR ARQUIVO NA PASTA TEMPLATE]
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="substituicao"></param>
        /// <returns></returns>
        string GerateBody(string filename, Dictionary<string, string> substituicao);
    }
}