using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Transaction
{
    public class TransferModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public TransferModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
    }
}
