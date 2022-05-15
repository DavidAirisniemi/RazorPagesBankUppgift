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
    public class TransferTest
    {
        public ApplicationDbContext _context { get; set; }

        public BankStartWeb.Pages.Transaction.TransferModel _sut { get; set; }

        public TransferTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Bank_AB")
                .Options;
            _context = new ApplicationDbContext(options);

            _sut = new BankStartWeb.Pages.Transaction.TransferModel(_context);

            var data = new DataInitializerTest(_context);
            data.SeedData();
        }

        [TestMethod]
        public void NoNegativeTransfer()
        {
            _sut.Amount = -1;
            _sut.OnPost(1);
            Assert.AreEqual(_sut.ModelState.IsValid, false);
        }

        [TestMethod]
        public void noTransferOverDraft()
        {
            _sut.Amount = 100001;
            _sut.OnPost(1);
            Assert.AreEqual(_sut.ModelState.IsValid, false);
        }
  
    }
}
