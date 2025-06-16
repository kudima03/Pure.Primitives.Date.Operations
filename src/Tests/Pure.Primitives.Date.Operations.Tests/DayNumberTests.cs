using Pure.Primitives.Abstractions.Date;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Materialized.Date;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Date;

namespace Pure.Primitives.Date.Operations.Tests;

public sealed record DayNumberTests
{
    [Fact]
    public void ProduceValidDayNumbers()
    {
        IEnumerable<IDate> randomDates = new RandomDateCollection(new UShort(1000)).ToArray();
        IEnumerable<DateOnly> materializedDates = randomDates.Select(x => new MaterializedDate(x).Value);
        IEnumerable<int> dayNumbers = randomDates.Select(x => new DayNumber(x))
            .Cast<INumber<uint>>()
            .Select(x => (int)x.NumberValue);

        Assert.Equal(materializedDates.Select(x => x.DayNumber), dayNumbers);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        Assert.Throws<NotSupportedException>(() => new DayNumber(new CurrentDate()).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        Assert.Throws<NotSupportedException>(() => new DayNumber(new CurrentDate()).ToString());
    }
}