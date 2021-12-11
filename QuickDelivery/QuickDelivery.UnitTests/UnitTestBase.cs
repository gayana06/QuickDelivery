using AutoFixture;

namespace QuickDelivery.UnitTests
{
    public class UnitTestBase
    {
        protected readonly Fixture _fixture;

        protected UnitTestBase()
        {
            _fixture = new Fixture();
        }
    }
}
