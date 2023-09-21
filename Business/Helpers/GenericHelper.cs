using System.Data;
using System.Reflection;

namespace Business.Helpers
{
    public class GenericHelper : IGenericHelper
    {
        public List<T> ConvertToGenericList<T>(DataTable dataTable)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }       

        public DataTable ConvertToDataTable<T>(List<T> objectList)
        {
            var dtData = new DataTable();
            var objectReference = objectList.GetType().GetGenericArguments()[0];
            var properties = objectReference.GetProperties();
            foreach (var prop in properties)
            {
                dtData.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in objectList)
            {
                var dataArray = new List<object>();
                foreach (var prop in properties)
                {
                    dataArray.Add(prop.GetValue(item));
                }

                dtData.Rows.Add(dataArray.ToArray());
            }

            return dtData;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }


    }
}
