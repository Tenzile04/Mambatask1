using Mamba.Business.Exceptions;
using Mamba.Business.Services.Interfaces;
using Mamba.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mamba.MVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly IProfessionService _professionService;
        public TeamController(ITeamService teamService, IProfessionService professionService)
        {
            _teamService = teamService;
            _professionService = professionService;
        }
        public async Task<IActionResult> Index()
        {
            List<Team> allTeam = await _teamService.GetAllAsync();

            return View(allTeam);

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            ViewBag.Professions = await _professionService.GetAllAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {

            ViewBag.Professions = await _professionService.GetAllAsync();

            if (!ModelState.IsValid) return View();

            try
            {
                await _teamService.CreateAsync(team);
            }
            catch (InvalidImageFileException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();

            }
           

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Professions = await _professionService.GetAllAsync();
            if (id == null) return NotFound();

            Team team = await _teamService.GetByIdAsync(id);

            if (team == null) return NotFound();

            return View(team);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Team team)
        {
            ViewBag.Professions = await _professionService.GetAllAsync();

            if (!ModelState.IsValid) return View();

            try
            {
                await _teamService.UpdateAsync(team);
            }
            catch (InvalidImageFileException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();

            }
           

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Professions = await _professionService.GetAllAsync();

            if (id == null) return NotFound();

            try
            {
                await _teamService.SoftDelete(id);
            }
            catch (NullReferenceException)
            {

            }

            return Ok();
        }
    }
}
