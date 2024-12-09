using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;

namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        public RolandContext Context { get; }

        public AirportController(RolandContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получение всех аерапортов
        /// </summary>
        /// <returns>Список всех аерапортов</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Airport> Airport = Context.Airports.ToList();
            return Ok(Airport);
        }
        /// <summary>
        /// Получение аерапортов по идентиификатору.
        /// </summary>
        /// <param name="id">Индефикатор аерапорта.</param>
        /// <returns>аерапорт с указанным индефикатором.</returns>
        /// <response code="200">Возвращает аерапортов.</response>
        /// <response code="404">Если аерапорт не найден.</response>

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Airport? Airport = Context.Airports.Where(x => x.AirportId == id).FirstOrDefault();
            if (Airport == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(Airport);
        }
        /// <summary>
        /// Создание нового аерапорта.
        /// </summary>
        /// <param name="Airport">Индефикатор аерапорта.</param>
        /// <returns>Созданный аерапорт.</returns>
        /// <response code="201">Возвращает созданный аерапорт.</response>
        [HttpPost]
        public IActionResult Add(Airport Airport)
        {
            Context.Airports.Add(Airport);
            Context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// Обновление существующего аерапорта.
        /// </summary>
        /// <param name="Airport">Данные для обновления аерапорта.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="204">Если аерапорт успешно обновлен.</response>
        [HttpPut]
        public IActionResult Update(Airport Airport)
        {
            Context.Airports.Update(Airport);
            Context.SaveChanges();
            return Ok(Airport);
        }
        /// <summary>
        /// Удаление аерапорт по индентификатору.
        /// </summary>
        /// <param name="id">Индентификатору аерапорта.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если аерапорт успешно удален.</response>
        /// <response code="400">Если аерапорт не найден.</response>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Airport? Airport = Context.Airports.Where(x => x.AirportId == id).FirstOrDefault();
            Context.Airports.Remove(Airport);
            Context.SaveChanges();
            return Ok();
        }
    }
}