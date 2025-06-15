using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Abstractions.Date;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Materialized.Date;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Date;

namespace Pure.Primitives.Date.Operations.Tests;

public sealed record BeforeConditionTests
{
    [Fact]
    public void TakesNegativeResultOnUnorderedLast()
    {
        INumber<ushort> year = new UShort(2000);
        INumber<ushort> month = new UShort(2);

        IBool isGreaterThan = new BeforeCondition(
            new Date(new UShort(1), month, year),
            new Date(new UShort(3), month, year),
            new Date(new UShort(2), month, year));

        Assert.False(isGreaterThan.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnOrderedRandoms()
    {
        IEnumerable<IDate> randomDates = new RandomDateCollection(new UShort(1000))
            .Select(x => new MaterializedDate(x).Value)
            .Distinct()
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ThenBy(x => x.Day)
            .Select(x => new Date(x));

        IBool before = new BeforeCondition(randomDates);

        Assert.True(before.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnSameValues()
    {
        IBool before = new BeforeCondition(new CurrentDate(), new CurrentDate(), new CurrentDate());
        Assert.False(before.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnAllAscendingAndDescendingDays()
    {
        IBool before = new BeforeCondition(
            new Date(new UShort(3), new UShort(1), new UShort(2000)),
            new Date(new UShort(2), new UShort(2), new UShort(2001)),
            new Date(new UShort(1), new UShort(3), new UShort(2002)));

        Assert.True(before.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnAllAscendingAndDescendingMonths()
    {
        IBool before = new BeforeCondition(
            new Date(new UShort(1), new UShort(3), new UShort(2000)),
            new Date(new UShort(2), new UShort(2), new UShort(2001)),
            new Date(new UShort(3), new UShort(1), new UShort(2002)));

        Assert.True(before.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnAllAscendingAndDescendingYears()
    {
        IBool isBeforeCondition = new BeforeCondition(
            new Date(new UShort(1), new UShort(1), new UShort(2002)),
            new Date(new UShort(2), new UShort(2), new UShort(2001)),
            new Date(new UShort(3), new UShort(3), new UShort(2000)));

        Assert.False(isBeforeCondition.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnAscendingDays()
    {
        INumber<ushort> year = new UShort(2000);
        INumber<ushort> month = new UShort(2);

        IBool isBeforeCondition = new BeforeCondition(
            new Date(new UShort(1), month, year),
            new Date(new UShort(2), month, year),
            new Date(new UShort(3), month, year));

        Assert.True(isBeforeCondition.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnAscendingMonths()
    {
        INumber<ushort> year = new UShort(2000);

        IBool isBeforeCondition = new BeforeCondition(
            new Date(new UShort(1), new UShort(1), year),
            new Date(new UShort(1), new UShort(2), year),
            new Date(new UShort(1), new UShort(3), year));

        Assert.True(isBeforeCondition.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnAscendingYears()
    {
        IBool isBeforeCondition = new BeforeCondition(
            new Date(new UShort(1), new UShort(1), new UShort(2000)),
            new Date(new UShort(1), new UShort(1), new UShort(2001)),
            new Date(new UShort(1), new UShort(1), new UShort(2002)));

        Assert.True(isBeforeCondition.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnDescendingDays()
    {
        INumber<ushort> year = new UShort(2000);
        INumber<ushort> month = new UShort(2);

        IBool isBeforeCondition = new BeforeCondition(
            new Date(new UShort(3), month, year),
            new Date(new UShort(2), month, year),
            new Date(new UShort(1), month, year));

        Assert.False(isBeforeCondition.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnDescendingMonths()
    {
        INumber<ushort> year = new UShort(2000);

        IBool isBeforeCondition = new BeforeCondition(
            new Date(new UShort(1), new UShort(3), year),
            new Date(new UShort(1), new UShort(2), year),
            new Date(new UShort(1), new UShort(1), year));

        Assert.False(isBeforeCondition.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnDescendingYears()
    {
        IBool isBeforeCondition = new BeforeCondition(
            new Date(new UShort(1), new UShort(1), new UShort(2003)),
            new Date(new UShort(1), new UShort(1), new UShort(2002)),
            new Date(new UShort(1), new UShort(1), new UShort(2001)));

        Assert.False(isBeforeCondition.BoolValue);
    }

    [Fact]
    public void TakesNegativeResultOnAllAscendingOneSameValue()
    {
        IBool isBeforeCondition = new BeforeCondition(
            new Date(new UShort(1), new UShort(1), new UShort(2000)),
            new Date(new UShort(1), new UShort(1), new UShort(2001)),
            new Date(new UShort(1), new UShort(1), new UShort(2002)),
            new Date(new UShort(1), new UShort(1), new UShort(2002)));

        Assert.False(isBeforeCondition.BoolValue);
    }

    [Fact]
    public void TakesPositiveResultOnSingleElementInCollection()
    {
        IBool isBeforeCondition = new BeforeCondition(new CurrentDate());
        Assert.True(isBeforeCondition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyCollection()
    {
        IBool isBeforeCondition = new BeforeCondition();
        Assert.Throws<ArgumentException>(() => isBeforeCondition.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnEmptyArguments()
    {
        IBool equality = new BeforeCondition();
        Assert.Throws<ArgumentException>(() => equality.BoolValue);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new BeforeCondition(new CurrentDate()).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new BeforeCondition(new CurrentDate()).ToString());
    }
}