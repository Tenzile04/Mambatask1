using Mamba.Business.Exceptions;
using Mamba.Business.Services.Interfaces;
using Mamba.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mamba.MVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ProfessionController : Controller
    {
        private readonly IProfessionService _professionService;
        public ProfessionController(IProfessionService professionService)
        {
            _professionService = professionService;
        }
        public async Task<IActionResult> Index()
        {
            List<Profession> allProfessions = await _professionService.GetAllAsync();

            return View(allProfessions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Profession profession)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _professionService.CreateAsync(profession);
            }
            catch (InvalidNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == null) return NotFound();

            Profession profession = await _professionService.GetByIdAsync(id);

            if (profession == null) return NotFound();

            return View(profession);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Profession profession)

        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _professionService.UpdateAsync(profession);
            }
            catch (InvalidNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();

            try
            {
                await _professionService.Delete(id);
            }
            catch (NullReferenceException)
            {
                return View();

            }
            return Ok();
        }
    }
}
