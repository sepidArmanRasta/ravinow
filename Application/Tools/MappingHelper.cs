using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Tools
{
    public class MappingHelper
    {
        public static T1 Map<T1>(object? obj)
            where T1 : class, new()
        {
            if (obj == null)
            {
                return new T1();
            }
            var serial = JsonSerializer.Serialize(obj, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            });
            var result = JsonSerializer.Deserialize<T1>(serial)!;
            return result;
        }
    }
}
