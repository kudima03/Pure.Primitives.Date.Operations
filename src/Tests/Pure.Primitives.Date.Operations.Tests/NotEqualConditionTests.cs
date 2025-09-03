using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Date;

namespace Pure.Primitives.Date.Operations.Tests;

public sealed record NotNotEqualConditionTests
{
    [Fact]
    public void TakesNegativeResultOnSameValues()
    {
        IBool equality = new NotEqualCondition(
            new CurrentDate(),
            new CurrentDate(),
            new CurrentDate(),
            new CurrentDate(),
            new CurrentDate()
        );

        Assert.False(equality.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnTwoSameValues()
    {
        IBool equality = new NotEqualCondition(new CurrentDate(), new CurrentDate());

        Assert.False(equality.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnDifferentValues()
    {
        IBool equality = new NotEqualCondition(new RandomDateCollection(new UShort(10)));

        Assert.True(equality.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnAllSameOneDifferentValue()
    {
        IBool equality = new NotEqualCondition(
            new CurrentDate(),
            new CurrentDate(),
            new CurrentDate(),
            new CurrentDate(),
            new CurrentDate(),
            new RandomDate()
        );

        Assert.True(equality.BoolValue);
    }

    [Fact]
    public void ProduceFalseOnSingleElementInCollection()
    {
        IBool equality = new NotEqualCondition(new CurrentDate());
        Assert.False(equality.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyArguments()
    {
        IBool equality = new NotEqualCondition();
        _ = Assert.Throws<ArgumentException>(() => equality.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new NotEqualCondition(new CurrentDate()).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new NotEqualCondition(new CurrentDate()).ToString()
        );
    }
}
