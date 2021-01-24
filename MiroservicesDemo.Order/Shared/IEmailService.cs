using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroservicesDemo.Order.Shared
{
    public interface IEmailService
    {
        Task SendEmailAsync(string from, string to, string subject, string message);
    }
}
