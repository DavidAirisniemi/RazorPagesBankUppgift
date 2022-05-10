using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customers
{
    public class TransactionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<TransactionsViewModel> Transactions { get; set; }
        public int Id { get; set; }

        public TransactionsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int id)
        {
            Id = id;
            var account = _context.Accounts.Include(account => account.Transactions).First(account => account.Id == id);
            Transactions = account.Transactions.Select(transaction => new TransactionsViewModel
            {
                Id = transaction.Id,
                Type = transaction.Type,
                Operation = transaction.Operation,
                Date = transaction.Date,
                Amount = transaction.Amount,
                NewBalance = transaction.NewBalance,
            }).ToList();
        }    
    }

    public class TransactionsViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal NewBalance { get; set; }
    }
}
