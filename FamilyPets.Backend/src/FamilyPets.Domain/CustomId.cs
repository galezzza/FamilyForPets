using CSharpFunctionalExtensions;

public abstract class CustomId<T> : ValueObject, IComparable<T>, IComparable
    where T : CustomId<T>
{
    public Guid Value { get; }

    protected CustomId(Guid value)
    {
        Value = value;
    }

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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}
