using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Domain.Models;
using WebApplication1.Contract;
using Mapster;
using Domain.Interfaces.Service;
using webapirold.Contract;

namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialController : ControllerBase
    {
        private readonly ISpecialService _special;

        public SpecialController(ISpecialService context)
        {
            _special = context;
        }
        /// <summary>
        /// Получение всех данных
        /// </summary>
        /// <returns>Список всех данных</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var special = await _special.GetAll();
            return Ok(special);
        }
        /// <summary>
        /// Получение данных по идентиификатору.
        /// </summary>
        /// <param name="id">Индефикатор данных.</param>
        /// <returns>данных с указанным индефикатором.</returns>
        /// <response code="200">Возвращает данных.</response>
        /// <response code="404">Если данных не найден.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var special = await _special.GetById(id);
            if (special == null)
            {
                return NotFound();
            }
            return Ok(special);
        }
        /// <summary>
        /// Создание новых данных.
        /// </summary>
        /// <param name="user">Индефикатор данных.</param>
        /// <returns>Созданный данных.</returns>
        /// <response code="201">Возвращает созданный данных.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateSpecial req)
        {
            var special = req.Adapt<Special>();
            await _special.Create(special);
            return Ok();
        }
        /// <summary>
        /// Обновление существующего данных.
        /// </summary>
        /// <param name="user">Данные для обновления .</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="204">Если данные успешно обновлен.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CreateSpecial req)
        {
            var special = req.Adapt<Special>();
            await _special.Update(special);

            return NoContent();
        }
        /// <summary>
        /// Удаление данных по индентификатору.
        /// </summary>
        /// <param name="id">Индентификатору данных.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если данных успешно удален.</response>
        /// <response code="400">Если данных не найден.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var special = await _special.GetById(id);

            if (special == null)
            {
                return BadRequest();
            }
            await _special.Delete(id);
            return Ok();
        }
    }
}