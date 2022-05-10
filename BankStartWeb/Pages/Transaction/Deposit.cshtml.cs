using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Transaction
{
    public class DepositModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public int Amount { get; set; }

        public DepositModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int id)
        {
            //var tempCustomer = _context.Customers.Include(customer => customer.Accounts).First(customer => customer.Id == id);

        }

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var account = _context.Accounts.First(account => account.Id == id);
                account.Balance = account.Balance + Amount;
                _context.SaveChanges();
                return RedirectToPage("/Customers/CustomerDetails", new {id});
            }
            return Page();
        }
    }
}
