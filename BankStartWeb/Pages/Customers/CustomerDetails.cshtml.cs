using BankStartWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BankStartWeb.Pages.Customers
{
    public class CustomerDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CustomerViewModel Customer { get; set; }
        public List<AccountViewModel> Accounts { get; set; }
        public string TotalBalance { get; set; }

        public CustomerDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public void OnGet(int id)
        {
            var tempCustomer = _context.Customers.Include(customer => customer.Accounts).First(customer => customer.Id == id);
            Customer = new CustomerViewModel
            {
                Id = tempCustomer.Id,
                Givenname = tempCustomer.Givenname,
                Surname = tempCustomer.Surname,
                City = tempCustomer.City,
                Country = tempCustomer.Country,
                Telephone = tempCustomer.Telephone,
                EmailAddress = tempCustomer.EmailAddress,
                Birthday = tempCustomer.Birthday
            };
            Accounts = tempCustomer.Accounts.Select(account => new AccountViewModel
            {
                Id = account.Id,
                AccountType = account.AccountType,
                Created = account.Created,
                Balance = account.Balance
            }).ToList();

            TotalBalance = tempCustomer.Accounts.Sum(sum => sum.Balance).ToString("C");
        }
    }


    public class AccountViewModel
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
    }
}
