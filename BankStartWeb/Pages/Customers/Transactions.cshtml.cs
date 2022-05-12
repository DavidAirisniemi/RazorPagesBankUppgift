using BankStartWeb.Data;
using BankStartWeb.Infrastructure.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customers
{
    public class TransactionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<TransactionsViewModel> Transactions { get; set; }
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }

        public TransactionsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int id, int customerId)
        {
            CustomerId = customerId;
            TransactionId = id;
            /*var account = _context.Accounts.Include(account => account.Transactions).First(account => account.Id == id)*/
            
        }

        public IActionResult OnGetViewMore(int pagenum, int id)
        {
            var transactionQuery = _context.Accounts.Where(account => account.Id == id).SelectMany(transaction => transaction.Transactions);
            var pageResult = transactionQuery.GetPaged(pagenum, 10);
            Transactions = pageResult.Results.Select(transaction => new TransactionsViewModel
            {
                Id = transaction.Id,
                Type = transaction.Type,
                Operation = transaction.Operation,
                Date = transaction.Date,
                Amount = transaction.Amount,
                NewBalance = transaction.NewBalance,
            }).ToList();

            return new JsonResult(new { items = Transactions });
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
