using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Formulario_4
{
    public class Correos
    {
        public List<string> Destinatarios { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
    }

    //Interfaz para envío de correos
    public static class InterfaceEmail {
        public static void SendEmail(Correos model)
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("aplicaciones@astsoftcr.com", "NotificacionesRRHH", System.Text.Encoding.UTF8);

            foreach (var destinatario in model.Destinatarios)
            {
                correo.To.Add(destinatario);
            }

            correo.Subject = model.Asunto; 
            correo.Body = model.Cuerpo; 
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.High;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.office365.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("aplicaciones@astsoftcr.com", "@stsoft.987");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(correo);
            }
            catch (Exception)
            {
            }
        }
    }
}