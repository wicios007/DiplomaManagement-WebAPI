using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly DiplomaManagementDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly SmtpClient smtpClient;
        private readonly ILogger<EmailSenderService>  logger;
        private readonly IConfiguration configuration;
        private readonly IUserContextService userContextService;
        public EmailSenderService(DiplomaManagementDbContext _dbContext, UserManager<User> _userManager, SmtpClient _smtpClient, ILogger<EmailSenderService> _logger, IConfiguration _configuration, IUserContextService _userContextService)
        {
            dbContext = _dbContext;
            userManager = _userManager;
            smtpClient = _smtpClient;
            logger = _logger;
            configuration = _configuration;
            userContextService = _userContextService;
        }

        public async Task SendEmailAsync(int recvId, string subject, string content)
        {
            try
            {
                var recv = userManager.Users.FirstOrDefault(c => c.Id == recvId);
                var sender = userManager.Users.FirstOrDefault(c => c.Id == userContextService.GetUserId);
                string fullName = $"{sender.FirstName} {sender.LastName}";
                var from = configuration.GetValue<string>("Email:Smtp:Username");
                await SendEmailAsync(from, fullName, recv.Email, subject, content);

            }catch(Exception e)
            {
                logger.LogError(e, e.Message);
            }
            

        }

        public async Task SendEmailAsync(string from, string senderName, string to, string subject, string content)
        {
             if(string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException($"Email, subject or content is null");
            }
            try
            {
                var mail = new MailAddress(from, senderName);
                var mailMessage = new MailMessage(
                    to : to, 
                    from: from, 
                    subject: subject, 
                    body: content
                    );
                mailMessage.From = mail;
                await smtpClient.SendMailAsync(mailMessage);
            }catch(Exception e)
            {
                logger.LogError(e, e.Message);
                throw;
            }
        }

        public void Dispose()
        {
            smtpClient.Dispose();
        }

        


    }
}
