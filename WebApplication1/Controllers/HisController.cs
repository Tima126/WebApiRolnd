
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;

namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HisController : ControllerBase
    {
        public RolandContext Context { get; }

        public HisController(RolandContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получение всех записей истории изменений.
        /// </summary>
        /// <returns>Список всех записей истории изменений.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ChangeHistory> ChangeHistory = Context.ChangeHistories.ToList();
            return Ok(ChangeHistory);
        }

        /// <summary>
        /// Получение записи истории изменений по идентификатору пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Запись истории изменений с указанным идентификатором пользователя.</returns>
        /// <response code="200">Возвращает запись истории изменений.</response>
        /// <response code="400">Если запись истории изменений не найдена.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ChangeHistory? ChangeHistory = Context.ChangeHistories.Where(x => x.UserId == id).FirstOrDefault();
            if (ChangeHistory == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(ChangeHistory);
        }

        /// <summary>
        /// Создание новой записи истории изменений.
        /// </summary>
        /// <param name="ChangeHistory">Данные для создания записи истории изменений.</param>
        /// <returns>Созданная запись истории изменений.</returns>
        /// <response code="200">Возвращает созданную запись истории изменений.</response>
        [HttpPost]
        public IActionResult Add(ChangeHistory ChangeHistory)
        {
            Context.ChangeHistories.Add(ChangeHistory);
            Context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Обновление существующей записи истории изменений.
        /// </summary>
        /// <param name="ChangeHistory">Данные для обновления записи истории изменений.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если запись истории изменений успешно обновлена.</response>
        [HttpPut]
        public IActionResult Update(ChangeHistory ChangeHistory)
        {
            Context.ChangeHistories.Update(ChangeHistory);
            Context.SaveChanges();
            return Ok(ChangeHistory);
        }

        /// <summary>
        /// Удаление записи истории изменений по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор записи истории изменений.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если запись истории изменений успешно удалена.</response>
        /// <response code="400">Если запись истории изменений не найдена.</response>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            ChangeHistory? ChangeHistory = Context.ChangeHistories.Where(x => x.ChangeId == id).FirstOrDefault();
            Context.ChangeHistories.Remove(ChangeHistory);
            Context.SaveChanges();
            return Ok();
        }
    }
}