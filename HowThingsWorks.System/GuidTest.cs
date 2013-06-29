using System;
using NUnit.Framework;

namespace HowThingsWorks.System
{
    class GuidTest
    {
        [Test]
        public void CreatingNewGuids()
        {
            Guid empty = new Guid();
            Assert.AreEqual(Guid.Empty, empty);

            Guid notEmpty = Guid.NewGuid();
            Assert.AreNotEqual(Guid.Empty, notEmpty);
        }

        [Test]
        public void CreatingNewGuidsFromStrings()
        {
            var noDashes = new Guid("030B4A821B7C11CF9D5300AA003C9CB6");
            var withDashes = new Guid("030B4A82-1B7C-11CF-9D53-00AA003C9CB6");

            Assert.AreEqual(noDashes, withDashes);
        }

        [Test]
        [ExpectedException(ExpectedException = typeof(FormatException))]
        public void CreatingGuidWithTooManyDashesThrowsException()
        {
            var withManyDashes = new Guid("030B-4A82-1B7C-11CF-9D53-00AA-003C-9CB6");

            Assert.Fail("No expection thrown");
        }
    }
}
