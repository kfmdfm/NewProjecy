using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class PlaceController : Controller
    {
        private readonly PlaceRepository _repo = new PlaceRepository();

        public IActionResult Index() => View(_repo.GetAll());

        public IActionResult Details(int id)
        {
            var place = _repo.GetById(id);
            if (place == null) return NotFound();
            return View(place);
        }

        public IActionResult Map() => View(_repo.GetAll());
    }
}