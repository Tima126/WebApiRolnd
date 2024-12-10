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
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerService _passanger;

        public PassengerController(IPassengerService context)
        {
            _passanger = context;
        }

        /// <summary>
        /// Получение всех пассажиров.
        /// </summary>
        /// <returns>Список всех пассажиров.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var passanger = await _passanger.GetAll();
            return Ok(passanger);
        }

        /// <summary>
        /// Получение пассажира по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пассажира.</param>
        /// <returns>Пассажир с указанным идентификатором.</returns>
        /// <response code="200">Возвращает пассажира.</response>
        /// <response code="400">Если пассажир не найден.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var passanger = await _passanger.GetById(id);
            if (passanger == null)
            {
                return NotFound();
            }
            return Ok(passanger);
        }

        /// <summary>
        /// Создание нового пассажира.
        /// </summary>
        /// <param name="Passenger">Данные для создания пассажира.</param>
        /// <returns>Созданный пассажир.</returns>
        /// <response code="200">Возвращает созданного пассажира.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreatePassenger req)
        {
            var passanger = req.Adapt<Passenger>();
            await _passanger.Create(passanger);
            return Ok();
        }

        /// <summary>
        /// Обновление существующего пассажира.
        /// </summary>
        /// <param name="Passenger">Данные для обновления пассажира.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если пассажир успешно обновлен.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CreatePassenger req)
        {
            var passanger = req.Adapt<Passenger>();
            await _passanger.Update(passanger);

            return NoContent();
        }
        /// <summary>
        /// Удаление пассажира по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пассажира.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если пассажир успешно удален.</response>
        /// <response code="400">Если пассажир не найден.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var passanger = await _passanger.GetById(id);

            if (passanger == null)
            {
                return BadRequest();
            }
            await _passanger.Delete(id);
            return Ok();
        }
    }
}