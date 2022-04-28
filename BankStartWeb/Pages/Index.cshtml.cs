using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ApplicationDbContext _context;

        public int Accounts { get; set; }
        public int Customers { get; set; }
        public decimal TotalAccountBalance { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Accounts = _context.Accounts.Count();
            Customers = _context.Customers.Count();
            TotalAccountBalance = _context.Accounts.Sum(Account => Account.Balance);
        }
    }
}