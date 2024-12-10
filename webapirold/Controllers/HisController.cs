
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Interfaces.Service;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApplication1.Contract;
using webapirold.Contract;
using Mapster;

namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HisController : ControllerBase
    {
        private readonly IHistorySevice _history;

        public HisController(IHistorySevice context)
        {
            _history = context;
        }

        /// <summary>
        /// Получение всех записей истории изменений.
        /// </summary>
        /// <returns>Список всех записей истории изменений.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var history = await _history.GetAll();
            return Ok(history);
        }

        /// <summary>
        /// Получение записи истории изменений по идентификатору пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Запись истории изменений с указанным идентификатором пользователя.</returns>
        /// <response code="200">Возвращает запись истории изменений.</response>
        /// <response code="400">Если запись истории изменений не найдена.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var history = await _history.GetById(id);
            if (history == null)
            {
                return NotFound();
            }
            return Ok(history);
        }

        /// <summary>
        /// Создание новой записи истории изменений.
        /// </summary>
        /// <param name="ChangeHistory">Данные для создания записи истории изменений.</param>
        /// <returns>Созданная запись истории изменений.</returns>
        /// <response code="200">Возвращает созданную запись истории изменений.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateChangesHistory req)
        {
            var history = req.Adapt<ChangeHistory>();
            await _history.Create(history);
            return Ok();
        }

        /// <summary>
        /// Обновление существующей записи истории изменений.
        /// </summary>
        /// <param name="ChangeHistory">Данные для обновления записи истории изменений.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если запись истории изменений успешно обновлена.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CreateAirport req)
        {
            var history = req.Adapt<ChangeHistory>();
            await _history.Update(history);

            return NoContent();
        }

        /// <summary>
        /// Удаление записи истории изменений по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор записи истории изменений.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если запись истории изменений успешно удалена.</response>
        /// <response code="400">Если запись истории изменений не найдена.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var history = await _history.GetById(id);

            if (history == null)
            {
                return BadRequest();
            }
            await _history.Delete(id);
            return Ok();
        }
    }
}