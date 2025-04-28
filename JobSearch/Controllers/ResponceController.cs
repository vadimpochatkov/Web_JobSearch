using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.Services.UseCases;
using JobSearch.Domains.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ResponceController : ControllerBase
    {
        private readonly IResponceService _service;

        public ResponceController(IResponceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Получить все заявки на вакансии.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var responses = await _service.GetAllAsync();
            return Ok(responses);
        }

        /// <summary>
        /// Получить заявку по ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetByIdAsync(id);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// Создать новую заявку на вакансию.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] ResponceDto dto)
        {
            var response = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = response.ResponceId }, response);
        }

        /// <summary>
        /// Обновить данные заявки.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromQuery] ResponceDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Удалить заявку.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
