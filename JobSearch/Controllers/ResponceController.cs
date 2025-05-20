using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ResponceController : ControllerBase
    {
        private readonly IResponseService _service;

        public ResponceController(IResponseService service)
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
        [HttpGet("{id:int}")]
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
        public async Task<IActionResult> Create([FromQuery] ResponseRequest newresponse)
        {
            var response = await _service.CreateAsync(newresponse);
            return Ok(response);
        }

        /// <summary>
        /// Обновить данные заявки.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromQuery] ResponseRequest updateresponse)
        {
            await _service.UpdateAsync(id, updateresponse);
            return NoContent();
        }

        /// <summary>
        /// Удалить заявку.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
