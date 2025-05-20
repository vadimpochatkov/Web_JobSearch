using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;



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
        /// Откликнуться на вакансию (с прикреплением резюме и уведомлением работодателя).
        /// </summary>
        [HttpPost("{vacancyId}/apply")]
        public async Task<IActionResult> ApplyToVacancy(int vacancyId, [FromQuery] ResponseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                dto.VacancyId = vacancyId;

                var response = await responceService.CreateAsync(dto);
                if (response == null)
                    return StatusCode(500, "Не удалось создать отклик.");

                var vacancy = await vacancyService.GetByIdAsync(vacancyId);
                var employer = await employerService.GetByIdAsync(vacancy.EmployerId);
                var user = await userService.GetByIdAsync(dto.UserId);

                await emailService.SendVacancyApplicationEmailAsync(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при отклике на вакансию.");
                return StatusCode(500, "Внутренняя ошибка сервера.");
            }
        }
    }
}