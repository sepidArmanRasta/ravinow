using Domain.Enumeration._Pagination;

namespace Application.Dtos._Pagination
{
    public class PaginationRequestDto<TSearchType, TSearchSortEnum> where TSearchType : class
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public TSearchType? Search { get; set; }
        public TSearchSortEnum? Sort { get; set; }
        public SortType SortType { get; set; }
    }
}