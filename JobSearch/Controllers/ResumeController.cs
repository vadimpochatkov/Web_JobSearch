using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _resumeService;

        public ResumeController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        /// <summary>
        /// Создание нового резюме.
        /// </summary>
        /// <param name="dto">Данные для создания резюме.</param>
        /// <returns>Созданное резюме.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromQuery] ResumeDto dto)
        {
            var resume = await _resumeService.CreateResumeAsync(dto);
            return Ok(resume);
        }

        /// <summary>
        /// Получение всех резюме.
        /// </summary>
        /// <returns>Список всех резюме.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var resumes = await _resumeService.GetAllResumesAsync();
            return Ok(resumes);
        }

        /// <summary>
        /// Получение резюме по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор резюме.</param>
        /// <returns>Резюме, если найдено.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var resume = await _resumeService.GetResumeByIdAsync(id);
            if (resume == null)
                return NotFound();

            return Ok(resume);
        }

        /// <summary>
        /// Обновление резюме по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор резюме для обновления.</param>
        /// <param name="dto">Новые данные резюме.</param>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromQuery] ResumeDto dto)
        {
            await _resumeService.UpdateResumeAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Удаление резюме по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор резюме для удаления.</param>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _resumeService.DeleteResumeAsync(id);
            return NoContent();
        }
    }
}
