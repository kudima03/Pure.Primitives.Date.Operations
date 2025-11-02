using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Abstractions.Date;

namespace Pure.Primitives.Date.Operations;

public sealed record EqualCondition : IBool
{
    private readonly IEnumerable<IDate> _values;

    public EqualCondition(params IEnumerable<IDate> values)
    {
        _values = values;
    }

    public bool BoolValue
    {
        get
        {
            int distinctCount = _values
                .DistinctBy(x =>
                    (x.Year.NumberValue, x.Month.NumberValue, x.Day.NumberValue)
                )
                .Count();

            return distinctCount == 0
                ? throw new ArgumentException()
                : distinctCount == 1;
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
