using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Text;

using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.Entities;

namespace JobSearch.Domains.Services.UseCases
{
    public class EmailService(
        IConfiguration configuration,
        IResumeService resumeService,
        IVacancyService vacancyService,
        IUserService userService,
        ILogger<EmailService> logger
    ) : IEmailService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IResumeService _resumeService = resumeService;
        private readonly IVacancyService _vacancyService = vacancyService;
        private readonly IUserService _userService = userService;
        private readonly ILogger<EmailService> _logger = logger;

        public async Task SendVacancyApplicationEmailAsync(Responce responce)
        {
            var vacancy = responce.Vacancy ?? await _vacancyService.GetByIdAsync(responce.VacancyId);
            var user = responce.User ?? await _userService.GetByIdAsync(responce.UserId);
            var employerEmail = vacancy?.Employer?.Email;

            if (string.IsNullOrEmpty(employerEmail))
            {
                _logger.LogWarning("Не удалось определить email работодателя");
                return;
            }

            var body = new StringBuilder();
            body.AppendLine($"Пользователь {user?.Name ?? $"с ID {responce.UserId}"} откликнулся на вакансию: {vacancy?.Title ?? "Неизвестно"}.");
            body.AppendLine($"Дата отклика: {responce.ResponceDate:g}");
            body.AppendLine($"Сопроводительное письмо:\n{responce.CoverLetter}");

            if (responce.ResumeId.HasValue)
            {
                var resume = await _resumeService.GetResumeByIdAsync(responce.ResumeId.Value);
                if (resume != null)
                {
                    body.AppendLine("\nРезюме:")
                        .AppendLine($"- Название: {resume.Title}")
                        .AppendLine($"- Образование: {resume.Education}")
                        .AppendLine($"- Опыт: {resume.Experience}");
                }
            }

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_configuration["Email:From"]));
            message.To.Add(MailboxAddress.Parse(employerEmail));
            message.Subject = $"Новый отклик на вакансию: {vacancy?.Title ?? "Без названия"}";

            message.Body = new TextPart("plain")
            {
                Text = body.ToString()
            };

            try
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(
                    _configuration["Email:SmtpServer"],
                    int.Parse(_configuration["Email:Port"]),
                    SecureSocketOptions.SslOnConnect
                );

                await client.AuthenticateAsync(
                    _configuration["Email:Username"] ?? _configuration["Email:From"],
                    _configuration["Email:Password"]
                );

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при отправке письма работодателю (MailKit)");
                throw;
            }
        }
    }
}
