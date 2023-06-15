using InvoiceManager.Models.Domains;
using InvoiceManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace InvoiceManager.Models.Repositories
{
    public class InvoiceRepository
    {
        public List<Invoice> GetInvoices(string userId)
        {
            using (var context=new ApplicationDbContext())
            {
                return context.Invoices.Include(x =>x.Client).Where(x=>x.UserId==userId).ToList();
            }
        }

        public Invoice GetInvoice(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Invoices
                    .Include(x => x.InvoicePositions)
                    .Include(x => x.InvoicePositions.Select(y => y.Product))
                    .Include(x => x.MethodOfPayment)
                    .Include(x => x.User)
                    .Include(x => x.User.Address)
                    .Include(x => x.Client)
                    .Include(x => x.Client.Adress)
                    .Single(x => x.ID == id && x.UserId == userId);
            }
        }

        public List<MethodOfPayment> GetMethodsOfPayment()
        {
            throw new NotImplementedException();
        }

        public InvoicePosition GetInvoicePosition(int invoicePositionId, string userId)
        {
            throw new NotImplementedException();
        }

        public void Add(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public void Update(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public void AddPosition(InvoicePosition invoicePosition, string userId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePosition(InvoicePosition invoicePosition, string userId)
        {
            throw new NotImplementedException();
        }

        public decimal UpdateInvoiceValue(int invoiceId, string userId)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public void DeletePosition(int id, string userId)
        {
            throw new NotImplementedException();
        }
    }
}