namespace MiroservicesDemo.Order.Shared
{
    using System;
    using System.ComponentModel;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string from, string to, string subject, string message)
        {
            using MailMessage mailMessage = new MailMessage(new MailAddress(from), new MailAddress(to))
            {
                Subject = subject,
                Body = message,                
            };
            
            using SmtpClient client = new SmtpClient
            {
                Host = Environment.GetEnvironmentVariable("SMTP_HOST_NAME") ?? "localhost",
                Port = int.TryParse(Environment.GetEnvironmentVariable("SMTP_HOST_PORT"), out int port) ? port : 1025
            };

            client.SendCompleted += (sender, args) =>
            {
                // Get the unique identifier for this asynchronous operation.
                // var token = (string)args.UserState;

                var message = args switch
                {
                    AsyncCompletedEventArgs a when args.Cancelled == true => $"Send canceled.",
                    AsyncCompletedEventArgs when args.Error != null => $"{args.Error}",
                    _ => "Mail messga has been sent."
                };

                Console.WriteLine(message);
            };

            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }
    }
}
