﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Domains
{
    public class Invoice
    {
        public Invoice()
        {
            InvoicePositions = new Collection<InvoicePosition>();
        }

        public int ID { get; set; }
        [Required(ErrorMessage ="Pole Tytuł jest wymagane")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Wartość")]
        public decimal Value { get; set; }
        [Required(ErrorMessage = "Pole Sposób płatności jest wymagane")]
        [Display(Name = "Sposób płatności")]
        public int MethodOfPaymentId { get; set; }
        [Required(ErrorMessage = "Pole Data płatności jest wymagane")]
        [Display(Name = "Data płatności")]
        public DateTime PaymentDate { get; set; }
        [Display(Name = "Data utworzenia")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Uwagi")]
        public string Comments { get; set; }
        [Required(ErrorMessage = "Pole Klient jest wymagane")]
        [Display(Name = "Klient")]
        public int ClientId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public MethodOfPayment MethodOfPayment { get; set; }
        public Client Client { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<InvoicePosition> InvoicePositions { get; set; }

    }
}