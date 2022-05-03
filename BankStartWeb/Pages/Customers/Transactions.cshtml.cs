using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customers
{
    public class TransactionsModel : PageModel
    {
        private readonly ApplicationDbContext _context;  

        public TransactionsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
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
