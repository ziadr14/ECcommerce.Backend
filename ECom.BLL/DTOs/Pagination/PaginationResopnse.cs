using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.BLL.DTOs.Pagination
{
    public class PaginationResopnse<T> where T : class
    {
        public PaginationResopnse(int pageNumber, int pageSize, int totalCount, IReadOnlyList<T> data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public IReadOnlyList<T> Data { get; set; }
    }
}
