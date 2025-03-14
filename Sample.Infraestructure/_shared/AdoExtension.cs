using Oracle.ManagedDataAccess.Client;
using Sample.Domain.Models;
using System.Reflection;

namespace Sample.Infraestructure._shared
{
    public class AdoExtension<T, TKey> where T : class, IEntity<TKey>
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
        public static T MapEntity(OracleDataReader reader)
        {
            T entity = Activator.CreateInstance<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                if (reader[properties[i].Name] == DBNull.Value)
                    properties[i].SetValue(entity, null);

                else
                {
                    object value = reader[properties[i].Name];

                    if (properties[i].PropertyType == typeof(Guid))
                        properties[i].SetValue(entity, Guid.Parse(value.ToString()!));
                    else
                        properties[i].SetValue(entity, Convert.ChangeType(value, Nullable.GetUnderlyingType(properties[i].PropertyType) ?? properties[i].PropertyType));
                }
            }
            return entity;
        }
        public static string MapSetClause(T entity)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            return string.Join(" ,", properties.Select(propertyInfo => $"{propertyInfo.Name} = '{propertyInfo.GetValue(entity)}'"));
        }
    }
}
