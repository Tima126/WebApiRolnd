using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Domain.Models;

namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharterController : ControllerBase
    {
        public RolandContext Context { get; }

        public CharterController(RolandContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получение всех чартеров.
        /// </summary>
        /// <returns>Список всех чартеров.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Charter> Charter = Context.Charters.ToList();
            return Ok(Charter);
        }

        /// <summary>
        /// Получение чартера по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор чартера.</param>
        /// <returns>Чартер с указанным идентификатором.</returns>
        /// <response code="200">Возвращает чартер.</response>
        /// <response code="400">Если чартер не найден.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Charter? Charter = Context.Charters.Where(x => x.CharterId == id).FirstOrDefault();
            if (Charter == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(Charter);
        }

        /// <summary>
        /// Создание нового чартера.
        /// </summary>
        /// <param name="Charter">Данные для создания чартера.</param>
        /// <returns>Созданный чартер.</returns>
        /// <response code="200">Возвращает созданный чартер.</response>
        [HttpPost]
        public IActionResult Add(Charter Charter)
        {
            Context.Charters.Add(Charter);
            Context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Обновление существующего чартера.
        /// </summary>
        /// <param name="Charter">Данные для обновления чартера.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если чартер успешно обновлен.</response>
        [HttpPut]
        public IActionResult Update(Charter Charter)
        {
            Context.Charters.Update(Charter);
            Context.SaveChanges();
            return Ok(Charter);
        }

        /// <summary>
        /// Удаление чартера по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор чартера.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если чартер успешно удален.</response>
        /// <response code="400">Если чартер не найден.</response>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Charter? Charter = Context.Charters.Where(x => x.CharterId == id).FirstOrDefault();
            Context.Charters.Remove(Charter);
            Context.SaveChanges();
            return Ok();
        }
    }
}