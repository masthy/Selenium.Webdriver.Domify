using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Selenium.Webdriver.Domify.Elements
{
    public class TableRowCollection: IEnumerable<TableRow>
    {
        public TableRowCollection(IEnumerable<TableRow> tableRows )
        {
            _tableRows = tableRows.ToList();
        }

        private readonly IList<TableRow> _tableRows;
        public TableRow this[int index]
        {
            get { return _tableRows[index]; }
        }

        public int Count
        {
            get { return _tableRows.Count; }
        }

        public IEnumerator<TableRow> GetEnumerator()
        {
            return _tableRows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}