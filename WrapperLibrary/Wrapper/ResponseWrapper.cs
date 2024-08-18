using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WrapperLibrary.Models;

namespace WrapperLibrary.Wrapper
{
    public class ResponseWrapper
    {
        public ResponseWrapper()
        {

        }
        public ResponseWrapper(object data, string message = null)
        {
            IsSuccess = true;
            Data = data;
            Message = message;
        }
        public ResponseWrapper(bool succeeded, string message)
        {
            IsSuccess = succeeded;
            Message = message;
        }
        public ResponseWrapper(bool succeeded, object data, string message)
        {
            IsSuccess = succeeded;
            Message = message;
        }

        /// <summary>
        /// Validation error
        /// </summary>
        public ResponseWrapper(IDictionary<string, string[]> errors, string message)
        {
            IsSuccess = false;
            Errors = errors;
            Message = message;
        }


        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int PageNumber { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int TotalPages { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int TotalCount { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, string[]> Errors { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> ValidationMessage { get; set; }

        public object Data { get; set; }

        public static ResponseWrapper Success(string message) => new() { IsSuccess = true, Message = message };
        public static ResponseWrapper Success(string message, object data) => new() { IsSuccess = true, Message = message, Data = data };
        public static ResponseWrapper Error(string message) => new() { IsSuccess = false, Message = message };

        //public static async Task<ResponseWrapper> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
        //{
        //    var list = await queryable.PaginatedListAsync(pageNumber, pageSize);
        //    return new() { IsSuccess = true, Data = list.Items, PageNumber = list.PageNumber, TotalPages = list.TotalPages, TotalCount = list.TotalCount };
        //}

        public static ResponseWrapper PaginatedListAsync<T>(PaginatedList<T> data, string message = "")
        {
            var list = data;
            return new() { IsSuccess = true, Message = message, Data = list.Items, PageNumber = list.PageNumber, TotalPages = list.TotalPages, TotalCount = list.TotalCount };
        }

        public static ResponseWrapper PaginatedListAsync(object data, int pageNumber, int totalPage, int totalCount)
        {
            return new() { IsSuccess = true, Data = data, PageNumber = pageNumber, TotalPages = totalPage, TotalCount = totalCount };
        }

        public static ResponseWrapper ValidationError(string message, IDictionary<string, string[]> errors) => new()
        {
            Message = message,
            IsSuccess = false,
            Errors = errors
        };
        public static ResponseWrapper ValidationError(string message, List<string> error) => new() { IsSuccess = false, Message = message, ValidationMessage = error };

        public static ResponseWrapper ValidationError(IEnumerable<ValidationError> failures)
        {
            IDictionary<string, string[]> errors = new Dictionary<string, string[]>();
            var failureGroups = failures
                        .GroupBy(e => e.Name, e => e.Reason);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();
                errors.Add(propertyName, propertyFailures);
            }
            return new ResponseWrapper(errors, "Validation-error");
        }

    }
}
