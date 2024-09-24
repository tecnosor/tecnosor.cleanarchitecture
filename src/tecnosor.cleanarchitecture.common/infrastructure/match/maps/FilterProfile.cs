using AutoMapper;
using tecnosor.cleanarchitecture.common.domain.match;

namespace tecnosor.cleanarchitecture.common.infrastructure.match.maps;

public class FilterProfile : Profile
{
    public FilterProfile()
    {
        CreateMap<RestFilter, Filter<object>>()
            .ConstructUsing((src, ctx) =>
            {
                var operation = Enum.Parse<Criteria>(src.Operation, ignoreCase: true);
                var value = ParseValue(src.Value);

                return new Filter<object>(src.Field, operation, value);
            });
    }

    private object ParseValue(string value)
    {
        if (int.TryParse(value, out var intValue)) return intValue;
        if (long.TryParse(value, out var longValue)) return longValue;
        if (decimal.TryParse(value, out var decimalValue)) return decimalValue;
        if (double.TryParse(value, out var doubleValue)) return doubleValue;
        if (float.TryParse(value, out var floatValue)) return floatValue;
        if (DateTime.TryParse(value, out var dateValue)) return dateValue;
        // Devuelve el valor como string si no coincide con ningún tipo numérico o fecha.
        return value;
    }
}
