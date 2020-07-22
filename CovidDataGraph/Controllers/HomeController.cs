using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidDataGraph.Models;
using Microsoft.AspNetCore.Mvc;

namespace CovidDataGraph.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repo;
        public HomeController(IRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _repo.Get<IDictionary<string, Country>>();
            return View();
        }
    }
}
