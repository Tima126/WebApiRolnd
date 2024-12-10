using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Interfaces.Service;
using WebApplication1.Contract;
using webapirold.Contract;
using Mapster;


namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class flightController : ControllerBase
    {
        private readonly IFlightService _flight;

        public flightController(IFlightService context)
        {
            _flight = context;
        }

        /// <summary>
        /// Получение всех полетов.
        /// </summary>
        /// <returns>Список всех полетов.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var airport = await _flight.GetAll();
            return Ok(airport);
        }

        /// <summary>
        /// Получение полета по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор полета.</param>
        /// <returns>Полет с указанным идентификатором.</returns>
        /// <response code="200">Возвращает полет.</response>
        /// <response code="400">Если полет не найден.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var flight = await _flight.GetById(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        /// <summary>
        /// Создание нового полета.
        /// </summary>
        /// <param name="Flight">Данные для создания полета.</param>
        /// <returns>Созданный полет.</returns>
        /// <response code="200">Возвращает созданный полет.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateAirport req)
        {
            var flight = req.Adapt<Flight>();
            await _flight.Create(flight);
            return Ok();
        }

        /// <summary>
        /// Обновление существующего полета.
        /// </summary>
        /// <param name="Flight">Данные для обновления полета.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если полет успешно обновлен.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CreateFlight req)
        {
            var flight = req.Adapt<Flight>();
            await _flight.Update(flight);

            return NoContent();
        }

        /// <summary>
        /// Удаление полета по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор полета.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если полет успешно удален.</response>
        /// <response code="400">Если полет не найден.</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var flight = await _flight.GetById(id);

            if (flight == null)
            {
                return BadRequest();
            }
            await _flight.Delete(id);
            return Ok();
        }
    }
}