using NUnit.Framework;

namespace NUnit
{

    /* 
     * 1. Install Nunit from Nuget Package
     * 2. Go to "Tools -> Extensions and Updates -> Online" and search for "NUnit3 Test Adapter" and then install to show testcase in Test Explorer
     */
    [TestFixture]
    public class SomeBusinessTest
    {
        private SomeBusiness sb = new SomeBusiness();

        [Test]
        public void GetSomeNumberTest()
        {
            Assert.That(sb.GetSomeNumber(), Is.EqualTo(99));
        }

        [Test]
        public void GetTheMostHandsomeManTest()
        {
            Assert.That(sb.GetTheMostHandsomeMan(), Is.EqualTo("Minh"));
        }
    }
}