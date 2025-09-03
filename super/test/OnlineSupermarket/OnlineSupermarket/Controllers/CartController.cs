using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineSupermarket.Data;
using OnlineSupermarket.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSupermarket.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var items = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            ViewBag.Total = items.Sum(i => i.Product.Price * i.Quantity);
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, int Quantity)
        {
            var userId = _userManager.GetUserId(User);

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.ProductId == id && c.UserId == userId);

            if (cartItem != null)
            {
                cartItem.Quantity += 1;
            }
            else
            {
                cartItem = new CartItem
                {
                    ProductId = product.Id,
                    Quantity = Quantity,
                    UserId = userId
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (cartItem == null)
                return NotFound();

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            return RedirectToAction("OnlinePayment", "Payment");
        }
    }
}
