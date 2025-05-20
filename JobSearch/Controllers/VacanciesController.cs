using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;
using JobSearch.Domains.Entities;
using JobSearch.Domains.Services.UseCases;



namespace JobSearch.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VacanciesController(IVacancyService vacancyService,
            IResponseService responceService,
            IEmailService emailService,
            IUserService userService,
            IResumeService resumeService,
            IEmployerService employerService,
            ILogger<VacanciesController> logger) : ControllerBase
    {


        /// <summary>
        /// Создать новую вакансию.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] VacancyRequest dto)
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
        public async Task<IActionResult> Update(int id, [FromQuery] VacancyRequest dto)
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
        /// Отправить письмо работодателю по уже созданному отклику.
        /// </summary>
        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmailForResponse([FromQuery] ResponseDto dto)
        {
            try
            {
                var response = await responceService.GetByIdAsync(dto.ResponseId);

                var vacancy = await vacancyService.GetByIdAsync(dto.VacancyId);
                if (vacancy == null)
                    return NotFound($"Вакансия с ID {dto.VacancyId} не найдена.");

                var employer = await employerService.GetByIdAsync(dto.EmployerId);
                if (employer == null)
                    return NotFound($"Работодатель с ID {dto.EmployerId} не найден.");

                var user = await userService.GetByIdAsync(dto.UserId);
                if (user == null)
                    return NotFound($"Пользователь с ID {dto.UserId} не найден.");

                Resume? resume = null;
                if (dto.ResumeId.HasValue)
                    resume = await resumeService.GetResumeByIdAsync(dto.ResumeId.Value);

                await emailService.SendVacancyApplicationEmailAsync(response, vacancy, employer, user, resume);

                return Ok("Письмо успешно отправлено.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при отправке письма по отклику.");
                return StatusCode(500, "Внутренняя ошибка сервера.");
            }
        }
    }
}