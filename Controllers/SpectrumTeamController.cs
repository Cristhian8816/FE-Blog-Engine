using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Threading.Tasks;
using SpectrumTeamClient.Services;
using SpectrumTeamService.Models;

namespace SpectrumTeamClient.Controllers
{
    public class SpectrumTeam : Controller
    {
        private ISpectrumTeamService _spectrumTeamService;

        public SpectrumTeam(ISpectrumTeamService spectrumTeamService)
        {
            _spectrumTeamService = spectrumTeamService;
        }

        // GET: SpectrumTeam
        [AuthorizeForScopes(ScopeKeySection = "TodoList:TodoListScope")]
        public async Task<ActionResult> Index()
        {
            return View(await _spectrumTeamService.GetAsync());
        }

        // GET: TodoList/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _spectrumTeamService.GetAsync(id));
        }

        //// GET: SpectrumTeam/Edit/5
        ////[AuthorizeForScopes(ScopeKeySection = "TodoList:TodoListScope2")]
        public async Task<ActionResult> Edit(int id)
        {
            Languages languague = await this._spectrumTeamService.GetAsync(id);

            if (languague == null)
            {
                return NotFound();
            }

            return View(languague);
        }
    }
}