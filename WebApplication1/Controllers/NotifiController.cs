using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;


namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifiController : ControllerBase
    {
        public RolandContext Context { get; }

        public NotifiController(RolandContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получение всех уведомлений.
        /// </summary>
        /// <returns>Список всех уведомлений.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Notification> Notification = Context.Notifications.ToList();
            return Ok(Notification);
        }

        /// <summary>
        /// Получение уведомления по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор уведомления.</param>
        /// <returns>Уведомление с указанным идентификатором.</returns>
        /// <response code="200">Возвращает уведомление.</response>
        /// <response code="400">Если уведомление не найдено.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Notification? Notification = Context.Notifications.Where(x => x.NotificationId == id).FirstOrDefault();
            if (Notification == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(Notification);
        }

        /// <summary>
        /// Создание нового уведомления.
        /// </summary>
        /// <param name="Notification">Данные для создания уведомления.</param>
        /// <returns>Созданное уведомление.</returns>
        /// <response code="200">Возвращает созданное уведомление.</response>
        [HttpPost]
        public IActionResult Add(Notification Notification)
        {
            Context.Notifications.Add(Notification);
            Context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Обновление существующего уведомления.
        /// </summary>
        /// <param name="Notification">Данные для обновления уведомления.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если уведомление успешно обновлено.</response>
        [HttpPut]
        public IActionResult Update(Notification Notification)
        {
            Context.Notifications.Update(Notification);
            Context.SaveChanges();
            return Ok(Notification);
        }

        /// <summary>
        /// Удаление уведомления по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор уведомления.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если уведомление успешно удалено.</response>
        /// <response code="400">Если уведомление не найдено.</response>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Notification? Notification = Context.Notifications.Where(x => x.NotificationId == id).FirstOrDefault();
            Context.Notifications.Remove(Notification);
            Context.SaveChanges();
            return Ok();
        }
    }
}