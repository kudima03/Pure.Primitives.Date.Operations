using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Abstractions.Date;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Materialized.Date;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Date;

namespace Pure.Primitives.Date.Operations.Tests;

public sealed record NotBeforeConditionTests
{
    [Fact]
    public void TakesNegativeResultOnUnorderedLast()
    {
        INumber<ushort> year = new UShort(2000);
        INumber<ushort> month = new UShort(2);

        IBool condition = new NotBeforeCondition(
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
            .OrderByDescending(x => x.Year)
            .ThenByDescending(x => x.Month)
            .ThenByDescending(x => x.Day)
            .Select(x => new Date(x));

        IBool condition = new NotBeforeCondition(randomDates);

        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnSameValues()
    {
        IBool condition = new NotBeforeCondition(new CurrentDate(), new CurrentDate(), new CurrentDate());
        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnAllAscendingOneSameValue()
    {
        IBool condition = new NotBeforeCondition(
            new Date(new UShort(1), new UShort(1), new UShort(2000)),
            new Date(new UShort(1), new UShort(1), new UShort(2001)),
            new Date(new UShort(1), new UShort(1), new UShort(2002)),
            new Date(new UShort(1), new UShort(1), new UShort(2002)));

        Assert.False(condition.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnSingleElementInCollection()
    {
        IBool condition = new NotBeforeCondition(new CurrentDate());
        Assert.True(condition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyCollection()
    {
        IBool condition = new NotBeforeCondition();
        Assert.Throws<ArgumentException>(() => condition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyArguments()
    {
        IBool condition = new NotBeforeCondition();
        Assert.Throws<ArgumentException>(() => condition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new NotBeforeCondition(new CurrentDate()).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new NotBeforeCondition(new CurrentDate()).ToString());
    }
}