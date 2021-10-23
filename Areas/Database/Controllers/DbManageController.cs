using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _dbContext;
        [TempData]
        public string StatusMessage {set;get;}

        public DbManageController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DeleteDb(){
            return View();
        }
         [HttpPost]
        public async Task<IActionResult> DeleteDbAsync(){
            var success = await _dbContext.Database.EnsureDeletedAsync();
            StatusMessage = success ? "Delete database success" : "Delete database failed";
            return RedirectToAction(nameof(Index));
        }

          [HttpPost]
        public async Task<IActionResult> MigrateAsync(){
            await _dbContext.Database.MigrateAsync();
            StatusMessage = "Update database success";
            return RedirectToAction(nameof(Index));
        }


    }
}