using Microsoft.AspNetCore.Mvc;
using Travel.Areas.Admin.ViewModels.Destination;
using Travel.Contexts;
using Travel.Models;

namespace Travel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DestinationController : Controller
    {
        TravelDbContext _db { get; }
        public DestinationController(TravelDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var datas = _db.Destinations.Select(d => new DestinationListItemVM
            {
                Id = d.Id,
                ImageUrl = d.ImageUrl,
                Price = d.Price,
                Title = d.Title
            }).ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DestinationCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Destination des = new Destination
            {
                Title = vm.Title,
                ImageUrl = vm.ImageUrl,
                Price = vm.Price,
            };
            await _db.Destinations.AddAsync(des);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0 || id == null) return BadRequest();
            var data = await _db.Destinations.FindAsync(id);
            if (data == null) return NotFound();
            return View(new DestinationUpdateVM
            {
                ImageUrl = data.ImageUrl,
                Price = data.Price,
                Title= data.Title
            });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(int id, DestinationUpdateVM vm)
        {
            if (id <= 0 || id == null) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await _db.Destinations.FindAsync(id);
            if (data == null) return NotFound();
            data.Title = vm.Title;
            data.Price = vm.Price;
            data.ImageUrl = vm.ImageUrl;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0 || id == null) return BadRequest();
            var data = await _db.Destinations.FindAsync(id);
            if (data == null) return NotFound();
            _db.Destinations.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
