using System;
using FluentAssertions;
using LanguageExt;
using LanguageExt.ClassInstances;
using Xunit;

namespace FunctionalCSharp.Tests
{
    public class EitherTests
    {
        [Fact]
        public void ProductWithAbsoluteDiscount()
        {
            var productWithAbsoluteDiscount = new Product(new ProductName("TestProduct"), new ProductPrice(1000M), new AbsoluteDiscount(100M, Periodicity.Annual));
            var finalPrice = productWithAbsoluteDiscount.GetFinalPrice();

            finalPrice.Value.Should().Be(900M);
        }

        [Fact]
        public void ProductWithRelativeDiscount()
        {
            var productWithAbsoluteDiscount = new Product(new ProductName("TestProduct"), new ProductPrice(1000M), new RelativeDiscount(25.0M));
            var finalPrice = productWithAbsoluteDiscount.GetFinalPrice();

            finalPrice.Value.Should().Be(750M);
        }
    }

    public class AbsoluteDiscount : Record<AbsoluteDiscount>
    {
        public readonly decimal Value;
        public readonly Periodicity Periodicity;

        public AbsoluteDiscount(decimal value, Periodicity periodicity)
        {
            Value = value;
            Periodicity = periodicity;
        }
    }

    public class RelativeDiscount : Record<RelativeDiscount>
    {
        public readonly decimal Value;

        public RelativeDiscount(decimal value)
        {
            Value = value;
        }
    }

    public class Product : Record<Product>
    {
        public readonly ProductName Name;
        public readonly ProductPrice Price;
        public readonly Either<AbsoluteDiscount, RelativeDiscount> Discount;

        public Product(ProductName name, ProductPrice price, Either<AbsoluteDiscount, RelativeDiscount> discount)
        {
            Name = name;
            Price = price;
            Discount = discount;
        }

        public ProductPrice GetFinalPrice()
        {
            var value = Discount.Match(
                relativeDiscount => CalculateRelativeDiscount(Price, relativeDiscount),
                absoluteDiscount => CalculateAbsoluteDiscount(Price, absoluteDiscount)
            );

            return new ProductPrice(value);
        }

        private decimal CalculateAbsoluteDiscount(ProductPrice price, AbsoluteDiscount absoluteDiscount)
        {
            decimal totalDiscount;

            switch (absoluteDiscount.Periodicity)
            {
                case Periodicity.Monthly:
                    totalDiscount = absoluteDiscount.Value * 12;
                    break;
                case Periodicity.Quarter:
                    totalDiscount = absoluteDiscount.Value * 4;
                    break;
                case Periodicity.Biannual:
                    totalDiscount = absoluteDiscount.Value * 2;
                    break;
                case Periodicity.Annual:
                    totalDiscount = absoluteDiscount.Value * 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return price.Value - totalDiscount;
        }

        private decimal CalculateRelativeDiscount(ProductPrice price, RelativeDiscount relativeDiscount)
        {
            var discount = price.Value * relativeDiscount.Value / 100M;
            return price.Value - discount;
        }
    }

    public class ProductName : NewType<ProductName, string> { public ProductName(string name) : base(name) { } } // To create Records with just one field
    public class ProductPrice : FloatType<ProductPrice, TDecimal, decimal> { public ProductPrice(decimal value) : base(value) {} }

    public enum Periodicity
    {
        Monthly,
        Quarter,
        Biannual,
        Annual
    }
}
