using Pure.Primitives.Abstractions.Date;
using Pure.Primitives.Abstractions.Number;

namespace Pure.Primitives.Date.Operations;

public sealed record DayNumber : INumber<uint>
{
    private readonly IDate _date;

    public DayNumber(IDate date)
    {
        _date = date;
    }

    uint INumber<uint>.NumberValue =>
        (uint)
            new DateOnly(
                _date.Year.NumberValue,
                _date.Month.NumberValue,
                _date.Day.NumberValue
            ).DayNumber;

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
