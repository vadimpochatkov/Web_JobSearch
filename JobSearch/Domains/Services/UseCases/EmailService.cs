using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.Entities;
using System.Text;

namespace JobSearch.Domains.Services.UseCases
{
    public class EmailService(
        IConfiguration configuration,
        IResumeService resumeService,
        ILogger<EmailService> logger
    ) : IEmailService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IResumeService _resumeService = resumeService;
        private readonly ILogger<EmailService> _logger = logger;

        public async Task SendVacancyApplicationEmailAsync(
            string employerEmail,
            string vacancyTitle,
            int userId,
            string coverLetter,
            int? resumeId = null
        )
        {
            // Формирование тела письма
            var body = new StringBuilder();
            body.AppendLine($"Пользователь с ID {userId} откликнулся на вакансию: {vacancyTitle}.");
            body.AppendLine($"Дата отклика: {DateTime.UtcNow:g}");
            body.AppendLine($"Сопроводительное письмо:\n{coverLetter}");

            // Добавление информации о резюме
            if (resumeId.HasValue)
            {
                var resume = await _resumeService.GetResumeByIdAsync(resumeId.Value);
                if (resume != null)
                {
                    body.AppendLine("\nРезюме:")
                        .AppendLine($"- Название: {resume.Title}")
                        .AppendLine($"- Образование: {resume.Education}")
                        .AppendLine($"- Опыт: {resume.Experience}");
                }
            }

            // Настройки SMTP
            var smtpClient = new SmtpClient(_configuration["Email:SmtpServer"])
            {
                Port = int.Parse(_configuration["Email:Port"]),
                Credentials = new NetworkCredential(
                    _configuration["Email:Username"],
                    _configuration["Email:Password"]
                ),
                EnableSsl = true,
                Timeout = 15000
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:From"]),
                Subject = $"Новый отклик на вакансию: {vacancyTitle}",
                Body = body.ToString(),
                IsBodyHtml = false
            };
            mailMessage.To.Add(employerEmail);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка отправки письма");
                throw;
            }
        }
    }
}