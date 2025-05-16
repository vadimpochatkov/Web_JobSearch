using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobSearch.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployersController : ControllerBase
    {
        private readonly IEmployerService _service;

        public EmployersController(IEmployerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Создать нового работодателя.
        /// </summary>
        /// <param name="dto"></param>
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] EmployerDto dto)
        {
            var employer = await _service.CreateAsync(dto);
            return Ok(employer); 
        }

        /// <summary>
        /// Получить работодателя по ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var employer = await _service.GetByIdAsync(id);
            if (employer == null)
                return NotFound();
            return Ok(employer);
        }

        /// <summary>
        /// Обновить информацию о работодателе.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromQuery] EmployerDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Удалить работодателя.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
