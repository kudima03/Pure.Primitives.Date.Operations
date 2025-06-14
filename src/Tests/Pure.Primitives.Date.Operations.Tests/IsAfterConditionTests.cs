using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Abstractions.Date;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Materialized.Date;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Date;

namespace Pure.Primitives.Date.Operations.Tests;

public sealed record IsAfterConditionTests
{
    [Fact]
    public void TakesNegativeResultOnUnOrderedLast()
    {
        INumber<ushort> year = new UShort(2000);
        INumber<ushort> month = new UShort(2);

        IBool isGreaterThan = new IsAfterCondition(
            new Date(new UShort(4), month, year),
            new Date(new UShort(2), month, year),
            new Date(new UShort(3), month, year));

        Assert.False(isGreaterThan.BoolValue);
    }
    [Fact]
    public void TakesPositiveResultOnOrderedRandoms()
    {
        IEnumerable<IDate> randomDates = new RandomDateCollection(new UShort(1000))
            .Select(x => new MaterializedDate(x).Value)
            .Distinct()
            .OrderByDescending(x => x.Year)
            .ThenByDescending(x => x.Month)
            .ThenByDescending(x => x.Day)
            .Select(x => new Date(x));

        IBool isAfter = new IsAfterCondition(randomDates);

        Assert.True(isAfter.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnSameValues()
    {
        IBool isGreaterThan = new IsAfterCondition(new CurrentDate(), new CurrentDate(), new CurrentDate());
        Assert.False(isGreaterThan.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnAllAscendingAndDescendingDays()
    {
        IBool isGreaterThan = new IsAfterCondition(
            new Date(new UShort(3), new UShort(1), new UShort(2000)),
            new Date(new UShort(2), new UShort(2), new UShort(2001)),
            new Date(new UShort(1), new UShort(3), new UShort(2002)));

        Assert.False(isGreaterThan.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnAllAscendingAndDescendingMonths()
    {
        IBool isGreaterThan = new IsAfterCondition(
            new Date(new UShort(1), new UShort(3), new UShort(2000)),
            new Date(new UShort(2), new UShort(2), new UShort(2001)),
            new Date(new UShort(3), new UShort(1), new UShort(2002)));

        Assert.False(isGreaterThan.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnAllAscendingAndDescendingYears()
    {
        IBool isGreaterThan = new IsAfterCondition(
            new Date(new UShort(1), new UShort(1), new UShort(2002)),
            new Date(new UShort(2), new UShort(2), new UShort(2001)),
            new Date(new UShort(3), new UShort(3), new UShort(2000)));

        Assert.True(isGreaterThan.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnAscendingDays()
    {
        INumber<ushort> year = new UShort(2000);
        INumber<ushort> month = new UShort(2);

        IBool isGreaterThan = new IsAfterCondition(
            new Date(new UShort(1), month, year),
            new Date(new UShort(2), month, year),
            new Date(new UShort(3), month, year));

        Assert.False(isGreaterThan.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnAscendingMonths()
    {
        INumber<ushort> year = new UShort(2000);

        IBool isGreaterThan = new IsAfterCondition(
            new Date(new UShort(1), new UShort(1), year),
            new Date(new UShort(1), new UShort(2), year),
            new Date(new UShort(1), new UShort(3), year));

        Assert.False(isGreaterThan.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnAscendingYears()
    {
        IBool isGreaterThan = new IsAfterCondition(
            new Date(new UShort(1), new UShort(1), new UShort(2000)),
            new Date(new UShort(1), new UShort(1), new UShort(2001)),
            new Date(new UShort(1), new UShort(1), new UShort(2002)));

        Assert.False(isGreaterThan.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnDescendingDays()
    {
        INumber<ushort> year = new UShort(2000);
        INumber<ushort> month = new UShort(2);

        IBool isGreaterThan = new IsAfterCondition(
            new Date(new UShort(3), month, year),
            new Date(new UShort(2), month, year),
            new Date(new UShort(1), month, year));

        Assert.True(isGreaterThan.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnDescendingMonths()
    {
        INumber<ushort> year = new UShort(2000);

        IBool isGreaterThan = new IsAfterCondition(
            new Date(new UShort(1), new UShort(3), year),
            new Date(new UShort(1), new UShort(2), year),
            new Date(new UShort(1), new UShort(1), year));

        Assert.True(isGreaterThan.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnDescendingYears()
    {
        IBool isGreaterThan = new IsAfterCondition(
            new Date(new UShort(1), new UShort(1), new UShort(2003)),
            new Date(new UShort(1), new UShort(1), new UShort(2002)),
            new Date(new UShort(1), new UShort(1), new UShort(2001)));

        Assert.True(isGreaterThan.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnAllAscendingOneSameValue()
    {
        IBool isGreaterThan = new IsAfterCondition(
            new Date(new UShort(1), new UShort(1), new UShort(2000)),
            new Date(new UShort(1), new UShort(1), new UShort(2001)),
            new Date(new UShort(1), new UShort(1), new UShort(2002)),
            new Date(new UShort(1), new UShort(1), new UShort(2002)));

        Assert.False(isGreaterThan.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnSingleElementInCollection()
    {
        IBool isGreaterThan = new IsAfterCondition(new CurrentDate());
        Assert.True(isGreaterThan.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyCollection()
    {
        IBool isGreaterThan = new IsAfterCondition();
        Assert.Throws<ArgumentException>(() => isGreaterThan.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyArguments()
    {
        IBool equality = new IsAfterCondition();
        Assert.Throws<ArgumentException>(() => equality.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new IsAfterCondition(new CurrentDate()).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new IsAfterCondition(new CurrentDate()).ToString());
    }
}