using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace BlazorPeliculas.Servicios
{
    public class ServicioCorreos(IConfiguration configuration) : IEmailSender
    {
        public async Task SendEmailAsync(string emailDestinatario, string asunto, string cuerpo)
        {
            var nuestroEmail = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:EMAIL");
            var password = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:PASSWORD");
            var host = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:HOST");
            var puerto = configuration.GetValue<int>("CONFIGURACIONES_EMAIL:PUERTO");

            var smtpCliente = new SmtpClient(host, puerto);
            smtpCliente.EnableSsl = true;
            smtpCliente.UseDefaultCredentials = false;
            smtpCliente.Credentials = new NetworkCredential(nuestroEmail, password);

            var mensaje = new MailMessage(nuestroEmail!, emailDestinatario, asunto, cuerpo);
            mensaje.IsBodyHtml = true;
            await smtpCliente.SendMailAsync(mensaje);
        }
    }
}
