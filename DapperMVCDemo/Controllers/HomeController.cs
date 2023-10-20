using DapperMVC.Data.Models.DbModel;
using DapperMVC.Data.Repository;
using DapperMVCDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DapperMVCDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonRepository _personRepo;
        public HomeController(ILogger<HomeController> logger, IPersonRepository personRepo)
        {
            _logger = logger;
            _personRepo = personRepo;
        }

        public IActionResult Person()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(person);
                }
                bool addPerson = await _personRepo.AddAsync(person);
                if(addPerson) 
                { 
                    TempData["msg"] = "Successfully Added";
                }
                else
                {
                    TempData["msg"] = "Could Not Added";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could Not Added";
            }
            return RedirectToAction(nameof(Add));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _personRepo.GetByIdAsync(id);
            if(person == null)
            {
                throw new Exception();
            }
            return View("Edit",person);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(person);
                }
                var updateResult = await _personRepo.UpdateAsync(person);
                if (updateResult)
                {
                    TempData["msg"] = "Edit Successfully.";
                    return RedirectToAction(nameof(DisplayAllPerson));
                }
                else
                {
                    TempData["msg"] = "Could Not Edit.";
                    return View(person);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could Not Edit.";
                return View(person);
            } 
        }
        [HttpGet]
        public async Task<IActionResult> DisplayAllPerson()
        {
            try
            {
                var personAll = await _personRepo.GetAllPersonAsync();
                return View(personAll);
            }
            catch (Exception ex)
            { 
                return View("Error", ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult =await _personRepo.DeleteAsync(id);
            return RedirectToAction(nameof(DisplayAllPerson));
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