using FluentAssertions;
using Xunit;

namespace FunctionalCSharp.Tests
{
    public class InvoiceTaxerTests
    {
        [Theory]
        [InlineData(Region.Europe, ClientType.Common, SeasonType.Low, 1.25)]
        [InlineData(Region.Asia, ClientType.Common, SeasonType.Low, 1.25)]
        [InlineData(Region.USA, ClientType.Common, SeasonType.Low, 1.25)]
        [InlineData(Region.Other, ClientType.Common, SeasonType.Low, 1.25)]
        [InlineData(Region.Europe, ClientType.Premium, SeasonType.Low, 1.0)]
        [InlineData(Region.Asia, ClientType.Premium, SeasonType.Low, 1.0)]
        [InlineData(Region.USA, ClientType.Premium, SeasonType.Low, 1.0)]
        [InlineData(Region.Other, ClientType.Premium, SeasonType.Low, 1.0)]
        [InlineData(Region.Europe, ClientType.Common, SeasonType.Peak, 1.5)]
        [InlineData(Region.Asia, ClientType.Common, SeasonType.Peak, 2.0)]
        [InlineData(Region.USA, ClientType.Common, SeasonType.Peak, 1.5)]
        [InlineData(Region.Other, ClientType.Common, SeasonType.Peak, 2.0)]
        [InlineData(Region.Europe, ClientType.Premium, SeasonType.Peak, 1.25)]
        [InlineData(Region.Asia, ClientType.Premium, SeasonType.Peak, 1.25)]
        [InlineData(Region.USA, ClientType.Premium, SeasonType.Peak, 1.25)]
        [InlineData(Region.Other, ClientType.Premium, SeasonType.Peak, 1.25)]
        public void CalculateMultiplier(Region region, ClientType clientType, SeasonType seasonType, double expected)
        {
            var actual = InvoiceTaxer.CalculatePriceMultiplier(region, clientType, seasonType);

            actual.Should().Be(expected);
        }
    }
}
