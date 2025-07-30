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

    bool IBool.BoolValue
    {
        get
        {
            int distinctCount = _values
                .DistinctBy(x => (x.Year.NumberValue, x.Month.NumberValue, x.Day.NumberValue))
                .Count();

            if (distinctCount == 0)
            {
                throw new ArgumentException();
            }

            return distinctCount == 1;
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