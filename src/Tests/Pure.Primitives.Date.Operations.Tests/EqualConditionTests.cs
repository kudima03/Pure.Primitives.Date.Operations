using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Date;

namespace Pure.Primitives.Date.Operations.Tests;

public sealed record EqualConditionTests
{
    [Fact]
    public void TakesPositiveResultOnSameValues()
    {
        IBool equality = new EqualCondition(
            new CurrentDate(),
            new CurrentDate(),
            new CurrentDate(),
            new CurrentDate(),
            new CurrentDate()
        );

        Assert.True(equality.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnTwoSameValues()
    {
        IBool equality = new EqualCondition(new CurrentDate(), new CurrentDate());

        Assert.True(equality.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnDifferentValues()
    {
        IBool equality = new EqualCondition(new RandomDateCollection(new UShort(10)));

        Assert.False(equality.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnAllSameOneDifferentValue()
    {
        IBool equality = new EqualCondition(
            new CurrentDate(),
            new CurrentDate(),
            new CurrentDate(),
            new CurrentDate(),
            new CurrentDate(),
            new RandomDate()
        );

        Assert.False(equality.BoolValue);
    }

    [Fact]
    public void ProduceTrueOnSingleElementInCollection()
    {
        IBool equality = new EqualCondition(new CurrentDate());
        Assert.True(equality.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyArguments()
    {
        IBool equality = new EqualCondition();
        _ = Assert.Throws<ArgumentException>(() => equality.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new EqualCondition(new CurrentDate()).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new EqualCondition(new CurrentDate()).ToString()
        );
    }
}
