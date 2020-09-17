using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroservicesDemo.Order.Domain.Entities
{
    public class Order : BaseEntity
    {
        private List<LineItem> lineItems = new List<LineItem>();

        public string Note { get; set; }

        public List<LineItem> LineItems { 
            get { return this.lineItems; } 
            set 
            { 
            if(value != null)
                {
                    this.lineItems = value;
                    this.Total = 0;

                    foreach (var item in this.lineItems)
                    {
                        this.Total += item.GetSubtotal();
                    }
                }
            } 
        }

        public decimal Total { get; private set; }

        public decimal GetTotalSavings() {

            decimal savings = 0;
            this.lineItems.ForEach(l => {
                savings += l.GetSavingsSubtotal();
            });

            return savings;
        }
    }
}
