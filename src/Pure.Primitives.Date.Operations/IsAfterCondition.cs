using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Abstractions.Date;

namespace Pure.Primitives.Date.Operations;

public sealed record IsAfterCondition : IBool
{
    private readonly IEnumerable<IDate> _values;

    public IsAfterCondition(params IDate[] values) : this(values.AsReadOnly()) { }

    public IsAfterCondition(IEnumerable<IDate> values)
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

            using IEnumerator<DateOnly> dates = _values.Select(x =>
                new DateOnly(x.Year.NumberValue, x.Month.NumberValue, x.Day.NumberValue)).GetEnumerator();

            dates.MoveNext();

            DateOnly prev = dates.Current;

            while (dates.MoveNext())
            {
                if (prev <= dates.Current)
                {
                    return false;
                }
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