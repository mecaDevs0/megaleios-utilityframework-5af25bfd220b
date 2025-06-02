using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using UtilityFramework.Application.Core3;
using UtilityFramework.Services.Core3.Interface;
using UtilityFramework.Services.Core3.Models;

namespace UtilityFramework.Services.Core3
{
    public class SendService : ISenderMailService, ISenderNotificationService
    {
        private readonly Config _config;

        public SendService()
        {
            var configuration = Utilities.GetConfigurationRoot();
            _config = configuration.GetSection("SERVICES").Get<Config>();
        }

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
        /// <param name="path"></param>
        /// <param name="ccEmails"></param>
        /// <param name="ccoEmails"></param>
        /// <param name="replyTo"></param>
        public void SendMessageEmail(string nome, string email, string body, string subject, string titleEvent = null, bool invite = false, string local = null, DateTime? inicio = null, DateTime? utc = null, DateTime? fim = null, bool recorrente = false, string path = null, List<string> ccEmails = null, List<string> ccoEmails = null, List<string> replyTo = null)
        {

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_config.SMTP.NAME, _config.SMTP.EMAIL));
                message.To.Add(new MailboxAddress(nome, email));

                if (ccEmails != null && ccEmails.Count > 0)
                    for (int i = 0; i < ccEmails.Count; i++) { message.Cc.Add(new MailboxAddress(ccEmails[i])); }

                if (ccoEmails != null && ccoEmails.Count > 0)
                    for (int i = 0; i < ccoEmails.Count; i++) { message.Bcc.Add(new MailboxAddress(ccoEmails[i])); }

                if (replyTo != null && replyTo.Count > 0)
                    for (int i = 0; i < replyTo.Count; i++) { message.ReplyTo.Add(new MailboxAddress(replyTo[i])); }

                message.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = body };


                if (invite)
                {
                    local ??= "Não informado";
                    inicio ??= DateTime.Now.AddHours(1);
                    fim ??= inicio.GetValueOrDefault().AddHours(2);
                    utc ??= DateTime.UtcNow.AddHours(1);

                    var str = new StringBuilder();
                    str.AppendLine("BEGIN:VCALENDAR");
                    str.AppendLine("PRODID:-//Schedule a Meeting");
                    str.AppendLine("VERSION:2.0");
                    str.AppendLine("METHOD:REQUEST");
                    str.AppendLine("BEGIN:VEVENT");
                    str.AppendLine($"DTSTART:{inicio?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    str.AppendLine($"DTSTAMP:{utc?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    str.AppendLine($"DTEND:{fim?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    if (recorrente)
                        str.AppendLine($"RRULE:FREQ=WEEKLY;COUNT={157}");

                    str.AppendLine("LOCATION:" + local);
                    str.AppendLine($"UID:{Guid.NewGuid()}");
                    str.AppendLine($"DESCRIPTION:{message.Body}");
                    str.AppendLine($"X-ALT-DESC;FMTTYPE=text/html:{message.Body}");
                    str.AppendLine($"SUMMARY:{titleEvent}");
                    str.AppendLine($"ORGANIZER:MAILTO:{_config.SMTP.EMAIL}");
                    str.AppendLine($"ATTENDEE;CN=\"{_config.SMTP.NAME}\";RSVP=TRUE:mailto:{_config.SMTP.EMAIL}");
                    str.AppendLine("BEGIN:VALARM");
                    str.AppendLine("TRIGGER:-PT15M");
                    str.AppendLine("ACTION:DISPLAY");
                    str.AppendLine("DESCRIPTION:Reminder");
                    str.AppendLine("END:VALARM");
                    str.AppendLine("END:VEVENT");
                    str.AppendLine("END:VCALENDAR");

                    var contype = new ContentType("text/calendar", "text/calendar");
                    contype.Parameters?.Add("method", "REQUEST");
                    contype.Parameters?.Add("name", $"{titleEvent.TrimSpaces()}.ics");

                    var bytes = Encoding.ASCII.GetBytes(str.ToString());
                    var ms = new MemoryStream(bytes);

                    builder.Attachments.Add("Meeding.ics", ms, contype);
                }

                if (string.IsNullOrEmpty(path) == false)
                {
                    var listFiles = path.Split(';').ToList();
                    listFiles.ForEach(filePath =>
                    {
                        builder.Attachments.Add(filePath);
                    });
                }

                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();

                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(_config.SMTP.HOST, _config.SMTP.PORT, _config.SMTP.SSL);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                if (_config.SMTP.USEAUTH)
                    client.Authenticate(_config.SMTP.EMAIL, _config.SMTP.PASSWORD);

                client.Send(message);
                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao enviar e-mail", ex);
            }
        }
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
        public void SendMessageEmail(string nome, List<string> email, string body, string subject, string titleEvent = null,
            bool invite = false, string local = null, DateTime? inicio = null, DateTime? utc = null, DateTime? fim = null,
            bool recorrente = false, string pathFile = null, List<string> ccEmails = null, List<string> ccoEmails = null, List<string> replyTo = null)
        {
            try
            {
                if (email.Count == 0)
                    throw new Exception("Informe pelo menos um destinatário");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_config.SMTP.NAME, _config.SMTP.EMAIL));
                message.To.Add(new MailboxAddress(nome, email[0]));

                //remove destinatario principal
                email.RemoveAt(0);

                if (email.Count > 0)
                    for (int i = 0; i < email.Count; i++) { message.Cc.Add(new MailboxAddress(email[i])); }

                if (ccEmails != null && ccEmails.Count > 0)
                    for (int i = 0; i < ccEmails.Count; i++) { message.Cc.Add(new MailboxAddress(ccEmails[i])); }

                if (ccoEmails != null && ccoEmails.Count > 0)
                    for (int i = 0; i < ccoEmails.Count; i++) { message.Bcc.Add(new MailboxAddress(ccoEmails[i])); }

                if (replyTo != null && replyTo.Count > 0)
                    for (int i = 0; i < replyTo.Count; i++) { message.ReplyTo.Add(new MailboxAddress(replyTo[i])); }

                message.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = body };


                if (invite)
                {
                    local ??= "Não informado";
                    inicio ??= DateTime.Now.AddHours(1);
                    fim ??= inicio.GetValueOrDefault().AddHours(2);
                    utc ??= DateTime.UtcNow.AddHours(1);

                    var str = new StringBuilder();
                    str.AppendLine("BEGIN:VCALENDAR");
                    str.AppendLine("PRODID:-//Schedule a Meeting");
                    str.AppendLine("VERSION:2.0");
                    str.AppendLine("METHOD:REQUEST");
                    str.AppendLine("BEGIN:VEVENT");
                    str.AppendLine($"DTSTART:{inicio?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    str.AppendLine($"DTSTAMP:{utc?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    str.AppendLine($"DTEND:{fim?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    if (recorrente)
                        str.AppendLine($"RRULE:FREQ=WEEKLY;COUNT={157}");

                    str.AppendLine("LOCATION:" + local);
                    str.AppendLine($"UID:{Guid.NewGuid()}");
                    str.AppendLine($"DESCRIPTION:{message.Body}");
                    str.AppendLine($"X-ALT-DESC;FMTTYPE=text/html:{message.Body}");
                    str.AppendLine($"SUMMARY:{titleEvent}");
                    str.AppendLine($"ORGANIZER:MAILTO:{_config.SMTP.EMAIL}");
                    str.AppendLine($"ATTENDEE;CN=\"{_config.SMTP.NAME}\";RSVP=TRUE:mailto:{_config.SMTP.EMAIL}");
                    str.AppendLine("BEGIN:VALARM");
                    str.AppendLine("TRIGGER:-PT15M");
                    str.AppendLine("ACTION:DISPLAY");
                    str.AppendLine("DESCRIPTION:Reminder");
                    str.AppendLine("END:VALARM");
                    str.AppendLine("END:VEVENT");
                    str.AppendLine("END:VCALENDAR");

                    var contype = new ContentType("text/calendar", "text/calendar");
                    contype.Parameters?.Add("method", "REQUEST");
                    contype.Parameters?.Add("name", $"{titleEvent.TrimSpaces()}.ics");

                    var bytes = Encoding.ASCII.GetBytes(str.ToString());
                    var ms = new MemoryStream(bytes);

                    builder.Attachments.Add("Meeding.ics", ms, contype);
                }

                if (string.IsNullOrEmpty(pathFile) == false)
                {
                    var listFiles = pathFile.Split(';').ToList();
                    listFiles.ForEach(filePath =>
                    {
                        builder.Attachments.Add(filePath);
                    });
                }

                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();

                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(_config.SMTP.HOST, _config.SMTP.PORT, _config.SMTP.SSL);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                if (_config.SMTP.USEAUTH)
                    client.Authenticate(_config.SMTP.EMAIL, _config.SMTP.PASSWORD);

                client.Send(message);
                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao enviar e-mail", ex);
            }
        }
        /// <summary>
        /// ENVIO DE EMAIL AMAZON
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
        public void SendMessageEmailAmazon(string nome, List<string> email, string body, string subject, string titleEvent = null,
            bool invite = false, string local = null, DateTime? inicio = null, DateTime? utc = null, DateTime? fim = null,
            bool recorrente = false, string pathFile = null, List<string> ccEmails = null, List<string> ccoEmails = null, List<string> replyTo = null)
        {
            try
            {
                if (email.Count == 0)
                    throw new Exception("Informe pelo menos um destinatário");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_config.SMTP.NAME, _config.SMTP.EMAIL));
                message.To.Add(new MailboxAddress(nome, email[0]));

                //remove destinatario principal
                email.RemoveAt(0);

                if (email.Count > 0)
                    for (int i = 0; i < email.Count; i++) { message.Cc.Add(new MailboxAddress(email[i])); }

                if (ccEmails != null && ccEmails.Count > 0)
                    for (int i = 0; i < ccEmails.Count; i++) { message.Cc.Add(new MailboxAddress(ccEmails[i])); }

                if (ccoEmails != null && ccoEmails.Count > 0)
                    for (int i = 0; i < ccoEmails.Count; i++) { message.Bcc.Add(new MailboxAddress(ccoEmails[i])); }

                if (replyTo != null && replyTo.Count > 0)
                    for (int i = 0; i < replyTo.Count; i++) { message.ReplyTo.Add(new MailboxAddress(replyTo[i])); }

                message.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = body };


                if (invite)
                {
                    local ??= "Não informado";
                    inicio ??= DateTime.Now.AddHours(1);
                    fim ??= inicio.GetValueOrDefault().AddHours(2);
                    utc ??= DateTime.UtcNow.AddHours(1);

                    var str = new StringBuilder();
                    str.AppendLine("BEGIN:VCALENDAR");
                    str.AppendLine("PRODID:-//Schedule a Meeting");
                    str.AppendLine("VERSION:2.0");
                    str.AppendLine("METHOD:REQUEST");
                    str.AppendLine("BEGIN:VEVENT");
                    str.AppendLine($"DTSTART:{inicio?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    str.AppendLine($"DTSTAMP:{utc?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    str.AppendLine($"DTEND:{fim?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    if (recorrente)
                        str.AppendLine($"RRULE:FREQ=WEEKLY;COUNT={157}");

                    str.AppendLine("LOCATION:" + local);
                    str.AppendLine($"UID:{Guid.NewGuid()}");
                    str.AppendLine($"DESCRIPTION:{message.Body}");
                    str.AppendLine($"X-ALT-DESC;FMTTYPE=text/html:{message.Body}");
                    str.AppendLine($"SUMMARY:{titleEvent}");
                    str.AppendLine($"ORGANIZER:MAILTO:{_config.SMTP.EMAIL}");
                    str.AppendLine($"ATTENDEE;CN=\"{_config.SMTP.NAME}\";RSVP=TRUE:mailto:{_config.SMTP.EMAIL}");
                    str.AppendLine("BEGIN:VALARM");
                    str.AppendLine("TRIGGER:-PT15M");
                    str.AppendLine("ACTION:DISPLAY");
                    str.AppendLine("DESCRIPTION:Reminder");
                    str.AppendLine("END:VALARM");
                    str.AppendLine("END:VEVENT");
                    str.AppendLine("END:VCALENDAR");

                    var contype = new ContentType("text/calendar", "text/calendar");
                    contype.Parameters?.Add("method", "REQUEST");
                    contype.Parameters?.Add("name", $"{titleEvent.TrimSpaces()}.ics");

                    var bytes = Encoding.ASCII.GetBytes(str.ToString());
                    var ms = new MemoryStream(bytes);

                    builder.Attachments.Add("Meeding.ics", ms, contype);
                }

                if (string.IsNullOrEmpty(pathFile) == false)
                {
                    var listFiles = pathFile.Split(';').ToList();
                    listFiles.ForEach(filePath =>
                    {
                        builder.Attachments.Add(filePath);
                    });
                }

                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Connect(new Uri("smtp://" + _config.SMTP.HOST + ":" + _config.SMTP.PORT + "/?starttls=" + _config.SMTP.SSL));

                // Note: only needed if the SMTP server requires authentication
                if (_config.SMTP.USEAUTH)
                    client.Authenticate(_config.SMTP.LOGIN, _config.SMTP.PASSWORD);

                client.Send(message);
                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao enviar e-mail", ex);
            }
        }
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
        public async Task SendMessageEmailAsync(string nome, string email, string body, string subject, string titleEvent = null,
            bool invite = false, string local = null, DateTime? inicio = null, DateTime? utc = null, DateTime? fim = null,
            bool recorrente = false, string pathFile = null, List<string> ccEmails = null, List<string> ccoEmails = null, List<string> replyTo = null, bool configureAwait = false)
        {
            try
            {
                if (email.Length == 0)
                    throw new Exception("Informe pelo menos um destinatário");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_config.SMTP.NAME, _config.SMTP.EMAIL));
                message.To.Add(new MailboxAddress(nome, email));

                if (ccEmails != null && ccEmails.Count > 0)
                    for (int i = 0; i < ccEmails.Count; i++) { message.Cc.Add(new MailboxAddress(ccEmails[i])); }

                if (ccoEmails != null && ccoEmails.Count > 0)
                    for (int i = 0; i < ccoEmails.Count; i++) { message.Bcc.Add(new MailboxAddress(ccoEmails[i])); }

                if (replyTo != null && replyTo.Count > 0)
                    for (int i = 0; i < replyTo.Count; i++) { message.ReplyTo.Add(new MailboxAddress(replyTo[i])); }

                message.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = body };


                if (invite)
                {
                    local ??= "Não informado";
                    inicio ??= DateTime.Now.AddHours(1);
                    fim ??= inicio.GetValueOrDefault().AddHours(2);
                    utc ??= DateTime.UtcNow.AddHours(1);

                    var str = new StringBuilder();
                    str.AppendLine("BEGIN:VCALENDAR");
                    str.AppendLine("PRODID:-//Schedule a Meeting");
                    str.AppendLine("VERSION:2.0");
                    str.AppendLine("METHOD:REQUEST");
                    str.AppendLine("BEGIN:VEVENT");
                    str.AppendLine($"DTSTART:{inicio?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    str.AppendLine($"DTSTAMP:{utc?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    str.AppendLine($"DTEND:{fim?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    if (recorrente)
                        str.AppendLine($"RRULE:FREQ=WEEKLY;COUNT={157}");

                    str.AppendLine("LOCATION:" + local);
                    str.AppendLine($"UID:{Guid.NewGuid()}");
                    str.AppendLine($"DESCRIPTION:{message.Body}");
                    str.AppendLine($"X-ALT-DESC;FMTTYPE=text/html:{message.Body}");
                    str.AppendLine($"SUMMARY:{titleEvent}");
                    str.AppendLine($"ORGANIZER:MAILTO:{_config.SMTP.EMAIL}");
                    str.AppendLine($"ATTENDEE;CN=\"{_config.SMTP.NAME}\";RSVP=TRUE:mailto:{_config.SMTP.EMAIL}");
                    str.AppendLine("BEGIN:VALARM");
                    str.AppendLine("TRIGGER:-PT15M");
                    str.AppendLine("ACTION:DISPLAY");
                    str.AppendLine("DESCRIPTION:Reminder");
                    str.AppendLine("END:VALARM");
                    str.AppendLine("END:VEVENT");
                    str.AppendLine("END:VCALENDAR");

                    var contype = new ContentType("text/calendar", "text/calendar");
                    contype.Parameters?.Add("method", "REQUEST");
                    contype.Parameters?.Add("name", $"{titleEvent.TrimSpaces()}.ics");

                    var bytes = Encoding.ASCII.GetBytes(str.ToString());
                    var ms = new MemoryStream(bytes);

                    builder.Attachments.Add("Meeding.ics", ms, contype);
                }

                if (string.IsNullOrEmpty(pathFile) == false)
                {
                    var listFiles = pathFile.Split(';').ToList();
                    listFiles.ForEach(filePath =>
                    {
                        builder.Attachments.Add(filePath);
                    });
                }

                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();

                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_config.SMTP.HOST, _config.SMTP.PORT, _config.SMTP.SSL);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                if (_config.SMTP.USEAUTH)
                    await client.AuthenticateAsync(_config.SMTP.EMAIL, _config.SMTP.PASSWORD).ConfigureAwait(configureAwait); ;

                await client.SendAsync(message).ConfigureAwait(configureAwait);
                await client.DisconnectAsync(true).ConfigureAwait(configureAwait);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao enviar e-mail", ex);
            }
        }
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
        /// <param name="ccoEmails"></param>
        /// <param name="replyTo"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        public async Task SendMessageEmailAsync(string nome, List<string> email, string body, string subject, string titleEvent = null,
            bool invite = false, string local = null, DateTime? inicio = null, DateTime? utc = null, DateTime? fim = null,
            bool recorrente = false, string pathFile = null, List<string> ccEmails = null, List<string> ccoEmails = null, List<string> replyTo = null, bool configureAwait = false)
        {
            try
            {
                if (email.Count == 0)
                    throw new Exception("Informe pelo menos um destinatário");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_config.SMTP.NAME, _config.SMTP.EMAIL));
                message.To.Add(new MailboxAddress(nome, email[0]));

                email.RemoveAt(0);

                if (email.Count > 0)
                    for (int i = 0; i < email.Count; i++) { message.Cc.Add(new MailboxAddress(email[i])); }

                if (ccEmails != null && ccEmails.Count > 0)
                    for (int i = 0; i < ccEmails.Count; i++) { message.Cc.Add(new MailboxAddress(ccEmails[i])); }

                if (ccoEmails != null && ccoEmails.Count > 0)
                    for (int i = 0; i < ccoEmails.Count; i++) { message.Bcc.Add(new MailboxAddress(ccoEmails[i])); }

                if (replyTo != null && replyTo.Count > 0)
                    for (int i = 0; i < replyTo.Count; i++) { message.ReplyTo.Add(new MailboxAddress(replyTo[i])); }

                message.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = body };


                if (invite)
                {
                    local ??= "Não informado";
                    inicio ??= DateTime.Now.AddHours(1);
                    fim ??= inicio.GetValueOrDefault().AddHours(2);
                    utc ??= DateTime.UtcNow.AddHours(1);

                    var str = new StringBuilder();
                    str.AppendLine("BEGIN:VCALENDAR");
                    str.AppendLine("PRODID:-//Schedule a Meeting");
                    str.AppendLine("VERSION:2.0");
                    str.AppendLine("METHOD:REQUEST");
                    str.AppendLine("BEGIN:VEVENT");
                    str.AppendLine($"DTSTART:{inicio?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    str.AppendLine($"DTSTAMP:{utc?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    str.AppendLine($"DTEND:{fim?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    if (recorrente)
                        str.AppendLine($"RRULE:FREQ=WEEKLY;COUNT={157}");

                    str.AppendLine("LOCATION:" + local);
                    str.AppendLine($"UID:{Guid.NewGuid()}");
                    str.AppendLine($"DESCRIPTION:{message.Body}");
                    str.AppendLine($"X-ALT-DESC;FMTTYPE=text/html:{message.Body}");
                    str.AppendLine($"SUMMARY:{titleEvent}");
                    str.AppendLine($"ORGANIZER:MAILTO:{_config.SMTP.EMAIL}");
                    str.AppendLine($"ATTENDEE;CN=\"{_config.SMTP.NAME}\";RSVP=TRUE:mailto:{_config.SMTP.EMAIL}");
                    str.AppendLine("BEGIN:VALARM");
                    str.AppendLine("TRIGGER:-PT15M");
                    str.AppendLine("ACTION:DISPLAY");
                    str.AppendLine("DESCRIPTION:Reminder");
                    str.AppendLine("END:VALARM");
                    str.AppendLine("END:VEVENT");
                    str.AppendLine("END:VCALENDAR");

                    var contype = new ContentType("text/calendar", "text/calendar");
                    contype.Parameters?.Add("method", "REQUEST");
                    contype.Parameters?.Add("name", $"{titleEvent.TrimSpaces()}.ics");

                    var bytes = Encoding.ASCII.GetBytes(str.ToString());
                    var ms = new MemoryStream(bytes);

                    builder.Attachments.Add("Meeding.ics", ms, contype);
                }

                if (string.IsNullOrEmpty(pathFile) == false)
                {
                    var listFiles = pathFile.Split(';').ToList();
                    listFiles.ForEach(filePath =>
                    {
                        builder.Attachments.Add(filePath);
                    });
                }

                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_config.SMTP.HOST, _config.SMTP.PORT, _config.SMTP.SSL);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                if (_config.SMTP.USEAUTH)
                    await client.AuthenticateAsync(_config.SMTP.EMAIL, _config.SMTP.PASSWORD).ConfigureAwait(configureAwait); ;

                await client.SendAsync(message).ConfigureAwait(configureAwait);
                await client.DisconnectAsync(true).ConfigureAwait(configureAwait);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao enviar e-mail", ex);
            }
        }
        /// <summary>
        /// ENVIO DE EMAIL AMAZON
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
        public async Task SendMessageEmailAmazonAsync(string nome, List<string> email, string body, string subject, string titleEvent = null,
            bool invite = false, string local = null, DateTime? inicio = null, DateTime? utc = null, DateTime? fim = null,
            bool recorrente = false, string pathFile = null, List<string> ccEmails = null, List<string> ccoEmails = null, List<string> replyTo = null, bool configureAwait = false)
        {
            try
            {
                if (email.Count == 0)
                    throw new Exception("Informe pelo menos um destinatário");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_config.SMTP.NAME, _config.SMTP.EMAIL));
                message.To.Add(new MailboxAddress(nome, email[0]));

                email.RemoveAt(0);

                if (email.Count > 0)
                    for (int i = 0; i < email.Count; i++) { message.Cc.Add(new MailboxAddress(email[i])); }

                if (ccEmails != null && ccEmails.Count > 0)
                    for (int i = 0; i < ccEmails.Count; i++) { message.Cc.Add(new MailboxAddress(ccEmails[i])); }

                if (ccoEmails != null && ccoEmails.Count > 0)
                    for (int i = 0; i < ccoEmails.Count; i++) { message.Bcc.Add(new MailboxAddress(ccoEmails[i])); }

                if (replyTo != null && replyTo.Count > 0)
                    for (int i = 0; i < replyTo.Count; i++) { message.ReplyTo.Add(new MailboxAddress(replyTo[i])); }

                message.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = body };


                if (invite)
                {
                    local ??= "Não informado";
                    inicio ??= DateTime.Now.AddHours(1);
                    fim ??= inicio.GetValueOrDefault().AddHours(2);
                    utc ??= DateTime.UtcNow.AddHours(1);

                    var str = new StringBuilder();
                    str.AppendLine("BEGIN:VCALENDAR");
                    str.AppendLine("PRODID:-//Schedule a Meeting");
                    str.AppendLine("VERSION:2.0");
                    str.AppendLine("METHOD:REQUEST");
                    str.AppendLine("BEGIN:VEVENT");
                    str.AppendLine($"DTSTART:{inicio?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    str.AppendLine($"DTSTAMP:{utc?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    str.AppendLine($"DTEND:{fim?.ToUniversalTime():yyyyMMddTHHmmssZ}");
                    if (recorrente)
                        str.AppendLine($"RRULE:FREQ=WEEKLY;COUNT={157}");

                    str.AppendLine("LOCATION:" + local);
                    str.AppendLine($"UID:{Guid.NewGuid()}");
                    str.AppendLine($"DESCRIPTION:{message.Body}");
                    str.AppendLine($"X-ALT-DESC;FMTTYPE=text/html:{message.Body}");
                    str.AppendLine($"SUMMARY:{titleEvent}");
                    str.AppendLine($"ORGANIZER:MAILTO:{_config.SMTP.EMAIL}");
                    str.AppendLine($"ATTENDEE;CN=\"{_config.SMTP.NAME}\";RSVP=TRUE:mailto:{_config.SMTP.EMAIL}");
                    str.AppendLine("BEGIN:VALARM");
                    str.AppendLine("TRIGGER:-PT15M");
                    str.AppendLine("ACTION:DISPLAY");
                    str.AppendLine("DESCRIPTION:Reminder");
                    str.AppendLine("END:VALARM");
                    str.AppendLine("END:VEVENT");
                    str.AppendLine("END:VCALENDAR");

                    var contype = new ContentType("text/calendar", "text/calendar");
                    contype.Parameters?.Add("method", "REQUEST");
                    contype.Parameters?.Add("name", $"{titleEvent.TrimSpaces()}.ics");

                    var bytes = Encoding.ASCII.GetBytes(str.ToString());
                    var ms = new MemoryStream(bytes);

                    builder.Attachments.Add("Meeding.ics", ms, contype);
                }

                if (string.IsNullOrEmpty(pathFile) == false)
                {
                    var listFiles = pathFile.Split(';').ToList();
                    listFiles.ForEach(filePath =>
                    {
                        builder.Attachments.Add(filePath);
                    });
                }

                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                await client.ConnectAsync(new Uri("smtp://" + _config.SMTP.HOST + ":" + _config.SMTP.PORT + "/?starttls=" + _config.SMTP.SSL)).ConfigureAwait(configureAwait);

                // Note: only needed if the SMTP server requires authentication
                if (_config.SMTP.USEAUTH)
                    await client.AuthenticateAsync(_config.SMTP.LOGIN, _config.SMTP.PASSWORD).ConfigureAwait(configureAwait);

                await client.SendAsync(message).ConfigureAwait(configureAwait);
                await client.DisconnectAsync(true).ConfigureAwait(configureAwait);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao enviar e-mail", ex);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename">NOME DO ARQUIVO SEM .HTML</param>
        /// <param name="substituicao"></param>
        /// <returns></returns>
        public string GerateBody(string filename, Dictionary<string, string> substituicao)
        {
            var body = LoadTemplate($"{filename}.html");

            body = substituicao.Aggregate(body, (current, item) => current.Replace(item.Key, item.Value));
            return body;
        }
        private string LoadTemplate(string fileName)
        {
            string conteudo;

            try
            {
                conteudo = File.ReadAllText(Path.Combine($"{Directory.GetCurrentDirectory()}/{_config.SMTP.TEMPLATE}", fileName));
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar template de e-mail", e);
            }
            return conteudo;
        }

        /// <summary>
        /// ENVIO DE PUSH NOTIFICATION ASYNC
        /// </summary>
        /// <param name="senderName"></param>
        /// <param name="message"></param>
        /// <param name="devicePushId"></param>
        /// <param name="groupName"></param>
        /// <param name="senderPhoto"></param>
        /// <param name="data"></param>
        /// <param name="dataSend"></param>
        /// <param name="indexKeys"></param>
        /// <param name="priority"></param>
        /// <param name="url"></param>
        /// <param name="sound"></param>
        /// <param name="customIcon"></param>
        /// <param name="settings"></param>
        /// <param name="configureAwait"></param>
        /// <param name="messagelength"></param>
        /// <param name="titleLength"></param>
        /// <returns></returns>
        public async Task<OneSignalResponse> SendPushAsync(string senderName, string message, IEnumerable<string> devicePushId, string groupName = null,
            string senderPhoto = null, JObject data = null, DateTime? dataSend = null, int indexKeys = 0, int priority = 1,
            string url = null, string sound = null, string customIcon = null, JObject settings = null, bool configureAwait = false, int messagelength = 150, int titleLength = 50)
        {
            try
            {
                var listDevices = devicePushId?.Where(x => string.IsNullOrEmpty(x) == false).Distinct().ToList() ?? new List<string>();

                if (listDevices.Count == 0) return null;

                var client = new RestClient("https://onesignal.com/api/v1/notifications");

                var request = new RestRequest(Method.POST);

                dynamic contentMessage = Combine(new JObject(), settings);

                dynamic content = new JObject();
                content.en = message.Truncate(messagelength);

                dynamic headings = new JObject();
                headings.en = senderName.Truncate(titleLength);

                contentMessage.ios_sound = sound ?? _config.ONESIGNAL[indexKeys].SOUND;
                contentMessage.android_sound = RemoveExt(sound ?? _config.ONESIGNAL[indexKeys].SOUND);
                contentMessage.app_id = _config.ONESIGNAL[indexKeys].APPID;
                contentMessage.contents = content;
                contentMessage.small_icon = customIcon ?? _config.ONESIGNAL[indexKeys].ICON;
                contentMessage.headings = headings;
                contentMessage.priority = priority;

                if (string.IsNullOrEmpty(groupName) == false)
                    contentMessage.android_group = $"group_{groupName}";

                if (string.IsNullOrEmpty(url) == false)
                    contentMessage.url = url;

                if (string.IsNullOrWhiteSpace(senderPhoto) == false)
                    contentMessage.large_icon = senderPhoto;

                if (data != null)
                    contentMessage.data = data;

                if (dataSend != null)
                    contentMessage.send_after = dataSend.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss zzz");


                contentMessage.include_player_ids = new JArray(listDevices);

                var json = JsonConvert.SerializeObject(contentMessage);

                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", $"Basic {_config.ONESIGNAL[indexKeys].KEY}");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var response = await client.ExecuteAsync<OneSignalResponse>(request).ConfigureAwait(configureAwait);

                if (response.StatusCode != HttpStatusCode.OK)
                    response.Data.Erro = true;

                var _return = response.Data;
                _return.StatusCode = (int)response.StatusCode;

                return _return;

            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao enviar notificação onesignal.", e);
            }
        }

        /// <summary>
        /// ENVIO DE PUSH NOTIFICATION PARA TODOS DEVICES REGISTRADOS ASYNC
        /// </summary>
        /// <param name="senderName"></param>
        /// <param name="message"></param>
        /// <param name="groupName"></param>
        /// <param name="senderPhoto"></param>
        /// <param name="segments"></param>
        /// <param name="data"></param>
        /// <param name="dataSend"></param>
        /// <param name="indexKeys"></param>
        /// <param name="priority"></param>
        /// <param name="url"></param>
        /// <param name="sound"></param>
        /// <param name="customIcon"></param>
        /// <param name="settings"></param>
        /// <param name="configureAwait"></param>
        /// <param name="messagelength"></param>
        /// <param name="titleLength"></param>
        /// <returns></returns>
        public async Task<OneSignalResponse> SendAllDevicesAsync(string senderName, string message, string groupName = null, string senderPhoto = null,
            string segments = "All", JObject data = null, DateTime? dataSend = null, int indexKeys = 0, int priority = 1,
            string url = null, string sound = null, string customIcon = null, JObject settings = null, bool configureAwait = false, int messagelength = 150, int titleLength = 50)
        {
            try
            {
                var client = new RestClient("https://onesignal.com/api/v1/notifications");

                var request = new RestRequest(Method.POST);

                dynamic contentMessage = Combine(new JObject(), settings);

                dynamic content = new JObject();
                content.en = message.Truncate(messagelength);

                dynamic headings = new JObject();
                headings.en = senderName.Truncate(titleLength);

                contentMessage.ios_sound = sound ?? _config.ONESIGNAL[indexKeys].SOUND;
                contentMessage.android_sound = RemoveExt(sound ?? _config.ONESIGNAL[indexKeys].SOUND);
                contentMessage.app_id = _config.ONESIGNAL[indexKeys].APPID;
                contentMessage.contents = content;
                contentMessage.small_icon = customIcon ?? _config.ONESIGNAL[indexKeys].ICON;
                contentMessage.headings = headings;
                contentMessage.priority = priority;
                contentMessage.included_segments = new JArray(new List<string> { segments });

                if (string.IsNullOrEmpty(groupName) == false)
                    contentMessage.android_group = $"group_{groupName}";

                if (string.IsNullOrEmpty(url) == false)
                    contentMessage.url = url;

                if (data != null)
                    contentMessage.data = data;

                if (!string.IsNullOrWhiteSpace(senderPhoto))
                    contentMessage.large_icon = senderPhoto;
                if (dataSend != null)
                    contentMessage.send_after = dataSend.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss zzz");


                var json = JsonConvert.SerializeObject(contentMessage);

                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", $"Basic {_config.ONESIGNAL[indexKeys].KEY}");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var response = await client.ExecuteAsync<OneSignalResponse>(request).ConfigureAwait(configureAwait);
                if (response.StatusCode != HttpStatusCode.OK)
                    response.Data.Erro = true;

                var _return = response.Data;
                _return.StatusCode = (int)response.StatusCode;

                return _return;
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao enviar notificação onesignal.", e);
            }
        }

        /// <summary>
        /// OBTER DETALHS DE UMA NOTIFICAÇÃO PUSH ENVIADA
        /// </summary>
        /// <param name="notificationId"></param>
        /// <param name="indexKeys"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        public async Task<OneSignalModel> GetNotificationAsync(string notificationId, int indexKeys = 0, bool configureAwait = false)
        {
            try
            {
                var client = new RestClient($"https://onesignal.com/api/v1/notifications/{notificationId}?app_id={_config.ONESIGNAL[indexKeys].APPID}");

                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", $"Basic {_config.ONESIGNAL[indexKeys].KEY}");

                var response = await client.ExecuteAsync<OneSignalModel>(request).ConfigureAwait(configureAwait);

                if (response.StatusCode != HttpStatusCode.OK && response.Data != null)
                    response.Data.Erro = true;

                return response.Data;
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao cancelar notificação onesignal.", e);
            }
        }

        /// <summary>
        /// CANCELAR PUSH ENVIADO PELO ID
        /// </summary>
        /// <param name="notificationId"></param>
        /// <param name="indexKeys"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        public async Task<bool> CancelPushAsync(string notificationId, int indexKeys = 0, bool configureAwait = false)
        {
            try
            {
                var client = new RestClient($"https://onesignal.com/api/v1/notifications/{notificationId}?app_id={_config.ONESIGNAL[indexKeys].APPID}");

                var request = new RestRequest(Method.DELETE);
                request.AddHeader("Authorization", $"Basic {_config.ONESIGNAL[indexKeys].KEY}");

                var response = await client.ExecuteAsync<OneSignalResponse>(request).ConfigureAwait(configureAwait);

                if (response.StatusCode != HttpStatusCode.OK)
                    response.Data.Erro = true;

                return response.Data.Success;
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro ao cancelar notificação onesignal.", e);
            }
        }

        private static dynamic Combine(JObject item1, JObject item2)
        {
            if (item2 != null)
            {
                item1.Merge(item2, new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union
                });
            }
            return item1;
        }

        private string RemoveExt(string file)
        {
            return file?.Split('.')[0];
        }
    }
}