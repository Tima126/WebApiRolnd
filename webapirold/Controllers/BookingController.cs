using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;


namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        public RolandContext Context { get; }

        public BookingController(RolandContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Получение всех бронирований
        /// </summary>
        /// <returns>Список всех бронирования</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Booking> Booking = Context.Bookings.ToList();
            return Ok(Booking);
        }
        /// <summary>
        /// Получение бронирования по идентиификатору.
        /// </summary>
        /// <param name="id">Индефикатор бронирования.</param>
        /// <returns>бронирования с указанным индефикатором.</returns>
        /// <response code="200">Возвращает бронирования.</response>
        /// <response code="404">Если бронирования не найден.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Booking? Booking = Context.Bookings.Where(x => x.BookingId == id).FirstOrDefault();
            if (Booking == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(Booking);
        }
        /// <summary>
        /// Создание нового бронирования.
        /// </summary>
        /// <param name="Booking">Индефикатор бронирования.</param>
        /// <returns>Созданный бронирования.</returns>
        /// <response code="201">Возвращает созданный бронирования.</response>
        [HttpPost]
        public IActionResult Add(Booking Booking)
        {
            Context.Bookings.Add(Booking);
            Context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// Обновление существующего бронирования.
        /// </summary>
        /// <param name="Booking">Данные для обновления бронирования.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="204">Если бронирования успешно обновлен.</response>
        [HttpPut]
        public IActionResult Update(Booking Booking)
        {
            Context.Bookings.Update(Booking);
            Context.SaveChanges();
            return Ok(Booking);
        }
        /// <summary>
        /// Удаление бронирования по индентификатору.
        /// </summary>
        /// <param name="id">Индентификатору бронирования.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если бронирования успешно удален.</response>
        /// <response code="400">Если бронирования не найден.</response>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Booking? Booking = Context.Bookings.Where(x => x.BookingId == id).FirstOrDefault();
            Context.Bookings.Remove(Booking);
            Context.SaveChanges();
            return Ok();
        }
    }
}