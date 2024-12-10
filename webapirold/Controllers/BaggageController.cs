using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using WebApplication1.Contract;
using Domain.Interfaces.Service;
using Mapster;
namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaggageController : ControllerBase
    {
        private readonly IBaggageService _baggage;


        public BaggageController(IBaggageService context)
        {
            _baggage = context;
        }

        /// <summary>
        /// Получение всех багажа
        /// </summary>
        /// <returns>Список всех багажей</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var baggage = await _baggage.GetAll();
            return Ok(baggage);
        }
        /// <summary>
        /// Получение багажей по идентиификатору.
        /// </summary>
        /// <param name="id">Индефикатор багажа.</param>
        /// <returns>багаж с указанным индефикатором.</returns>
        /// <response code="200">Возвращает багаж.</response>
        /// <response code="404">Если багаж не найден.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var baggage = await _baggage.GetById(id);
            if (baggage == null)
            {
                return NotFound();
            }
            return Ok(baggage);
        }
        /// <summary>
        /// Создание нового багажа.
        /// </summary>
        /// <param name="Baggage">Индефикатор багажа.</param>
        /// <returns>Созданный багажа.</returns>
        /// <response code="201">Возвращает созданный багажа.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateBaggage req)
        {
            var baggage = req.Adapt<Baggage>();
            await _baggage.Create(baggage);
            return Ok();
        }
        /// <summary>
        /// Обновление существующего багажа.
        /// </summary>
        /// <param name="Airport">Данные для обновления багажа.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="204">Если багажа успешно обновлен.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CreateBaggage req)
        {
            var baggage = req.Adapt<Baggage>();
            await _baggage.Update(baggage);

            return NoContent();
        }
        /// <summary>
        /// Удаление багажа по индентификатору.
        /// </summary>
        /// <param name="id">Индентификатору багажа.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если багаж успешно удален.</response>
        /// <response code="400">Если багаж не найден.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var baggage = await _baggage.GetById(id);

            if (baggage == null)
            {
                return BadRequest();
            }
            await _baggage.Delete(id);
            return Ok();
        }
    }
}