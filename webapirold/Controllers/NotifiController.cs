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
    public class NotifiController : ControllerBase
    {
        private readonly INotificationService _notification;

        public NotifiController(INotificationService context)
        {
            _notification = context;
        }

        /// <summary>
        /// Получение всех уведомлений.
        /// </summary>
        /// <returns>Список всех уведомлений.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notification = await _notification.GetAll();
            return Ok(notification);
        }

        /// <summary>
        /// Получение уведомления по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор уведомления.</param>
        /// <returns>Уведомление с указанным идентификатором.</returns>
        /// <response code="200">Возвращает уведомление.</response>
        /// <response code="400">Если уведомление не найдено.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var notification = await _notification.GetById(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        /// <summary>
        /// Создание нового уведомления.
        /// </summary>
        /// <param name="Notification">Данные для создания уведомления.</param>
        /// <returns>Созданное уведомление.</returns>
        /// <response code="200">Возвращает созданное уведомление.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateNotification req)
        {
            var notification = req.Adapt<Notification>();
            await _notification.Create(notification);
            return Ok();
        }

        /// <summary>
        /// Обновление существующего уведомления.
        /// </summary>
        /// <param name="Notification">Данные для обновления уведомления.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если уведомление успешно обновлено.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CreateNotification req)
        {
            var notification = req.Adapt<Notification>();
            await _notification.Update(notification);

            return NoContent();
        }

        /// <summary>
        /// Удаление уведомления по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор уведомления.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если уведомление успешно удалено.</response>
        /// <response code="400">Если уведомление не найдено.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var notification = await _notification.GetById(id);

            if (notification == null)
            {
                return BadRequest();
            }
            await _notification.Delete(id);
            return Ok();
        }
    }
}