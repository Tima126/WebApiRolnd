using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Interfaces.Service;
using WebApplication1.Contract;
using Mapster;


namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _booking;

        public BookingController(IBookingService context)
        {
            _booking = context;
        }
        /// <summary>
        /// Получение всех бронирований
        /// </summary>
        /// <returns>Список всех бронирования</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var booking = await _booking.GetAll();
            return Ok(booking);
        }
        /// <summary>
        /// Получение бронирования по идентиификатору.
        /// </summary>
        /// <param name="id">Индефикатор бронирования.</param>
        /// <returns>бронирования с указанным индефикатором.</returns>
        /// <response code="200">Возвращает бронирования.</response>
        /// <response code="404">Если бронирования не найден.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _booking.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }
        /// <summary>
        /// Создание нового бронирования.
        /// </summary>
        /// <param name="Booking">Индефикатор бронирования.</param>
        /// <returns>Созданный бронирования.</returns>
        /// <response code="201">Возвращает созданный бронирования.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateBooking req)
        {
            var booking = req.Adapt<Booking>();
            await _booking.Create(booking);
            return Ok();
        }

        /// <summary>
        /// Обновление существующего бронирования.
        /// </summary>
        /// <param name="Booking">Данные для обновления бронирования.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="204">Если бронирования успешно обновлен.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CreateBooking req)
        {
            var booking = req.Adapt<Booking>();
            await _booking.Update(booking);

            return NoContent();
        }
        /// <summary>
        /// Удаление бронирования по индентификатору.
        /// </summary>
        /// <param name="id">Индентификатору бронирования.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если бронирования успешно удален.</response>
        /// <response code="400">Если бронирования не найден.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _booking.GetById(id);

            if (booking == null)
            {
                return BadRequest();
            }
            await _booking.Delete(id);
            return Ok();
        }
    }
}