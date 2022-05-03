using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<CustomerViewModel> Customers { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Customers = _context.Customers.Take(50).Select(customer => new CustomerViewModel
            {
                Id = customer.Id,
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                City = customer.City,
                Country = customer.Country,
                Telephone = customer.Telephone,
                EmailAddress = customer.EmailAddress, 
                Birthday = customer.Birthday
            }).ToList();
        }
    }

    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
