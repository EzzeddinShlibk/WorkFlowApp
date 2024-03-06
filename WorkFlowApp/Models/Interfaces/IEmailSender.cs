using WorkFlowApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace WorkFlowApp.Models.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}
