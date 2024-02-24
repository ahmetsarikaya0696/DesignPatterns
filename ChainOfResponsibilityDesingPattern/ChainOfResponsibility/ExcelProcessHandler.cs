using ClosedXML.Excel;
using System.Data;

namespace ChainOfResponsibilityDesingPattern.ChainOfResponsibility
{
    public class ExcelProcessHandler<T> : ProcessHandler
    {
        private DataTable GetTable(Object o)
        {
            var table = new DataTable();

            var type = typeof(T);

            type.GetProperties().ToList().ForEach(x => table.Columns.Add(x.Name, x.PropertyType));

            var list = o as List<T>;

            list.ForEach(x =>
            {
                var values = type.GetProperties().Select(propertyInfo => propertyInfo.GetValue(x, null)).ToArray();

                table.Rows.Add(values);
            });

            return table;
        }

        public override object Handle(object o)
        {
            var workbook = new XLWorkbook();
            var dataset = new DataSet();
            var table = GetTable(o);

            dataset.Tables.Add(table);

            workbook.Worksheets.Add(dataset);  

           var excelMemoryStream = new MemoryStream();

            workbook.SaveAs(excelMemoryStream);

            return base.Handle(excelMemoryStream);
        }
    }
}
