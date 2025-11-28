using Sample.Domain.Models;
using System.Reflection;

namespace Sample.Infraestructure._shared
{
    public class Extension<T, TKey> where T : class, IEntity<TKey>
    {
        public static string GetParameters(T entity)
        {
            List<object> values = [];
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var item in properties)
            {
                Object? value = item.GetValue(entity) ?? null;
                if (value == null)
                    values.Add("NULL");
                else if (item.PropertyType == typeof(string) || item.PropertyType == typeof(Guid) || item.PropertyType == typeof(DateTime))
                    values.Add($"'{value}'");
                else
                    values.Add(value);
            }
            return string.Join(", ", values);
        }
        public static string MapSetClause(T entity)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            return string.Join(" ,", properties.Select(propertyInfo => $"{propertyInfo.Name} = '{propertyInfo.GetValue(entity)}'"));
        }
    }
}
