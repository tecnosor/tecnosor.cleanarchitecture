namespace tecnosor.cleanarchitecture.common.domain.match;

public enum Criteria
{
    HIGHER,
    HIGHEREQUALS,
    LOWER,
    LOWEREQUALS,
    EQUALS,
    CONTAINS
}
public class CriteriaConverter : JsonConverter<Criteria>
{
    public override Criteria Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string value = reader.GetString();
        return value switch
        {
            ">" => Criteria.HIGHER,
            ">=" => Criteria.HIGHEREQUALS,
            "<" => Criteria.LOWER,
            "<=" => Criteria.LOWEREQUALS,
            "=" => Criteria.EQUALS,
            "contains" => Criteria.CONTAINS,
            _ => throw new ArgumentException("Invalid criteria value")
        };
    }

    public override void Write(Utf8JsonWriter writer, Criteria value, JsonSerializerOptions options)
    {
        string stringValue = value switch
        {
            Criteria.HIGHER => ">",
            Criteria.HIGHEREQUALS => ">=",
            Criteria.LOWER => "<",
            Criteria.LOWEREQUALS => "<=",
            Criteria.EQUALS => "=",
            Criteria.CONTAINS => "contains",
            _ => throw new ArgumentException("Invalid criteria value")
        };

        writer.WriteStringValue(stringValue);
    }
}