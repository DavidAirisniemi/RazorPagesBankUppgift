using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customers
{
    public class CustomersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Customer> Customers { get; set; }

        public CustomersModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
             Customers = _context.Customers.Select(customer => customer).Take(50).ToList();
        }
    }
}
