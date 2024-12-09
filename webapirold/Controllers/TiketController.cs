using Microsoft.AspNetCore.Http;
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
    public class TiketController : ControllerBase
    {
        private ITicketService _ticketService;

        public TiketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        /// <summary>
        /// Получение всех билетов
        /// </summary>
        /// <returns>Список всех билетов</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ticket = await _ticketService.GetAll();
            return Ok(ticket);
        }
        /// <summary>
        /// Получение билета по идентиификатору.
        /// </summary>
        /// <param name="id">Индефикатор билета.</param>
        /// <returns>билета с указанным индефикатором.</returns>
        /// <response code="200">Возвращает билета.</response>
        /// <response code="404">Если билета не найден.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _ticketService.GetById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }
        /// <summary>
        /// Создание нового билета.
        /// </summary>
        /// <param name="user">Индефикатор билета.</param>
        /// <returns>Созданный билета.</returns>
        /// <response code="201">Возвращает созданный билета.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateTicket req)
        {
            var ticket = req.Adapt<Ticket>();
            await _ticketService.Create(ticket);
            return Ok(ticket);
        }
        /// <summary>
        /// Обновление существующего билеты.
        /// </summary>
        /// <param name="user">Данные для обновления билеты.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="204">Если билеты успешно обновлен.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CreateTicket req)
        {
            var ticket = req.Adapt<Ticket>();
            await _ticketService.Update(ticket);
            return NoContent();
        }
        /// <summary>
        /// Удаление билеты по индентификатору.
        /// </summary>
        /// <param name="id">Индентификатору билеты.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если билеты успешно удален.</response>
        /// <response code="400">Если билеты не найден.</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketService.GetById(id);
            if (ticket == null)
            {
                return BadRequest();
            }
            await _ticketService.Delete(id);
            return Ok();
        }
    }
}