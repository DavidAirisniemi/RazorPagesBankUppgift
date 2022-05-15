using BankStartWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.Pages.Transaction
{
    [TestClass]
    public class WithdrawTest
    {
        public ApplicationDbContext _context { get; set; }

        public BankStartWeb.Pages.Transaction.WithdrawModel _sut { get; set; }

        public WithdrawTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Bank_AB")
                .Options;
            _context = new ApplicationDbContext(options);

            _sut = new BankStartWeb.Pages.Transaction.WithdrawModel(_context);

            var data = new DataInitializerTest(_context);
            data.SeedData();
        }

        [TestMethod]
        public void NoNegativeWithdraw()
        {
            _sut.Amount = -1;
            _sut.OnPost(1);
            Assert.AreEqual(_sut.ModelState.IsValid, false);
        }

        [TestMethod]
        public void NoWithdrawOverDraft()
        {
            _sut.Amount = 100001;
            _sut.OnPost(1);
            Assert.AreEqual(_sut.ModelState.IsValid, false);
        }
    }
}
