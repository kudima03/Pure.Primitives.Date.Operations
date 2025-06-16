using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Abstractions.Date;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Materialized.Date;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Date;

namespace Pure.Primitives.Date.Operations.Tests;

public sealed record NotAfterConditionTests
{
    [Fact]
    public void TakesNegativeResultOnUnorderedLast()
    {
        INumber<ushort> year = new UShort(2000);
        INumber<ushort> month = new UShort(2);

        IBool condition = new NotAfterCondition(
            new Date(new UShort(1), month, year),
            new Date(new UShort(3), month, year),
            new Date(new UShort(2), month, year));

        Assert.False(condition.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnOrderedRandoms()
    {
        IEnumerable<IDate> randomDates = new RandomDateCollection(new UShort(1000))
            .Select(x => new MaterializedDate(x).Value)
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ThenBy(x => x.Day)
            .Select(x => new Date(x));

        IBool condition = new NotAfterCondition(randomDates);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnSameValues()
    {
        IBool condition = new NotAfterCondition(new CurrentDate(), new CurrentDate(), new CurrentDate());
        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnAllAscendingOneSameValue()
    {
        IBool condition = new NotAfterCondition(
            new Date(new UShort(1), new UShort(1), new UShort(2000)),
            new Date(new UShort(1), new UShort(1), new UShort(2001)),
            new Date(new UShort(1), new UShort(1), new UShort(2002)),
            new Date(new UShort(1), new UShort(1), new UShort(2002)));

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnSingleElementInCollection()
    {
        IBool condition = new NotAfterCondition(new CurrentDate());
        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyCollection()
    {
        IBool condition = new NotAfterCondition();
        Assert.Throws<ArgumentException>(() => condition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyArguments()
    {
        IBool condition = new NotAfterCondition();
        Assert.Throws<ArgumentException>(() => condition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new NotAfterCondition(new CurrentDate()).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new NotAfterCondition(new CurrentDate()).ToString());
    }
}