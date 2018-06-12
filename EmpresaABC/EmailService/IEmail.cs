using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.EmailService
{
    public interface IEmail
    {
        Task SendEmail(string email, string subject, string message);
    }
}
