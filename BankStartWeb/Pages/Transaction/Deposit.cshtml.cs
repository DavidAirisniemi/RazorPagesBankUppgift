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

        }

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var account = _context.Accounts.First(account => account.Id == id);
                var transaction = new Data.Transaction
                {
                    Type = "Debit",
                    Operation = "Deposit",
                    Date = DateTime.Now,
                    Amount = Amount,
                    NewBalance = account.Balance + Amount

                };
                account.Transactions.Add(transaction);
                account.Balance = account.Balance + Amount;
                _context.SaveChanges();
                return RedirectToPage("/Customers/Transactions", new { id });
            }
            return Page();
        }
    }
}
