using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankStartWeb.Pages.Transaction
{
    public class WithdrawModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        [Range(1, 100000)]
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
            if (Amount < 1)
            {
                ModelState.AddModelError(nameof(Amount), "Amount is negative");
            }

            if (ModelState.IsValid)
            {
                var account = _context.Accounts.First(account => account.Id == id);
                if (account.Balance < Amount)
                {
                    ModelState.AddModelError(nameof(Amount), "Balance not available");
                    return Page();
                }
                var transaction = new Data.Transaction
                {
                    Type = "Credit",
                    Operation = "Withdrawal",
                    Date = DateTime.Now,
                    Amount = Amount,
                    NewBalance = account.Balance - Amount

                };
                account.Transactions.Add(transaction);
                account.Balance = account.Balance - Amount;
                _context.SaveChanges();
                return RedirectToPage("/Customers/Transactions", new { id });
            }
            return Page();
        }
    }
}
