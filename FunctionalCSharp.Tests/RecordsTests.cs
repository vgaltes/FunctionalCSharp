using FluentAssertions;
using LanguageExt;
using Xunit;

namespace FunctionalCSharp.Tests
{
    public class RecordsTests
    {
        [Fact]
        public void TwoRecordsWithTheSameValueAreEqual()
        {
            var recordA = new BavelId(4);
            var recordB = new BavelId(4);

            recordA.Should().Be(recordB);
        }

        [Fact]
        public void TwoRecordsWithTheDifferntValueAreNotEqual()
        {
            var recordA = new BavelId(4);
            var recordB = new BavelId(5);
            
            recordA.Should().NotBe(recordB);
        }

        [Fact]
        public void CanRemoveAFieldFromTheEqualityComparision()
        {
            var recordA = new SomeRecord(4, 6);
            var recordB = new SomeRecord(4, 8);

            recordA.Should().Be(recordB);
        }

        [Fact]
        public void CanCreateANewInmmutableRecord()
        {
            var recordA = new SomeRecord(4, 6);
            var recordB = new SomeRecord(6, 8);

            recordA.Should().NotBe(recordB);

            var recordC = recordB.With(value:4);

            recordC.Should().Be(recordA);
        }
    }

    public class BavelId : Record<BavelId>
    {
        public readonly int Value;

        public BavelId(int value)
        {
            Value = value;
        }
    }

    public class SomeRecord : Record<SomeRecord>
    {
        public readonly int Value;

        [NonRecord] public readonly int AnotherField;

        public SomeRecord(int value, int anotherField)
        {
            Value = value;
            AnotherField = anotherField;
        }

        public SomeRecord With(int? value = null, int? anotherField = null) =>
            new SomeRecord(
                value ?? Value,
                anotherField ?? AnotherField
            );
    }
}
