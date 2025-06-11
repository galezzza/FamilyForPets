using CSharpFunctionalExtensions;

public abstract class CustomId<T> : ComparableValueObject, IComparable<T>, IComparable
    where T : CustomId<T>
{
    protected CustomId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public int CompareTo(T? other)
    {
        if (other is null)
            return 1;
        return Value.CompareTo(other.Value);
    }

    public int CompareTo(object? obj)
    {
        if (obj is T other)
            return CompareTo(other);

        throw new ArgumentException($"Object must be of type {typeof(T).Name}");
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Value;
    }

}
