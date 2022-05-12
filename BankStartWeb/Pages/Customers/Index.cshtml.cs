using BankStartWeb.Data;
using BankStartWeb.Infrastructure.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankStartWeb.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<CustomerViewModel> Customers { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CustomerSearch { get; set; }
        public int PageNum { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(int pagenum = 1)
        {
            PageNum = pagenum;

            var cust = _context.Customers.AsQueryable();
            
            
            var pageResult = cust.GetPaged(PageNum, 10);

            Customers = pageResult.Results.Select(customer => new CustomerViewModel
            {
                Id = customer.Id,
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                NationalId = customer.NationalId,
                Streetaddress = customer.Streetaddress,
                City = customer.City,
                Country = customer.Country,
                Telephone = customer.Telephone,
                EmailAddress = customer.EmailAddress, 
                Birthday = customer.Birthday
            }).ToList();

            if (!string.IsNullOrEmpty(CustomerSearch))
            {
                CustomerSearch = CustomerSearch.ToLower();
                Customers = Customers.Where(customer =>
                customer.Id.ToString().Contains(CustomerSearch) ||
                customer.Givenname.ToLower().Contains(CustomerSearch) ||
                customer.Surname.ToLower().Contains(CustomerSearch) ||
                customer.City.ToLower().Contains(CustomerSearch)).ToList(); 
            }
        }

       
    }

    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string NationalId { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Birthday { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
