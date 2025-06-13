using Pure.Primitives.Abstractions.Bool;
using Pure.Primitives.Abstractions.Date;
using Pure.Primitives.Bool;
using Pure.Primitives.Choices.Bool;
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
            IBool result =
                new BoolChoice(
                    new GreaterThanCondition<ushort>(_values.Select(x => x.Year)),
                    new True(),
                    new BoolChoice(
                        new NotEqualCondition<ushort>(_values.Select(x => x.Year)),
                        new False(),
                        new BoolChoice(new GreaterThanCondition<ushort>(_values.Select(x => x.Month)),
                            new True(),
                            new BoolChoice(new NotEqualCondition<ushort>(_values.Select(x => x.Month)),
                                new False(),
                                new BoolChoice(new GreaterThanCondition<ushort>(_values.Select(x => x.Day)),
                                    new True(),
                                    new False())))));

            return result.BoolValue;
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