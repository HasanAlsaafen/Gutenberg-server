using _1Fstask.Models;
using _1Fstask.Models._1Fstask.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _1Fstask.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        [HttpPost]
        public IActionResult AddContent([FromBody] Content newContent)
        {
            if (newContent == null || string.IsNullOrEmpty(newContent.Title) || string.IsNullOrEmpty(newContent.PageType))
            {
                return BadRequest("Invalid content data.");
            }
            newContent.ContentId = contents.Count > 0 ? contents.Max(c => c.ContentId) + 1 : 1;
            newContent.CreatedAt = DateTime.Now;
            newContent.CreatedBy = "admin"; 

            contents.Add(newContent);

            return CreatedAtAction(nameof(GetById), new { id = newContent.ContentId }, newContent);
        }

        private object GetById()
        {
            throw new NotImplementedException();
        }

        private static List<Content> contents = new List<Content>
        {
             new Content
        {
            ContentId = 1,
            Title = "Welcome to our website",
            Body = "This text appears on the home page..",
            PageType = "Home",
            CreatedBy = "admin",
            CreatedAt = DateTime.Now.AddDays(-7)
        },
              new Content
        {
            ContentId = 2,
            Title = "Who are we",
            Body = "We are a team specialized in developing software solutions.",
            PageType = "About",
            CreatedBy = "admin",
            CreatedAt = DateTime.Now.AddDays(-5)
        },
               new Content
        {
            ContentId = 3,
            Title = "Our services",
            Body = "We provide website and application development services.",
            PageType = "Services",
            CreatedBy = "admin",
            CreatedAt = DateTime.Now.AddDays(-3)
        }
        };
        [HttpGet]
        [HttpPut("{id}")]
        public IActionResult EditContent(int id, [FromBody] Content updatedContent)
        {
            var existingContent = contents.FirstOrDefault(c => c.ContentId == id);
            if (existingContent == null)
            {
                return NotFound("Content not found.");
            }

            if (updatedContent == null || string.IsNullOrEmpty(updatedContent.Title) || string.IsNullOrEmpty(updatedContent.PageType))
            {
                return BadRequest("Invalid content data.");
            }
            existingContent.Title = updatedContent.Title;
            existingContent.Body = updatedContent.Body;
            existingContent.PageType = updatedContent.PageType;

            return Ok(existingContent);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteContent(int id)
        {
            var contentToDelete = contents.FirstOrDefault(c => c.ContentId == id);
            if (contentToDelete == null)
            {
                return NotFound("Content not found.");
            }

            contents.Remove(contentToDelete);
            return Ok($"Content with ID {id} has been deleted.");
        }

        public IActionResult GetAll()
        {
            return Ok(contents);
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
