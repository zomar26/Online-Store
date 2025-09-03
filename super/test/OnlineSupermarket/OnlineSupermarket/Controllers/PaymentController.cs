using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineSupermarket.Data;
using OnlineSupermarket.Models;
using System.Threading.Tasks;
using System.Linq;

namespace OnlineSupermarket.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnlinePayment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnlinePayment(Payment payment)
        {
            
            if (ModelState.IsValid)
            {
                payment.UserId = _userManager.GetUserId(User); 
                payment.Date = DateTime.Now;

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Success");
            }
            return View(payment);
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var payments = await _context.Payments
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return View(payments);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
