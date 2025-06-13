using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Abstractions.Date;
using Pure.Primitives.Bool.Operations;
using Pure.Primitives.Number.Operations;

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
            IBool condition = new Or(
                new GreaterThanCondition<ushort>(_values.Select(x => x.Year)),
                new GreaterThanCondition<ushort>(_values.Select(x => x.Month)),
                new GreaterThanCondition<ushort>(_values.Select(x => x.Day)));

            return condition.BoolValue;
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