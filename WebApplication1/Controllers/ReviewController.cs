using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;


namespace SocNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public RolandContext Context { get; }

        public ReviewController(RolandContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Получение всех отзывов.
        /// </summary>
        /// <returns>Список всех отзывов.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Review> Review = Context.Reviews.ToList();
            return Ok(Review);
        }

        /// <summary>
        /// Получение отзыва по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор отзыва.</param>
        /// <returns>Отзыв с указанным идентификатором.</returns>
        /// <response code="200">Возвращает отзыв.</response>
        /// <response code="400">Если отзыв не найден.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Review? Review = Context.Reviews.Where(x => x.ReviewId == id).FirstOrDefault();
            if (Review == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(Review);
        }

        /// <summary>
        /// Создание нового отзыва.
        /// </summary>
        /// <param name="Review">Данные для создания отзыва.</param>
        /// <returns>Созданный отзыв.</returns>
        /// <response code="200">Возвращает созданный отзыв.</response>
        [HttpPost]
        public IActionResult Add(Review Review)
        {
            Context.Reviews.Add(Review);
            Context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Обновление существующего отзыва.
        /// </summary>
        /// <param name="Review">Данные для обновления отзыва.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если отзыв успешно обновлен.</response>
        [HttpPut]
        public IActionResult Update(Review Review)
        {
            Context.Reviews.Update(Review);
            Context.SaveChanges();
            return Ok(Review);
        }

        /// <summary>
        /// Удаление отзыва по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор отзыва.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если отзыв успешно удален.</response>
        /// <response code="400">Если отзыв не найден.</response>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Review? Review = Context.Reviews.Where(x => x.ReviewId == id).FirstOrDefault();
            Context.Reviews.Remove(Review);
            Context.SaveChanges();
            return Ok();
        }
    }
}