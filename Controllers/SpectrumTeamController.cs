using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Threading.Tasks;
using SpectrumTeamClient.Services;

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
    }
}