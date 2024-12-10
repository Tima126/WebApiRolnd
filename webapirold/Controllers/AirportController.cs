using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using WebApplication1.Contract;
using Domain.Interfaces.Service;
using System.Net;
using Mapster;

namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airport;

        public AirportController(IAirportService context)
        {
            _airport = context;
        }

        /// <summary>
        /// Получение всех аерапортов
        /// </summary>
        /// <returns>Список всех аерапортов</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var airport = await _airport.GetAll();
            return Ok(airport);
        }
        /// <summary>
        /// Получение аерапортов по идентиификатору.
        /// </summary>
        /// <param name="id">Индефикатор аерапорта.</param>
        /// <returns>аерапорт с указанным индефикатором.</returns>
        /// <response code="200">Возвращает аерапортов.</response>
        /// <response code="404">Если аерапорт не найден.</response>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var airport = await _airport.GetById(id);
            if (airport == null)
            {
                return NotFound();
            }
            return Ok(airport);
        }
        /// <summary>
        /// Создание нового аерапорта.
        /// </summary>
        /// <param name="Airport">Индефикатор аерапорта.</param>
        /// <returns>Созданный аерапорт.</returns>
        /// <response code="201">Возвращает созданный аерапорт.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateAirport req)
        {
            var airport = req.Adapt<Airport>();
            await _airport.Create(airport);
            return Ok();
        }
        /// <summary>
        /// Обновление существующего аерапорта.
        /// </summary>
        /// <param name="Airport">Данные для обновления аерапорта.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="204">Если аерапорт успешно обновлен.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CreateAirport req)
        {
            var airport = req.Adapt<Airport>();
            await _airport.Update(airport);

            return NoContent();
        }
        /// <summary>
        /// Удаление аерапорт по индентификатору.
        /// </summary>
        /// <param name="id">Индентификатору аерапорта.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если аерапорт успешно удален.</response>
        /// <response code="400">Если аерапорт не найден.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var airport = await _airport.GetById(id);

            if (airport == null)
            {
                return BadRequest();
            }
            await _airport.Delete(id);
            return Ok();
        }
    }
}