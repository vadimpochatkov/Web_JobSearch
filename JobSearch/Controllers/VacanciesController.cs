using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VacanciesController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;
        private readonly IResponceService _responceService;
        private readonly IEmailService _emailService;
        private readonly IResumeService _resumeService;
        private readonly IEmployerService _employerService;

        public VacanciesController(
            IVacancyService vacancyService,
            IResponceService responceService,
            IEmailService emailService,
            IResumeService resumeService,
            IEmployerService employerService)
        {
            _vacancyService = vacancyService;
            _responceService = responceService;
            _emailService = emailService;
            _resumeService = resumeService; 
            _employerService = employerService;
        }

        /// <summary>
        /// Создать новую вакансию.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] VacancyDto dto)
        {
            var vacancy = await _vacancyService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = vacancy.VacancyId }, vacancy);
        }

        /// <summary>
        /// Получить все вакансии.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vacancies = await _vacancyService.GetAllAsync();
            return Ok(vacancies);
        }

        /// <summary>
        /// Получить вакансию по ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vacancy = await _vacancyService.GetByIdAsync(id);
            if (vacancy == null)
                return NotFound();
            return Ok(vacancy);
        }

        /// <summary>
        /// Обновить информацию о вакансии.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] VacancyDto dto)
        {
            await _vacancyService.UpdateAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Удалить вакансию.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vacancyService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Откликнуться на вакансию (с прикреплением резюме и уведомлением работодателя).
        /// </summary>
        [Authorize]
        [HttpPost("{id}/apply")]
        public async Task<IActionResult> ApplyToVacancy(int id, [FromQuery] ResponceDto dto)
        {
            var vacancy = await _vacancyService.GetByIdAsync(id);
            if (vacancy == null)
                return NotFound("Вакансия не найдена.");

            if (User.Identity is not { IsAuthenticated: true })
                return Unauthorized("Вы должны быть авторизованы для отклика.");

            // Найти работодателя через вакансию
            var employer = await _employerService.GetByIdAsync(vacancy.EmployerId);
            if (employer == null)
                return NotFound("Работодатель не найден для данной вакансии.");

            // Создаём отклик
            var application = await _responceService.CreateAsync(dto);

            string resumeInfo = string.Empty;
            if (dto.ResumeId.HasValue)
            {
                var resume = await _resumeService.GetResumeByIdAsync(dto.ResumeId.Value);
                if (resume != null)
                {
                    resumeInfo = $"\n\nРезюме:\n" +
                                 $"- Название: {resume.Title}\n" +
                                 $"- Образование: {resume.Education}\n" +
                                 $"- Опыт: {resume.Experience}\n";
                }
            }

            // Отправляем письмо работодателю
            var subject = "Новый отклик на вашу вакансию";
            var body = $"На вакансию '{vacancy.Title}' поступил новый отклик от пользователя с ID {dto.UserId}.\n" +
                       $"{(string.IsNullOrEmpty(dto.CoverLetter) ? "" : $"Сопроводительное письмо:\n{dto.CoverLetter}\n")}" +
                       $"{resumeInfo}";

            await _emailService.SendEmailAsync(employer.Email, subject, body);

            return Ok(new
            {
                Message = "Отклик успешно отправлен. Работодатель получил уведомление на почту.",
                ApplicationId = application.ResponceId
            });
        }
    }
}
