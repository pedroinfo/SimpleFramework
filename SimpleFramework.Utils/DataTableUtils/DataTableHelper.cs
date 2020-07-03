using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace SimpleFramework.Utils.DataTableUtils
{
    public static class DataTableHelper
    {
        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static IEnumerable<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            var dictionaryProperty = new Dictionary<Type, ICollection<PropertyInfo>>();

            var objType = typeof(T);

            ICollection<PropertyInfo> properties;

            lock (dictionaryProperty)
            {
                if (!dictionaryProperty.TryGetValue(objType, out properties))
                {
                    properties = objType.GetProperties().Where(property => property.CanWrite).ToList();
                    dictionaryProperty.Add(objType, properties);
                }
            }

            var list = new List<T>(table.Rows.Count);

            foreach (var row in table.AsEnumerable().Skip(1))
            {
                var obj = new T();

                foreach (var prop in properties)
                {
                    var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    var safeValue = row[prop.Name] == null ? null : Convert.ChangeType(row[prop.Name], propType);

                    prop.SetValue(obj, safeValue, null);
                }

                list.Add(obj);
            }

            return list;
        }

        public static DataTable ConvertTo<T>(List<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }
                table.Rows.Add(row);
            }
            return table;
        }

        private static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            return table;
        }

    }
}


