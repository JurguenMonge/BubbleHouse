using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public class EmailSender
{
    private string _fromAddress;
    private string _password;
    private SmtpClient _smtpClient;

    public EmailSender(string fromAddress, string password)
    {
        _fromAddress = fromAddress;
        _password = password;

        // Configurar el cliente SMTP
        _smtpClient = new SmtpClient
        {
            Host = "smtp-mail.outlook.com", // Servidor SMTP 
            Port = 587,              // Puerto SMTP
            EnableSsl = true,        // Habilitar SSL para seguridad
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_fromAddress, _password)
        };
    }

    public bool SendEmail(string toAddress, string subject, string body)
    {
        // Crear un mensaje de correo electrónico
        MailMessage message = new MailMessage(_fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        };

        try
        {
            // Enviar el correo electrónico
            _smtpClient.Send(message);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
            return false;
        }
    }
}
