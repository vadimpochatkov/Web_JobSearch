using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;
using System.Net.Mail;
using System.Text;
using JobSearch.Domains.Services.UseCases;


namespace JobSearch.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VacanciesController(IVacancyService vacancyService,
            IResponseService responceService,
            IEmailService emailService,
            IResumeService resumeService,
            IEmployerService employerService,
            ILogger<VacanciesController> logger) : ControllerBase
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
        public async Task<IActionResult> ApplyToVacancy(int id, [FromBody] ResponseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vacancy = await vacancyService.GetByIdAsync(id);
            if (vacancy == null)
                return NotFound("Вакансия не найдена.");

            var employer = await employerService.GetByIdAsync(vacancy.EmployerId);
            if (employer == null || string.IsNullOrWhiteSpace(employer.Email))
                return NotFound("Работодатель не найден или email не указан.");

            var application = await responceService.CreateAsync(dto);
            if (application == null)
                return StatusCode(500, "Ошибка при создании отклика.");

            try
            {
                await emailService.SendVacancyApplicationEmailAsync(
                    employer.Email,
                    vacancy.Title,
                    dto.UserId,
                    dto.CoverLetter,
                    dto.ResumeId
                );
                return Ok(new { Message = "Отклик успешно отправлен!" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при обработке отклика");
                return StatusCode(500, "Внутренняя ошибка сервера.");
            }
        }
    }
}