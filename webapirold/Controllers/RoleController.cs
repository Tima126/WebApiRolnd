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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _role;

        public RoleController(IRoleService context)
        {
            _role = context;
        }

        /// <summary>
        /// Получение всех ролей.
        /// </summary>
        /// <returns>Список всех ролей.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var role = await _role.GetAll();
            return Ok(role);
        }

        /// <summary>
        /// Получение роли по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор роли.</param>
        /// <returns>Роль с указанным идентификатором.</returns>
        /// <response code="200">Возвращает роль.</response>
        /// <response code="400">Если роль не найдена.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _role.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        /// <summary>
        /// Создание новой роли.
        /// </summary>
        /// <param name="Role">Данные для создания роли.</param>
        /// <returns>Созданная роль.</returns>
        /// <response code="200">Возвращает созданную роль.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateRole req)
        {
            var role = req.Adapt<Role>();
            await _role.Create(role);
            return Ok();
        }

        /// <summary>
        /// Обновление существующей роли.
        /// </summary>
        /// <param name="Role">Данные для обновления роли.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если роль успешно обновлена.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CreateRole req)
        {
            var role = req.Adapt<Role>();
            await _role.Update(role);
            return Ok();


        }

        /// <summary>
        /// Удаление роли по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор роли.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если роль успешно удалена.</response>
        /// <response code="400">Если роль не найдена.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _role.GetById(id);

            if (role == null)
            {
                return BadRequest();
            }
            await _role.Delete(id);
            return Ok();
        }
    }
}