using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
 
namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaggageController : ControllerBase
    {
        public RolandContext Context { get; }

        public BaggageController(RolandContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Получение всех багажа
        /// </summary>
        /// <returns>Список всех багажей</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Baggage> Baggage = Context.Baggages.ToList();
            return Ok(Baggage);
        }
        /// <summary>
        /// Получение багажей по идентиификатору.
        /// </summary>
        /// <param name="id">Индефикатор багажа.</param>
        /// <returns>багаж с указанным индефикатором.</returns>
        /// <response code="200">Возвращает багаж.</response>
        /// <response code="404">Если багаж не найден.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Baggage? Baggage = Context.Baggages.Where(x => x.BaggageId == id).FirstOrDefault();
            if (Baggage == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(Baggage);
        }
        /// <summary>
        /// Создание нового багажа.
        /// </summary>
        /// <param name="Baggage">Индефикатор багажа.</param>
        /// <returns>Созданный багажа.</returns>
        /// <response code="201">Возвращает созданный багажа.</response>
        [HttpPost]
        public IActionResult Add(Baggage Baggage)
        {
            Context.Baggages.Add(Baggage);
            Context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// Обновление существующего багажа.
        /// </summary>
        /// <param name="Airport">Данные для обновления багажа.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="204">Если багажа успешно обновлен.</response>
        [HttpPut]
        public IActionResult Update(Baggage Baggage)
        {
            Context.Baggages.Update(Baggage);
            Context.SaveChanges();
            return Ok(Baggage);
        }
        /// <summary>
        /// Удаление багажа по индентификатору.
        /// </summary>
        /// <param name="id">Индентификатору багажа.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если багаж успешно удален.</response>
        /// <response code="400">Если багаж не найден.</response>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Baggage? Baggage = Context.Baggages.Where(x => x.BaggageId == id).FirstOrDefault();
            Context.Baggages.Remove(Baggage);
            Context.SaveChanges();
            return Ok();
        }
    }
}