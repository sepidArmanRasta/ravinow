using Application.Dtos._HTTP;

namespace Application.Dtos._Pagination
{
    public class PaginatedItemsDto<TEntity> : ResponseDto<IEnumerable<TEntity>> where TEntity : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }
        public Pager Pager
        {
            get
            {
                return new Pager(Count, PageIndex, PageSize);
            }
        }
        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 1;
            }
        }
        public bool HasNextPage
        {
            get
            {
                return PageIndex < Pager.TotalPages;
            }
        }
    }

    public class Pager
    {
        public Pager(long totalItems, int currentPage = 1, int pageSize = 10, int maxPages = 5)
        {
            var totalPages = 0;
            if (pageSize != 0)
            {
                totalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
            }

            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            int startPage, endPage;
            if (totalPages <= maxPages)
            {
                startPage = 1;
                endPage = totalPages;
            }
            else
            {
                var maxPagesBeforeCurrentPage = (int)Math.Floor(maxPages / (decimal)2);
                var maxPagesAfterCurrentPage = (int)Math.Ceiling(maxPages / (decimal)2) - 1;
                if (currentPage <= maxPagesBeforeCurrentPage)
                {
                    startPage = 1;
                    endPage = maxPages;
                }
                else if (currentPage + maxPagesAfterCurrentPage >= totalPages)
                {
                    startPage = totalPages - maxPages + 1;
                    endPage = totalPages;
                }
                else
                {
                    startPage = currentPage - maxPagesBeforeCurrentPage;
                    endPage = currentPage + maxPagesAfterCurrentPage;
                }
            }

            var pages = Enumerable.Range(startPage, endPage + 1 - startPage);

            TotalPages = totalPages;
            Pages = pages;
        }
        public IEnumerable<int> Pages { get; private set; }
        public int TotalPages { get; private set; }
    }
}
