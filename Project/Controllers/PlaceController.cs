using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Project.Controllers
{
    public class PlaceController : Controller
    {
        private readonly PlaceRepository _repo = new PlaceRepository();

        public IActionResult Index(string searchString, string category)
        {
            var places = _repo.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                places = places.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(category) && category != "Все")
            {
                places = places.Where(p => p.Category == category).ToList();
            }
            ViewBag.CurrentSearch = searchString;
            ViewBag.CurrentCategory = category;

            return View(places);
        }
        public IActionResult Details(int id)
        {
            var place = _repo.GetById(id);
            if (place == null) return NotFound();
            return View(place);
        }


        public IActionResult Transactions()
        {
            var services = ServiceRepository.GetAll();
            return View(services);
        }


        public IActionResult AddToCart(int id)
        {
      
            var cartJson = HttpContext.Session.GetString("Cart");
            List<int> cart = cartJson == null ? new List<int>() : JsonSerializer.Deserialize<List<int>>(cartJson);


            cart.Add(id);

 
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));

            return RedirectToAction("Transactions");
        }


        public IActionResult Cart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            List<int> cartIds = cartJson == null ? new List<int>() : JsonSerializer.Deserialize<List<int>>(cartJson);
            List<Service> cartItems = new List<Service>();
            foreach (var id in cartIds)
            {
                var service = ServiceRepository.GetById(id);
                if (service != null) cartItems.Add(service);
            }

            return View(cartItems);
        }
        public IActionResult RemoveFromCart(int id)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (cartJson != null)
            {
                List<int> cart = JsonSerializer.Deserialize<List<int>>(cartJson);
                cart.Remove(id); 
                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
            }
            return RedirectToAction("Cart");
        }

        public IActionResult Map() => View(_repo.GetAll());
    }




}