using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordGenerator;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestPasswordGenerateService
{

    [TestClass]
    public class TestPasswordGeneratorBuilder
    {
        [TestMethod]
        public void PasswordLengthEqualToSpecific()
        {
            int passLength = 10;

            var builder = new PasswordGeneratorBuilder();
            var generator = builder
                .SetRequiredLength(passLength)
                .Build();

            var password = generator.Generate();

            password.Length.Should().Be(passLength);
        }
    }

    [TestClass]
    public class TestPasswordGenerator
    {
        [TestMethod]
        public void TestDivide()
        {
            var builder = new PasswordGeneratorBuilder();
            var gen = (PasswordGenerator.PasswordGenerator)builder.Build();

            var divided = gen.Divide(10, 4);

            divided.Count.Should().Be(4);
            divided.Sum().Should().Be(10);

            divided = gen.Divide(3, 3);
            divided.Count.Should().Be(3);
            divided.Sum().Should().Be(3);

            Action action = () => gen.Divide(3, 4);
            action.Should().Throw<ArgumentException>();

            action = () => gen.Divide(3, 0);
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void TestPickupCharsFromString()
        {
            string s = "abcdefghijklmn";

            var builder = new PasswordGeneratorBuilder();
            var gen = (PasswordGenerator.PasswordGenerator)builder.Build();

            string result = gen.PickupCharsFromString(s, 3);
            result.Length.Should().Be(3);

            var results = Enumerable.Range(0, 10).Select(i => gen.PickupCharsFromString(s, 3));
            results.GroupBy(x => x).Count().Should().BeGreaterThan(1);

            Action action = () => gen.PickupCharsFromString(s, 10);
            action.Should().NotThrow();
        }

        [TestMethod]
        public void TestShuffle()
        {
            var builder = new PasswordGeneratorBuilder();
            var gen = (PasswordGenerator.PasswordGenerator)builder.Build();

            string shuffled = gen.Shuffle("abcde");

            shuffled.Should().NotBe("abcde");
        }

        [TestMethod]
        public void SetOnlyDigit()
        {
            var gen = new PasswordGeneratorBuilder()
                .SetRequireDigit(true)
                .SetRequireLowercase(false)
                .SetRequireUppercase(false)
                .SetRequireNonAlphanumeric(false)
                .Build();

            gen.Generate().Should().MatchRegex(@"\d+");
        }

        [TestMethod]
        public void SetOnlyLowercase()
        {
            var gen = new PasswordGeneratorBuilder()
                .SetRequireDigit(false)
                .SetRequireLowercase(true)
                .SetRequireUppercase(false)
                .SetRequireNonAlphanumeric(false)
                .Build();

            gen.Generate().Should().MatchRegex(@"[a-z]+");
        }

        [TestMethod]
        public void SetOnlyUppercase()
        {
            var gen = new PasswordGeneratorBuilder()
                .SetRequireDigit(false)
                .SetRequireLowercase(false)
                .SetRequireUppercase(true)
                .SetRequireNonAlphanumeric(false)
                .Build();

            gen.Generate().Should().MatchRegex(@"[A-Z]+");
        }

        [TestMethod]
        public void SetOnlyNonAlphaNumeric()
        {
            var gen = new PasswordGeneratorBuilder()
                .SetRequireDigit(false)
                .SetRequireLowercase(false)
                .SetRequireUppercase(false)
                .SetRequireNonAlphanumeric(true)
                .Build();

            gen.Generate().Should().MatchRegex(@"[^a-zA-Z0-9]+");
        }

        [TestMethod]
        public void SetAllFalse()
        {
            var builder = new PasswordGeneratorBuilder()
                .SetRequireDigit(false)
                .SetRequireLowercase(false)
                .SetRequireUppercase(false)
                .SetRequireNonAlphanumeric(false);

            var gen = builder.Build();

            gen.Generate().Should().MatchRegex(@"\d+");
            gen.Generate().Should().MatchRegex(@"[a-z]+");
            gen.Generate().Should().MatchRegex(@"[A-Z]+");
            gen.Generate().Should().MatchRegex(@"[^a-zA-Z0-9]+");
        }
    }
}
