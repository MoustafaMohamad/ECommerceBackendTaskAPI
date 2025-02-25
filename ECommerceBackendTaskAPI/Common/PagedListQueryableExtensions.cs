using System.Collections.Generic;

namespace ECommerceBackendTaskAPI.Common
{
    public static class PagedListQueryableExtensions
    {

        public class PagedList<T>
        {
            private IEnumerable<T> items;
            private int count;
            private int page;
            private int pageSize;

            public PagedList(IEnumerable<T> items, int count, int page, int pageSize)
            {
                this.items = items;
                this.count = count;
                this.page = page;
                this.pageSize = pageSize;
            }
        }
        public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int page,
        int pageSize,
        CancellationToken token = default)
        {
            var count = await source.CountAsync(token);
            if (count > 0)
            {
                var items = await source
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(token);
                return new PagedList<T>(items, count, page, pageSize);
            }

            return new(Enumerable.Empty<T>(), 0, 0, 0);
        }

     



    }
}
