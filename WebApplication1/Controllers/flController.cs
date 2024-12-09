using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;


namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class flController : ControllerBase
    {
        public RolandContext Context { get; }

        public flController(RolandContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получение всех полетов.
        /// </summary>
        /// <returns>Список всех полетов.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Flight> Flight = Context.Flights.ToList();
            return Ok(Flight);
        }

        /// <summary>
        /// Получение полета по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор полета.</param>
        /// <returns>Полет с указанным идентификатором.</returns>
        /// <response code="200">Возвращает полет.</response>
        /// <response code="400">Если полет не найден.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Flight? Flight = Context.Flights.Where(x => x.FlightId == id).FirstOrDefault();
            if (Flight == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(Flight);
        }

        /// <summary>
        /// Создание нового полета.
        /// </summary>
        /// <param name="Flight">Данные для создания полета.</param>
        /// <returns>Созданный полет.</returns>
        /// <response code="200">Возвращает созданный полет.</response>
        [HttpPost]
        public IActionResult Add(Flight Flight)
        {
            Context.Flights.Add(Flight);
            Context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Обновление существующего полета.
        /// </summary>
        /// <param name="Flight">Данные для обновления полета.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если полет успешно обновлен.</response>
        [HttpPut]
        public IActionResult Update(Flight Flight)
        {
            Context.Flights.Update(Flight);
            Context.SaveChanges();
            return Ok(Flight);
        }

        /// <summary>
        /// Удаление полета по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор полета.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если полет успешно удален.</response>
        /// <response code="400">Если полет не найден.</response>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Flight? Flight = Context.Flights.Where(x => x.FlightId == id).FirstOrDefault();
            Context.Flights.Remove(Flight);
            Context.SaveChanges();
            return Ok();
        }
    }
}