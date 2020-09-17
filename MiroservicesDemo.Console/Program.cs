using MiroservicesDemo.Order.Domain.Entities;
using System;

namespace MiroservicesDemo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var lineItemOne = new LineItem()
            {
                Name = "Line Item One",
                Desc = "Line Item One Description",
                Unit = 1,
                UnitPrice = 1.0m,
                DiscountPercentage = 0
            };

            var lineItemTwo = new LineItem()
            {
                Name = "Line Item Two",
                Desc = "Line Item Two Description",
                Unit = 2,
                UnitPrice = 2.0m,
                DiscountPercentage = 25
            };


            try
            {
                var totalDiscountForLineItemOne = decimal.Multiply(decimal.Multiply(lineItemOne.UnitPrice, decimal.Divide(lineItemOne.DiscountPercentage, 100)), lineItemOne.Unit);
                var totalDiscountForLineItemTwo = (lineItemTwo.UnitPrice * lineItemTwo.DiscountPercentage / 100) * lineItemTwo.Unit;
            }
            catch (Exception e)
            {
                var message = e.ToString();
            }

        }
    }
}
