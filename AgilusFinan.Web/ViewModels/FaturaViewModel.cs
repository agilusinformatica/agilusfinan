using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilusFinan.Web.ViewModels
{
    public class FaturaViewModel
    {
        public string email { get; set; }
        public string cc_emails { get; set; }
        public string due_date { get; set; }
        public List<ItemFatura> items { get; set; }
        public string return_url { get; set; }
        public string expired_url { get; set; }
        public string notification_url { get; set; }
        public Boolean fines { get; set; }
        public int late_payment_fine { get; set; }
        public Boolean per_day_interest { get; set; }
        public Int32 discount_cents { get; set; }
        public string customer_id { get; set; }
        public bool ignore_due_email { get; set; }
        public string payable_with { get; set; }
        public bool early_payment_discount { get; set; }
        public List<ItemDesconto> early_payment_discounts { get; set; }
        public Payer payer { get; set; }

        public FaturaViewModel()
        {
            early_payment_discounts = new List<ItemDesconto>();
            payer = new Payer();
            items = new List<ItemFatura>();
        }
    }

    public class ItemFatura
    {
        public String description { get; set; }
        public int quantity { get; set; }
        public long price_cents { get; set; }
    }

    public class ItemDesconto
    {
        public int days { get; set; }
        public string percent { get; set; }

    }

    public class Payer
    {
        public string cpf_cnpj { get; set; }
        public string name { get; set; }
        public string phone_prefix { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public Address address { get; set; }

        public Payer()
        {
            address = new Address();
        }
    }
    public class Address
    {
        public string zip_code { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string complement { get; set; }

    }
}
