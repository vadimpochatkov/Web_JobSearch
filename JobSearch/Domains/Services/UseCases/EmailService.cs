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
        private readonly ILogger<EmailService> _logger = logger;

        public async Task SendVacancyApplicationEmailAsync(Response response, Vacancy vacancy, Employer employer, User user, Resume? resume = null)
        {
            if (string.IsNullOrEmpty(employer.Email))
            {
                _logger.LogWarning("Не удалось определить email работодателя");
                return;
            }

            var body = new StringBuilder();
            body.AppendLine($"Пользователь {user?.Name ?? $"с ID {response.UserId}"} откликнулся на вакансию: {vacancy?.Title ?? "Неизвестно"}.");
            body.AppendLine($"Дата отклика: {response.ResponseDate:g}");
            body.AppendLine($"Сопроводительное письмо:\n{response.CoverLetter}");

            if (resume != null)
            {
                body.AppendLine("\n🔹 Информация из резюме:");
                body.AppendLine($" Специальность: {resume.Title}");
                body.AppendLine($" Образование: {resume.Education ?? "не указано"}");
                body.AppendLine($" Опыт работы: {resume.Experience}");
                body.AppendLine($" Дата рождения: {resume.DateofBirth?.ToString("dd.MM.yyyy") ?? "не указана"}");
                body.AppendLine($" Пользователь: {resume.User?.Name ?? "неизвестен"}");
            }

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_configuration["Email:From"]));
            message.To.Add(MailboxAddress.Parse(employer.Email));
            message.Subject = $"Новый отклик на вакансию: {vacancy?.Title ?? "Без названия"}";

            message.Body = new TextPart("plain")
            {
                Text = body.ToString()
            };

            try
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(_configuration["Email:SmtpServer"], int.Parse(_configuration["Email:Port"]), SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(_configuration["Email:Username"] ?? _configuration["Email:From"], _configuration["Email:Password"]);
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

