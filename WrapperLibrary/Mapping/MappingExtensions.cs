using Gurung.Wrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gurung.Wrapper.Mapping
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, RequestParameter requestParameter)
            => PaginatedList<TDestination>.CreateAsync(queryable, requestParameter);
        public static Task<PaginatedList<TDestination>> ProjectionPaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, RequestParameter requestParameter)
     => PaginatedList<TDestination>.SelectDynamicAsync(queryable, requestParameter);
    }
}
