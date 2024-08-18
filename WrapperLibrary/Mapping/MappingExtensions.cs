using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WrapperLibrary.Models;

namespace WrapperLibrary.Mapping
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, RequestParameter requestParameter)
            => PaginatedList<TDestination>.CreateAsync(queryable, requestParameter);
    }
}
