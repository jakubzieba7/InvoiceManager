using InvoiceManager.Models.Domains;
using InvoiceManager.Models.ViewModels;
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
        public ActionResult Index()
        {
            var invoices = new List<Invoice>()
            {
            new Invoice
                {
                    ID=1,
                    Title="FA/01/2023",
                    CreatedDate= DateTime.Now,
                    PaymentDate= DateTime.Now,
                    Value=1200,
                    Client=new Client{ Name="Klient 1"}
                },
            new Invoice
                {
                    ID=2,
                    Title="FA/02/2023",
                    CreatedDate= DateTime.Now,
                    PaymentDate= DateTime.Now,
                    Value=1500,
                    Client=new Client{ Name="Klient 2"}
                }
            };

            return View(invoices);
        }

        public ActionResult Invoice(int id = 0)
        {
            EditInvoiceViewModel vm = null;

            if (id == 0)
            {
                vm = new EditInvoiceViewModel
                {
                    Clients = new List<Client> { new Client { Id = 1, Name = "Klient1" } },
                    MethodOfPayments = new List<MethodOfPayment> { new MethodOfPayment { Id = 1, Name = "Przelew" } },
                    Heading = "Edycja faktury",
                    Invoice = new Invoice()

                };
            }
            else
            {
                vm = new EditInvoiceViewModel
                {
                    Clients = new List<Client> { new Client { Id = 1, Name = "Klient1" } },
                    MethodOfPayments = new List<MethodOfPayment> { new MethodOfPayment { Id = 1, Name = "Przelew" } },
                    Heading = "Edycja faktury",
                    Invoice = new Invoice
                    {
                        ClientId = 1,
                        Comments = "Uwagi do faktury",
                        PaymentDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        MethodOfPaymentId = 1,
                        ID = 1,
                        Value = 1000,
                        Title = "FA/07/2023",
                        InvoicePositions = new List<InvoicePosition>
                        {  new InvoicePosition
                            {
                                Lp=1,
                                Product=new Product
                                {
                                    Name="Produkt 1"
                                },
                                Quantity=10,
                                Value=1000
                            },
                            new InvoicePosition
                            {
                                Lp=2,
                                Product=new Product
                                {
                                    Name="Produkt 2"
                                },
                                Quantity=4,
                                Value=120
                            }
                        }
                    }

                };
            }

            return View(vm);
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