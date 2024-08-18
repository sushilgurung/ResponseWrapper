using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WrapperLibrary.Models
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; }
        public int PageNumber { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public int PageSize { get; set; }

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
            PageSize = pageSize;
        }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        private delegate Expression Binder(Expression left, Expression right);

        private static readonly MethodInfo MethodContains = typeof(Enumerable).GetMethods(
                        BindingFlags.Static | BindingFlags.Public)
                        .Single(m => m.Name == nameof(Enumerable.Contains)
                            && m.GetParameters().Length == 2);

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, RequestParameter requestParameter)
        {
            if (requestParameter.search != null && requestParameter.search.Count > 0)
            {
                source = ExpressionLinq.filter<T>(source, requestParameter.search);
            }
            if (requestParameter.Sort != null && requestParameter.Sort.Count > 0)
            {
                foreach (var item in requestParameter.Sort)
                {
                    source = ApplyOrdering.OrderBy(source, item.ColumnName, item.IsAscending);
                }
            }
            var count = await source.CountAsync();
            var items = await source.Skip((requestParameter.PageNumber - 1) * requestParameter.PageSize).Take(requestParameter.PageSize).ToListAsync();

            return new PaginatedList<T>(items, count, requestParameter.PageNumber, requestParameter.PageSize);
        }
    }
}
