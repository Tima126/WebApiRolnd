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
    public class CharterController : ControllerBase
    {
        private readonly ICharterService _charter;

        public CharterController(ICharterService context)
        {
            _charter = context;
        }

        /// <summary>
        /// Получение всех чартеров.
        /// </summary>
        /// <returns>Список всех чартеров.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var airport = await _charter.GetAll();
            return Ok(airport);
        }

        /// <summary>
        /// Получение чартера по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор чартера.</param>
        /// <returns>Чартер с указанным идентификатором.</returns>
        /// <response code="200">Возвращает чартер.</response>
        /// <response code="400">Если чартер не найден.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var charter = await _charter.GetById(id);
            if (charter == null)
            {
                return NotFound();
            }
            return Ok(charter);
        }
        /// <summary>
        /// Создание нового чартера.
        /// </summary>
        /// <param name="Charter">Данные для создания чартера.</param>
        /// <returns>Созданный чартер.</returns>
        /// <response code="200">Возвращает созданный чартер.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateCharter req)
        {
            var charter = req.Adapt<Charter>();
            await _charter.Create(charter);
            return Ok();
        }
        /// <summary>
        /// Обновление существующего чартера.
        /// </summary>
        /// <param name="Charter">Данные для обновления чартера.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если чартер успешно обновлен.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CreateCharter req)
        {
            var charter = req.Adapt<Charter>();
            await _charter.Update(charter);
            return Ok();
        }

        /// <summary>
        /// Удаление чартера по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор чартера.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если чартер успешно удален.</response>
        /// <response code="400">Если чартер не найден.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var charter = await _charter.GetById(id);

            if (charter == null)
            {
                return BadRequest();
            }
            await _charter.Delete(id);
            return Ok();
        }
    }
}