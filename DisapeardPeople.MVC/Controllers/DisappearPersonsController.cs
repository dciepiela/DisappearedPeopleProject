using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DisapeardPeople.MVC.Data;
using DisapeardPeople.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using DisapeardPeople.MVC.Data.Enum;
using Microsoft.Extensions.Hosting;

namespace DisapeardPeople.MVC.Controllers
{
    public class DisappearPersonsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DisappearPersonsController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: DisappearPersons
        public async Task<IActionResult> Index(Gender? genderFilter)
        {
            var disappearPersons = _context.DisappearPerson.AsQueryable();

            if (genderFilter.HasValue)
            {
                disappearPersons = disappearPersons.Where(x => x.Gender == genderFilter.Value);
            }

            return View(await disappearPersons.ToListAsync());
        }

        // GET: DisappearPersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DisappearPerson == null)
            {
                return NotFound();
            }

            var disappearPerson = await _context.DisappearPerson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disappearPerson == null)
            {
                return NotFound();
            }

            return View(disappearPerson);
        }

        // GET: DisappearPersons/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: DisappearPersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,FirstName,Surname,Age,Height,City,ImageFile,DisappearDate,Province,Gender")] DisappearPerson disappearPerson)
        {
                //Zapisz zdjęcie do wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(disappearPerson.ImageFile.FileName);
                string extension = Path.GetExtension(disappearPerson.ImageFile.FileName);
                disappearPerson.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await disappearPerson.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(disappearPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: DisappearPersons/Edit/5
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DisappearPerson == null)
            {
                return NotFound();
            }

            var disappearPerson = await _context.DisappearPerson.FindAsync(id);
            if (disappearPerson == null)
            {
                return NotFound();
            }
            return View(disappearPerson);
        }

        // POST: DisappearPersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Surname,Age,Height,City,ImageFile,DisappearDate,Province,Gender")] DisappearPerson disappearPerson)
        {
            if (id != disappearPerson.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    if (disappearPerson.ImageFile != null && disappearPerson.ImageFile.Length > 0)
                    {
                        string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Image");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + disappearPerson.ImageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await disappearPerson.ImageFile.CopyToAsync(stream);
                        }
                        disappearPerson.Image = uniqueFileName;
                    }

                    _context.Update(disappearPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisappearPersonExists(disappearPerson.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(disappearPerson);
        }

        // GET: DisappearPersons/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DisappearPerson == null)
            {
                return NotFound();
            }

            var disappearPerson = await _context.DisappearPerson
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disappearPerson == null)
            {
                return NotFound();
            }

            return View(disappearPerson);
        }

        // POST: DisappearPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disappearPerson = await _context.DisappearPerson.FindAsync(id);

            //usuwanie img z wwwroot/Image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Image", disappearPerson.Image);
            if(System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.DisappearPerson.Remove(disappearPerson);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisappearPersonExists(int id)
        {
          return (_context.DisappearPerson?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
