using FlatRockProject.Application.Models;
using FlatRockProject.Application.Services.Quiz;
using FlatRockProject.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FlatRockProject.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizQuestionService _quizQuestionService;

        public QuizController(IQuizQuestionService quizQuestionService)
        {
            _quizQuestionService = quizQuestionService;
        }
        [HttpGet]
        [Route("last-binary-choice-question")]
        [ProducesResponseType(typeof(BinaryQuestion), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetLastBinaryChoiceQuestion()
        {
            return Ok(await _quizQuestionService.GetLastBinaryChoiceQuestionAsync());
        }

        [HttpGet]
        [Route("binary-choice-question")]
        [ProducesResponseType(typeof(BinaryChoiceQuestionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBinaryChoiceQuestion([FromQuery] int initialId)
        {
            return Ok(await _quizQuestionService.GetBinaryChoiceQuestionAsync(initialId));
        }
        [HttpGet]
        [Route("last-multiple-choice-question")]
        [ProducesResponseType(typeof(MultipleChoiceQuestion), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetLastMultipleChoiceQuestion()
        {
            return Ok(await _quizQuestionService.GetLastMultipleChoiceQuestionAsync());
        }
        [HttpGet]
        [Route("multiple-choice-question")]
        [ProducesResponseType(typeof(MultipleChoiceQuizQuestionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMultipleChoiceQuestion([FromQuery] int initialId)
        {
            return Ok(await _quizQuestionService.GetMultipleChoiceQuizQuestionAsync(initialId));
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetBinaryChoiceQuestionsAsync(int initialId = 0)
        {
            return Ok(await _quizQuestionService.GetBinaryChoiceQuestionsAsync(initialId));
        }
        [HttpGet("listOfMultiple")]
        public async Task<IActionResult> GetMultipleChoiceQuizQuestionsAsync(int initialId = 0)
        {
            return Ok(await _quizQuestionService.GetMultipleChoiceQuizQuestionsAsync(initialId));
        }
        [HttpPost("AddAuthor")]
        public async Task<IActionResult> AddAuthor(string name)
        {
            return Ok(await _quizQuestionService.AddAuthor(name));
        }
        [HttpPost("AddQuote")]
        public async Task<IActionResult> AddQuote(string content, string authorName)
        {
            return Ok(await _quizQuestionService.AddQuote(content, authorName));
        }
        [HttpPost("AddMultipleChoiceQuestion")]
        public async Task<IActionResult> AddMultipleChoiceQuestion(string quoteContent,
            string correctAuthorName)
        {
            return Ok(await _quizQuestionService.AddMultipleChoiceQuestion(quoteContent, correctAuthorName));
        }
        [HttpPost("AddBinaryQuestion")]
        public async Task<IActionResult> AddBinaryQuestion(string quoteContent,
            string questionableAuthorName,
            string correctAuthorName)
        {
            return Ok(await _quizQuestionService.AddBinaryQuestion(quoteContent, questionableAuthorName, correctAuthorName));
        }
    }
}
