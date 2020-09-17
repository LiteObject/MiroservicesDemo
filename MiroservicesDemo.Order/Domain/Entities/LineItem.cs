using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroservicesDemo.Order.Domain.Entities
{
    public class LineItem : BaseEntity
    {
        private decimal unitPrice;

        public string Name { get; set; }

        public string Desc { get; set; }

        public int Unit { get; set; } = 1;

        public decimal UnitPrice
        {
            get { return this.unitPrice; }
            set
            {
                if (value <= 0)
                {
                    throw new InvalidOperationException($"{nameof(this.unitPrice)} cannot less than or equal to zero.");
                }

                this.unitPrice = value;
            }
        }

        public int DiscountPercentage { get; set; }

        public decimal GetSubtotal()
        {
            return (this.Unit * this.UnitPrice) - GetSavingsSubtotal();
        }

        public decimal GetSubtotalBeforeSavings()
        {
            return this.Unit * this.UnitPrice;
        }

        public decimal GetSavingsSubtotal()
        {

            // Some business logic
            if (this.DiscountPercentage <= 0 || this.DiscountPercentage >= 100)
            {
                return 0;
            }

            return (this.UnitPrice * this.DiscountPercentage / 100) * this.Unit;
        }
    }
}
