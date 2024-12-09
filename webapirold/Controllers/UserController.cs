using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BusinessLogic.Sevices;
using WebApplication1.Contract;
using Mapster;
using Domain.Interfaces.Service;

namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns>Список всех пользователей</returns>
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            var user = await _userService.GetAll();
            return Ok(user);    
        }
        /// <summary>
        /// Получение пользователей по идентиификатору.
        /// </summary>
        /// <param name="id">Индефикатор пользователей.</param>
        /// <returns>пользователь с указанным индефикатором.</returns>
        /// <response code="200">Возвращает пользователей.</response>
        /// <response code="404">Если пользователь не найден.</response>
        [HttpGet("{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null) 
            {
                return NotFound();
            }
            return Ok(user);
        }
        /// <summary>
        /// Создание нового пользователя.
        /// </summary>
        /// <param name="user">Индефикатор пользователя.</param>
        /// <returns>Созданный пользователь.</returns>
        /// <response code="201">Возвращает созданный пользователь.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest req)
        {
            var user = req.Adapt<User>();
            await _userService.Create(user);
            return Ok(user);
        }
        /// <summary>
        /// Обновление существующего пользователя.
        /// </summary>
        /// <param name="user">Данные для обновления пользователья.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="204">Если пользователь успешно обновлен.</response>
        [HttpPut]
        public async Task <IActionResult> Update(CreateUserRequest req)
        {
            var user = req.Adapt<User>();
            await _userService.Update(user);
            return NoContent();
        }
        /// <summary>
        /// Удаление пользователя по индентификатору.
        /// </summary>
        /// <param name="id">Индентификатору пользователя.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если пользователь успешно удален.</response>
        /// <response code="400">Если пользователь не найден.</response>
        [HttpDelete]
        public async Task <IActionResult> Delete(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return BadRequest();
            }
            await _userService.Delete(id);
            return Ok();
        }
    }
}