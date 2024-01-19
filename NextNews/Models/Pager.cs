using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace NextNews.Models
{
    public class Pager
    {
        public int TotalItem { get; private set; }

        public int CurrentPage { get; private set; }

        public int PageSize { get; private set; }

        public int TotalPages { get; private set; }

        public int StartPage { get; private set; }

        public int EndPage { get; private set; }



        public Pager()
        {

        }

        public Pager(int totalItems, int page, int pageSize = 10)
        {

            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                startPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }

            }

        }
    }
}
