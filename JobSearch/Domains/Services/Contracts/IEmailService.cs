namespace JobSearch.Domains.Services.Contracts
{
    public interface IEmailService
    {
        Task SendVacancyApplicationEmailAsync(
            string employerEmail,
            string vacancyTitle,
            int userId,
            string coverLetter,
            int? resumeId = null
        );
    }
}