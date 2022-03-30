using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    
    public class HomeController : Controller
    {
        private IBowlersRepository _repo {get; set;}

        public HomeController(IBowlersRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {
            var blah = _repo.Bowlers.Include(x => x.Team).ToList();
            return View(blah);
        }
        // when you get the form
        [HttpGet]
        public IActionResult BowlerForm()
        {
            ViewBag.Teams = _repo.Teams.ToList();
            return View();
        }
        
        //when you post the form 
        [HttpPost]
        public IActionResult BowlerForm(Bowler blah)
        {
            _repo.CreateBowler(blah);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit (int bowlerid)
        {
            ViewBag.Teams = _repo.Teams.ToList();
            var form = _repo.Bowlers.Single(x => x.BowlerID == bowlerid);
            return View("BowlerForm", form);
        }
        [HttpPost]
        public IActionResult Edit (Bowler blah)
        {
            _repo.SaveBowler(blah);
            return RedirectToAction("Index");
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
