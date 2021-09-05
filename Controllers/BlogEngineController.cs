using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Threading.Tasks;
using BlogClient.Services;

namespace BlogClient.Controllers
{
    public class BlogEngine : Controller
    {
        private IBlogService _blogService;

        public BlogEngine(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: Blog
        [AuthorizeForScopes(ScopeKeySection = "TodoList:TodoListScope")]
        public async Task<ActionResult> Index()
        {
            return View(await _blogService.GetAsync());
        }     
    }
}