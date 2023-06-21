using InvoiceManager.Models;
using InvoiceManager.Models.Domains;
using InvoiceManager.Models.Repositories;
using Microsoft.AspNet.Identity;
using Rotativa;
using Rotativa.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static InvoiceManager.Models.KnownFolders;

namespace InvoiceManager.Controllers
{
    public class PrintController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        private InvoiceRepository _invoiceRepository = new InvoiceRepository();

        public ActionResult InvoiceToPdf(int id)
        {
            var handle = Guid.NewGuid().ToString();
            var userId = User.Identity.GetUserId();
            var invoice = _invoiceRepository.GetInvoice(id, userId);

            TempData[handle] = GetPdfContent(invoice);

            return Json(new
            {
                FileGuid = handle,
                FileName = $@"Faktura_{invoice.ID}.pdf"
            });
        }

        private byte[] GetPdfContent(Invoice invoice)
        {
            var pdfResult = new ViewAsPdf(@"InvoiceTemplate", invoice)
            {
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait
            };

            return pdfResult.BuildFile(ControllerContext);
        }

        public ActionResult DownloadInvoicePdf(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] == null)
                throw new Exception("Błąd przy próbie eksportu faktury do PDF.");

            var data = TempData[fileGuid] as byte[];
            return File(data, "application/pdf", fileName);
        }

        public ActionResult InvoiceTemplate(int id)
        {
            var userId = User.Identity.GetUserId();
            var invoice = _invoiceRepository.GetInvoice(id, userId);

            return View(invoice);
        }

        public void PrintInvoice(int id)
        {
            InvoiceToPdf(id);

            var downloadFolderPath = GetPath("{374DE290-123F-4565-9164-39C4925E467B}", KnownFolderFlags.DontVerify, false);
            var userId = User.Identity.GetUserId();
            var invoice = _invoiceRepository.GetInvoice(id, userId);

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                Verb = "print",
                FileName = Path.Combine(downloadFolderPath, $@"Faktura_{invoice.ID}.pdf") //put the correct path here
            };
            p.Start();
        }
    }
}