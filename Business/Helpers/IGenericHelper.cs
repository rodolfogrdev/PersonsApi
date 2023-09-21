using System.Data;

namespace Business.Helpers
{
    public interface IGenericHelper
    {
        public List<T> ConvertToGenericList<T>(DataTable dataTable);

        public DataTable ConvertToDataTable<T>(List<T> objectList);
    }
}
