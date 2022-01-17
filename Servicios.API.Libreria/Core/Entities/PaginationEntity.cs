using System.Collections;
using System.Collections.Generic;

namespace Servicios.API.Libreria.Core.Entities
{
    public class PaginationEntity<T>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public string SortDirection { get; set; }
        public string Filter { get; set; }
        public FilterValue FilterValue { get; set; }
        public int PagesQuantity { get; set; }
        public IEnumerable<T> Data { get; set; }
        public int TotalRows { get; set; }

    }
}
