using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineSupermarket.Data;
using OnlineSupermarket.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSupermarket.Controllers
{
    [Authorize] 
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return View(products);
        }
        [Authorize(Roles ="Admin")]
        // GET: Product/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {

            if (product.Image == null || product.Image.Length == 0)
            {
                ModelState.AddModelError("Image", "No image uploaded");
                return View("Create", product);
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(product.Image.FileName).ToLower();

            if (!allowedExtensions.Contains(ext))
            {
                ModelState.AddModelError("", "Only .jpg, .jpeg, .png formats are allowed.");
                return View("Create", product);
            }


            var fileName = $"{Guid.NewGuid()}{ext}";


            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);


            using (var stream = new FileStream(path, FileMode.Create))
            {
                await product.Image.CopyToAsync(stream);
            }


            product.ImageUrl = $"images/{fileName}";




            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }
        //[Authorize(Roles = "Admin")]

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }
        

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return NotFound();
            Product existingProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id );
            if (ModelState.IsValid)
            {
                try
                {
                    existingProduct.Price = product.Price;
                    existingProduct.Name = product.Name;
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.Description = product.Description;
                    if (product.Image != null && product.Image.Length > 0)
                    {
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                        var ext = Path.GetExtension(product.Image.FileName).ToLower();

                        if (!allowedExtensions.Contains(ext))
                        {
                            ModelState.AddModelError("", "Only .jpg, .jpeg, .png formats are allowed.");
                            return View(product);
                        }

                        var fileName = $"{Guid.NewGuid()}{ext}";
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await product.Image.CopyToAsync(stream);
                        }



                        existingProduct.ImageUrl = $"images/{fileName}";
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }
        //[Authorize(Roles = "Admin")]

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Product/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
