using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Pages;

namespace SportsStore.Controllers
{
    public class StoreController : Controller
    {
        private IRepository prodRepo;
        private ICategoryRepository catRepo;

        public StoreController(IRepository pRepo, ICategoryRepository cRepo)
        {
            prodRepo = pRepo;
            catRepo = cRepo;
        }

        public IActionResult Index([FromQuery(Name = "options")] QueryOptions prodOptions, QueryOptions catOptions, long category)
        {
            ViewBag.Categories = catRepo.GetCategories(catOptions);
            ViewBag.SelectedCategory = category;

            return View(prodRepo.GetProducts(prodOptions, category));
        }
    }
}