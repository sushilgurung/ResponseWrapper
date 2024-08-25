using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gurung.Wrapper.Models
{
    public class RequestParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public RequestParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public RequestParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
        public List<SearchParameter> search { get; set; }
        public List<Sorting> Sort { get; set; }
        public ProjectionModel projectionModel { get; set; }
    }
    public class SearchParameter
    {
        public string ColumnSearchName { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
    }
    public class Sorting
    {
        public string ColumnName { get; set; }
        public bool IsAscending { get; set; }
    }
}
