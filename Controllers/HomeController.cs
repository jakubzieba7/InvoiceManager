using InvoiceManager.Models.Domains;
using InvoiceManager.Models.Repositories;
using InvoiceManager.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private InvoiceRepository _invoiceRepository = new InvoiceRepository();
        private ClientRepository _clientRepository = new ClientRepository();
        private ProductRepository _productRepository = new ProductRepository();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var invoices = _invoiceRepository.GetInvoices(userId);


            return View(invoices);
        }

        public ActionResult Invoice(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var invoice = id == 0 ? GetNewInvoice(userId) : _invoiceRepository.GetInvoice(id, userId);

            var vm = PrepareInvoiceVm(invoice, userId);

            return View(vm);
        }


        private object PrepareInvoiceVm(Invoice invoice, string userId)
        {
            return new EditInvoiceViewModel
            {
                Invoice = invoice,
                Heading = invoice.ID == 0 ? "Dodawanie nowej faktury" : "Faktura",
                Clients = _clientRepository.GetClients(userId),
                MethodOfPayments = _invoiceRepository.GetMethodsOfPayment()
            };
        }

        private Invoice GetNewInvoice(string userId)
        {
            return new Invoice
            {
                UserId = userId,
                CreatedDate = DateTime.Now,
                PaymentDate = DateTime.Now.AddDays(7),
            };
        }

        public ActionResult InvoicePosition(int invoiceId, int invoicePositionId = 0)
        {
            var userId = User.Identity.GetUserId();

            var invoicePosition = invoicePositionId == 0 ? GetNewPosition(invoiceId, invoicePositionId) : _invoiceRepository.GetInvoicePosition(invoicePositionId, userId);

            var vm = PrepareInvoicePositionVm(invoicePosition);

            return View(vm);
        }

        private EditInvoicePositionViewModel PrepareInvoicePositionVm(InvoicePosition invoicePosition)
        {
            return new EditInvoicePositionViewModel
            {
                InvoicePosition = invoicePosition,
                Heading = invoicePosition.Id == 0 ? "Dodawanie nowej pozycji" : "Pozycja",
                Products = _productRepository.GetProducts(),

            };
        }

        private InvoicePosition GetNewPosition(int invoiceId, int invoicePositionId)
        {
            return new InvoicePosition
            {
                InvoiceId = invoiceId,
                Id = invoicePositionId,
            };
        }

        [HttpPost]
        public ActionResult Invoice(Invoice invoice)
        {
            var userId = User.Identity.GetUserId();
            invoice.UserId = userId;

            if (invoice.ID == 0)
                _invoiceRepository.Add(invoice);
            else
                _invoiceRepository.Update(invoice);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}