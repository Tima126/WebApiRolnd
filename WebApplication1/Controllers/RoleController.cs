using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Domain.Models;

namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public RolandContext Context { get; }

        public RoleController(RolandContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получение всех ролей.
        /// </summary>
        /// <returns>Список всех ролей.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Role> Role = Context.Roles.ToList();
            return Ok(Role);
        }

        /// <summary>
        /// Получение роли по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор роли.</param>
        /// <returns>Роль с указанным идентификатором.</returns>
        /// <response code="200">Возвращает роль.</response>
        /// <response code="400">Если роль не найдена.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Role? Role = Context.Roles.Where(x => x.RoleId == id).FirstOrDefault();
            if (Role == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(Role);
        }

        /// <summary>
        /// Создание новой роли.
        /// </summary>
        /// <param name="Role">Данные для создания роли.</param>
        /// <returns>Созданная роль.</returns>
        /// <response code="200">Возвращает созданную роль.</response>
        [HttpPost]
        public IActionResult Add(Role Role)
        {
            Context.Roles.Add(Role);
            Context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Обновление существующей роли.
        /// </summary>
        /// <param name="Role">Данные для обновления роли.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если роль успешно обновлена.</response>
        [HttpPut]
        public IActionResult Update(Role Role)
        {
            Context.Roles.Update(Role);
            Context.SaveChanges();
            return Ok(Role);
        }

        /// <summary>
        /// Удаление роли по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор роли.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если роль успешно удалена.</response>
        /// <response code="400">Если роль не найдена.</response>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Role? Role = Context.Roles.Where(x => x.RoleId == id).FirstOrDefault();
            Context.Roles.Remove(Role);
            Context.SaveChanges();
            return Ok();
        }
    }
}