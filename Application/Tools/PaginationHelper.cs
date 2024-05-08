namespace Application.Tools
{
    public static class PaginationHelper
    {
        public static IQueryable<TSource> ToPaged<TSource>(this IQueryable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> source, int page, int pageSize, out int rowsCount)
        {
            rowsCount = source.Count();
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> PagedResult<T, TResult>(this IQueryable<T> query, int pageNum, int pageSize, out int rowsCount)
        {
            if (pageNum <= 0 && pageSize <= 0)
            {
                pageSize = query.Count();
                pageNum = 1;
            }

            rowsCount = query.Count();
            int excludedRows = (pageNum - 1) * pageSize;
            return query.Skip(excludedRows).Take(pageSize);
        }

        public static IQueryable<TSource> PagedResult<TSource>(this IQueryable<TSource> query, int pageNum, int pageSize, out int rowsCount)
        {
            if (pageNum <= 0 && pageSize <= 0)
            {
                pageSize = query.Count();
                pageNum = 1;
            }

            rowsCount = query.Count();
            int excludedRows = (pageNum - 1) * pageSize;
            return query.Skip(excludedRows).Take(pageSize);
        }
    }
}
