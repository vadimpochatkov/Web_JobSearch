using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VacanciesController(IVacancyService vacancyService,
            IResponceService responceService,
            IEmailService emailService,
            IResumeService resumeService,
            IEmployerService employerService) : ControllerBase
    {
       

        /// <summary>
        /// Создать новую вакансию.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] VacancyDto dto)
        {
            var vacancy = await vacancyService.CreateAsync(dto);
            return Ok(vacancy);
        }

        /// <summary>
        /// Получить все вакансии.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vacancies = await vacancyService.GetAllAsync();
            return Ok(vacancies);
        }

        /// <summary>
        /// Получить вакансию по ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vacancy = await vacancyService.GetByIdAsync(id);
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
            await vacancyService.UpdateAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Удалить вакансию.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await vacancyService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Откликнуться на вакансию (с прикреплением резюме и уведомлением работодателя).
        /// </summary>
        [Authorize]
        [HttpPost("{id}/apply")]
        public async Task<IActionResult> ApplyToVacancy(int id, [FromQuery] ResponceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Переданы некорректные данные.");

            var vacancy = await vacancyService.GetByIdAsync(id);
            if (vacancy == null)
                return NotFound("Вакансия не найдена.");

            var employer = await employerService.GetByIdAsync(vacancy.EmployerId);
            if (employer == null || string.IsNullOrWhiteSpace(employer.Email))
                return NotFound("Не удалось найти почту работодателя.");

            var application = await responceService.CreateAsync(dto);

            string resumeInfo = string.Empty;
            if (dto.ResumeId.HasValue)
            {
                var resume = await resumeService.GetResumeByIdAsync(dto.ResumeId.Value);
                if (resume != null)
                {
                    resumeInfo = $"\n\nРезюме:\n" +
                                 $"- Название: {resume.Title}\n" +
                                 $"- Образование: {resume.Education}\n" +
                                 $"- Опыт: {resume.Experience}\n";
                }
            }

            var subject = "Новый отклик на вашу вакансию";
            var body = $"На вакансию '{vacancy.Title}' поступил отклик от пользователя с ID {dto.UserId}.\n" +
                       $"{(string.IsNullOrEmpty(dto.CoverLetter) ? "" : $"Сопроводительное письмо:\n{dto.CoverLetter}\n")}" +
                       $"{resumeInfo}";

            try
            {
                await emailService.SendEmailAsync(employer.Email, subject, body);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при отправке письма: {ex.Message}");
            }

            return Ok(new
            {
                Message = "Отклик успешно отправлен. Работодатель получил уведомление на почту.",
                ApplicationId = application.ResponceId
            });
        }

    }
}
