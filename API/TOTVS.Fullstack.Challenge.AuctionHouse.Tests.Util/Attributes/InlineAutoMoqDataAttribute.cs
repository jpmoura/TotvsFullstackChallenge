using AutoFixture.Xunit2;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes
{
    public class InlineAutoMoqDataAttribute : CompositeDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] values) : base(new InlineDataAttribute(values), new AutoMoqDataAttribute())
        {
        }
    }
}
