using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;


namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        public RolandContext Context { get; }

        public PassengerController(RolandContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получение всех пассажиров.
        /// </summary>
        /// <returns>Список всех пассажиров.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Passenger> Passenger = Context.Passengers.ToList();
            return Ok(Passenger);
        }

        /// <summary>
        /// Получение пассажира по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пассажира.</param>
        /// <returns>Пассажир с указанным идентификатором.</returns>
        /// <response code="200">Возвращает пассажира.</response>
        /// <response code="400">Если пассажир не найден.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Passenger? Passenger = Context.Passengers.Where(x => x.PassengerId == id).FirstOrDefault();
            if (Passenger == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(Passenger);
        }

        /// <summary>
        /// Создание нового пассажира.
        /// </summary>
        /// <param name="Passenger">Данные для создания пассажира.</param>
        /// <returns>Созданный пассажир.</returns>
        /// <response code="200">Возвращает созданного пассажира.</response>
        [HttpPost]
        public IActionResult Add(Passenger Passenger)
        {
            Context.Passengers.Add(Passenger);
            Context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Обновление существующего пассажира.
        /// </summary>
        /// <param name="Passenger">Данные для обновления пассажира.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если пассажир успешно обновлен.</response>
        [HttpPut]
        public IActionResult Update(Passenger Passenger)
        {
            Context.Passengers.Update(Passenger);
            Context.SaveChanges();
            return Ok(Passenger);
        }

        /// <summary>
        /// Удаление пассажира по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пассажира.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если пассажир успешно удален.</response>
        /// <response code="400">Если пассажир не найден.</response>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Passenger? Passenger = Context.Passengers.Where(x => x.PassengerId == id).FirstOrDefault();
            Context.Passengers.Remove(Passenger);
            Context.SaveChanges();
            return Ok();
        }
    }
}