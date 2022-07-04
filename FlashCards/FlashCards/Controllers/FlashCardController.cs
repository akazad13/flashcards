using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Controllers
{
    [ApiController]
    [Route("api/flashcards")]
    public class FlashCardController : ControllerBase
    {
        [HttpGet]
        public IActionResult get()
        {
            
        }
    }
}
