﻿using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
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