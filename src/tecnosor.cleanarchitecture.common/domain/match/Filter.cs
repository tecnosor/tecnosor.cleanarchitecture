namespace tecnosor.cleanarchitecture.common.domain.match;

public class Filter<TAggregate>
{
    public string Field { get; }
    [JsonConverter(typeof(CriteriaConverter))]
    public Criteria Operation { get; }

    public object Value { get; }

    private readonly Dictionary<Criteria, List<Type>> _validCriterias = new Dictionary<Criteria, List<Type>>
    {
        { Criteria.LOWEREQUALS, new List<Type> { typeof(DateTime), typeof(long), typeof(int), typeof(Int32), typeof(Int64), typeof(Int128), typeof(decimal), typeof(double), typeof(float) } },
        { Criteria.LOWER, new List<Type> { typeof(DateTime), typeof(long), typeof(int), typeof(Int32), typeof(Int64), typeof(Int128), typeof(decimal), typeof(double), typeof(float) } },
        { Criteria.HIGHEREQUALS, new List<Type> { typeof(DateTime), typeof(long), typeof(int), typeof(Int32), typeof(Int64), typeof(Int128), typeof(decimal), typeof(double), typeof(float) } },
        { Criteria.HIGHER, new List<Type> { typeof(DateTime), typeof(long), typeof(int), typeof(Int32), typeof(Int64), typeof(Int128), typeof(decimal), typeof(double), typeof(float) } },
        { Criteria.EQUALS, new List<Type> { typeof(DateTime), typeof(long), typeof(int), typeof(Int32), typeof(Int64), typeof(Int128), typeof(decimal), typeof(string), typeof(double), typeof(float) } },
        { Criteria.CONTAINS, new List<Type> { typeof(string) } }
    };

    public Filter(string field, Criteria operation, object value)
    {
        if (value is null) throw new ArgumentNullException($"value of {field} cannot be empty");

        ValidateIsValidType(value.GetType());

        var type = GetFieldType(field);
        if (type != value.GetType()) throw new ArgumentException($"value of {field} is not the same type than the comparable value given");

        ValidateOperationBasedOnTypes(type, operation);

        Field = field;
        Operation = operation;
        Value = value;
    }

    private Type GetFieldType(string field)
    {
        var deep = field.Split('.');
        Type type = typeof(TAggregate);
        PropertyInfo fieldInfo = null;

        foreach (var level in deep)
        {
            fieldInfo = type.GetProperty(level, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo == null) throw new ArgumentException($"Field '{level}' not found in type '{type.FullName}'.");
            type = fieldInfo.PropertyType;
        }
        return type;
    }

    private void ValidateIsValidType(Type type)
    {
        if (!IsValidType(type)) throw new ArgumentException("Filter value is not a valid type");
    }

    private void ValidateOperationBasedOnTypes(Type type, Criteria operation)
    {
        if (!IsValidCriteria(type, operation)) throw new ArgumentException($"Operation '{operation}' is not valid for type '{type.FullName}'");
    }
    private bool IsValidType(Type type) => type.IsPrimitive || type == typeof(decimal) || type == typeof(DateTime) || type == typeof(string);
    private bool IsValidCriteria(Type type, Criteria operation) => (_validCriterias.TryGetValue(operation, out var validTypes) || !validTypes.Contains(type));
}
