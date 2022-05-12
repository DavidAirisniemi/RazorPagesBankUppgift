using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Transaction
{
    [BindProperties]
    public class TransferModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public int Amount { get; set; }
        public int TargetAccountId { get; set; }
        public TransferModel(ApplicationDbContext context)
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
                if (account.Balance < Amount)
                {
                    ModelState.AddModelError(nameof(Amount), "Balance not available");
                    return Page();

                }
                var transaction = new Data.Transaction
                {
                    Type = "Credit",
                    Operation = "Transfer",
                    Date = DateTime.Now,
                    Amount = Amount,
                    NewBalance = account.Balance - Amount
                };
                account.Transactions.Add(transaction);

                var targetAccount = _context.Accounts.First(account => account.Id == TargetAccountId);
                var targetAccountTransaction = new Data.Transaction
                {
                    Type = "Debit",
                    Operation = "Deposit",
                    Date = DateTime.Now,
                    Amount = Amount,
                    NewBalance = targetAccount.Balance + Amount
                };
                targetAccount.Transactions.Add(targetAccountTransaction);
                account.Balance = account.Balance - Amount;
                targetAccount.Balance = targetAccount.Balance + Amount;
                _context.SaveChanges();
                return RedirectToPage("/Customers/Transactions", new { id });
            }
            return Page();
        }
    }
}
