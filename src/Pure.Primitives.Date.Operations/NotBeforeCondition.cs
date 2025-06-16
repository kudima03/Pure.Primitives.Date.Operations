using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Abstractions.Date;

namespace Pure.Primitives.Date.Operations;

public sealed record NotBeforeCondition : IBool
{
    private readonly IEnumerable<IDate> _values;

    public NotBeforeCondition(params IDate[] values) : this(values.AsReadOnly()) { }

    public NotBeforeCondition(IEnumerable<IDate> values)
    {
        _values = values;
    }

    bool IBool.BoolValue
    {
        get
        {
            if (!_values.Any())
            {
                throw new ArgumentException();
            }

            IEnumerable<DateOnly> dates =
                _values.Select(x => new DateOnly(
                    x.Year.NumberValue,
                    x.Month.NumberValue,
                    x.Day.NumberValue));

            DateOnly prev = DateOnly.MaxValue;

            foreach (DateOnly date in dates)
            {
                if (prev < date)
                {
                    return false;
                }

                prev = date;
            }

            return true;
        }
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}