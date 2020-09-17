using MiroservicesDemo.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace MiroservicesDemo.UnitTest
{
    public class OrderUnitTest
    {
        [Fact]
        public void TestLineItemCreation()
        {
            // ARRANGE
            var lineItem = new LineItem()
            {
                Name = "Line Item One",
                Desc = "Line Item One Description",
                Unit = 1,
                UnitPrice = 1m,
                DiscountPercentage = 0
            };

            // ACT


            // ASSERT
            Assert.NotNull(lineItem);
            Assert.IsType<Guid>(lineItem.Id);
            Assert.IsAssignableFrom<Guid>(lineItem.Id);
            Assert.False(lineItem.Id == null);
            Assert.False(lineItem.Id == default);
        }

        [Fact]
        public void TestLineItemInvalidUnitPrice()
        {
            // ARRANGE
            var lineItem = new LineItem()
            {
                Name = "Line Item One",
                Desc = "Line Item One Description",
                Unit = 1,
                UnitPrice = 1m,
                DiscountPercentage = 0
            };

            // ACT

            // ASSERT
            Assert.Throws<InvalidOperationException>(() => lineItem.UnitPrice = 0);
        }

        [Fact]
        public void TestOrderCreation()
        {
            // ARRANGE
            var lineItemOne = new LineItem()
            {
                Name = "Line Item One",
                Desc = "Line Item One Description",
                Unit = 1,
                UnitPrice = 1m,
                DiscountPercentage = 0
            };

            var lineItemTwo = new LineItem()
            {
                Name = "Line Item Two",
                Desc = "Line Item Two Description",
                Unit = 2,
                UnitPrice = 2m,
                DiscountPercentage = 25
            };

            // ACT
            var order = new Order.Domain.Entities.Order
            {
                LineItems = new List<LineItem>
                {
                    lineItemOne, lineItemTwo
                }
            };

            // ASSERT
            Assert.NotNull(order);
            Assert.IsType<Guid>(order.Id);
            Assert.IsAssignableFrom<Guid>(order.Id);

            Assert.False(order.Id == default);
            Assert.NotEqual(Guid.Empty, order.Id);

            Assert.Equal(2, order.LineItems.Count);
        }

        [Fact]
        public void GetOrderTotal()
        {
            // ARRANGE
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

            var totalBeforeDiscount = (lineItemOne.UnitPrice * lineItemOne.Unit) + (lineItemTwo.UnitPrice * lineItemTwo.Unit);
            var totalDiscountForLineItemOne = (lineItemOne.UnitPrice * lineItemOne.DiscountPercentage / 100) * lineItemOne.Unit;
            var totalDiscountForLineItemTwo = (lineItemTwo.UnitPrice * lineItemTwo.DiscountPercentage / 100) * lineItemTwo.Unit;

            var total = totalBeforeDiscount - totalDiscountForLineItemOne - totalDiscountForLineItemTwo;

            // ACT
            var order = new Order.Domain.Entities.Order()
            {
                LineItems = new List<LineItem> { lineItemOne, lineItemTwo }
            };

            // ASSERT
            Assert.Equal(total, order.Total);
        }
    }
}
