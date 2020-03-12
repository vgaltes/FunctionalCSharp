using System.ComponentModel;
using FluentAssertions;
using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using Xunit;
using static LanguageExt.Prelude;

namespace FunctionalCSharp.Tests
{
    public class OptionTests
    {
        [Fact]
        public void SomeAndNone()
        {
            var a = GetValue(true);

            a.IsSome.Should().BeTrue();
            a.IsNone.Should().BeFalse();

            a = GetValue(false);

            a.IsSome.Should().BeFalse();
            a.IsNone.Should().BeTrue();
        }

        [Fact]
        public void ImplicitConversion()
        {
            var a = GetSome();

            a.IsSome.Should().BeTrue();

            a.ValueUnsafe().Should().Be(1000); // Just for testing. Don't use this in production.

            a = GetNone();
            a.IsNone.Should().BeTrue();
        }

        [Fact]
        public void UseMatch()
        {
            const int defaultValue = 42;
            
            var a = GetValue(true);
            var someValue = a.Match(x => x, () => defaultValue);
            someValue.Should().Be(1000);

            someValue = a.Some(x => x).None(defaultValue);
            someValue.Should().Be(1000);

            someValue = match(a, x => x, () => defaultValue);
            someValue.Should().Be(1000);

            a = GetValue(false);
            someValue = a.Match(x => x, () => defaultValue);
            someValue.Should().Be(defaultValue);

            someValue = a.Some(x => x).None(defaultValue);
            someValue.Should().Be(defaultValue);

            someValue = match(a, x => x, () => defaultValue);
            someValue.Should().Be(defaultValue);
        }

        Option<int> GetSome()
        {
            return 1000;
        }

        // Implicitly converts to a None of int
        Option<int> GetNone()
        {
            return None;
        }

        // Will handle either a None or a Some returned
        Option<int> GetValue(bool select) =>
            select
                ? Some(1000)
                : None;
    }
}
