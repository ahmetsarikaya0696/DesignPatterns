using ClosedXML.Excel;
using System.Data;

namespace CommandDesignPattern.Commands
{
    public class ExcelFile<T>
    {
        private readonly List<T> _list;
        public string FileName => $"{typeof(T).Name}.xlsx";
        public string FileType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public ExcelFile(List<T> list)
        {
            _list = list;
        }

        public MemoryStream Create()
        {
            var workboook = new XLWorkbook();

            var dataset = new DataSet();

            dataset.Tables.Add(GetTable());

            workboook.Worksheets.Add(dataset);

            var excelMemory = new MemoryStream();

            workboook.SaveAs(excelMemory);

            return excelMemory;
        }

        private DataTable GetTable()
        {
            var table = new DataTable();

            var type = typeof(T);

            type.GetProperties().ToList().ForEach(propertyInfo => table.Columns.Add(propertyInfo.Name, propertyInfo.PropertyType));

            _list.ForEach(x =>
            {
                var values = type.GetProperties().Select(propertInfo => propertInfo.GetValue(x, null)).ToArray();

                table.Rows.Add(values);
            });

            return table;
        }
    }
}
