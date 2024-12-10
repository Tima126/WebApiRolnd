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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _review;

        public ReviewController(IReviewService context)
        {
            _review = context;
        }
        /// <summary>
        /// Получение всех отзывов.
        /// </summary>
        /// <returns>Список всех отзывов.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var review = await _review.GetAll();
            return Ok(review);
        }

        /// <summary>
        /// Получение отзыва по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор отзыва.</param>
        /// <returns>Отзыв с указанным идентификатором.</returns>
        /// <response code="200">Возвращает отзыв.</response>
        /// <response code="400">Если отзыв не найден.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _review.GetById(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        /// <summary>
        /// Создание нового отзыва.
        /// </summary>
        /// <param name="Review">Данные для создания отзыва.</param>
        /// <returns>Созданный отзыв.</returns>
        /// <response code="200">Возвращает созданный отзыв.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateReview req)
        {
            var review = req.Adapt<Review>();
            await _review.Create(review);
            return Ok();
        }

        /// <summary>
        /// Обновление существующего отзыва.
        /// </summary>
        /// <param name="Review">Данные для обновления отзыва.</param>
        /// <returns>Результат обновления.</returns>
        /// <response code="200">Если отзыв успешно обновлен.</response>
        [HttpPut]
        public async Task<IActionResult> Update(CreateReview req)
        {
            var review = req.Adapt<Review>();
            await _review.Update(review);

            return NoContent();
        }

        /// <summary>
        /// Удаление отзыва по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор отзыва.</param>
        /// <returns>Результат удаления.</returns>
        /// <response code="200">Если отзыв успешно удален.</response>
        /// <response code="400">Если отзыв не найден.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _review.GetById(id);

            if (review == null)
            {
                return BadRequest();
            }
            await _review.Delete(id);
            return Ok();
        }
    }
}