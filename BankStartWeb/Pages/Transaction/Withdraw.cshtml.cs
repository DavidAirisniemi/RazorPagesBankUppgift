using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Transaction
{
    public class WithdrawModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public int Amount { get; set; }
        public WithdrawModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var account = _context.Accounts.First(account => account.Id == id);
                account.Balance = account.Balance - Amount;
                _context.SaveChanges();
                return RedirectToPage("/Customers/CustomerDetails", new { id });
            }
            return Page();
        }
    }
}
