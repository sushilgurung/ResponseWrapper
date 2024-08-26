using Gurung.Wrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gurung.Wrapper.Wrapper
{
    public  class ResponseWrapper
    {
        public ResponseWrapper()
        {

        }
        /// <summary>
        /// Constructor for sucessful response
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public ResponseWrapper(object data, string message = null)
        {
            Success = true;
            Data = data;
            Message = message;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="succeeded"></param>
        /// <param name="message"></param>
        public ResponseWrapper(bool succeeded, string message)
        {
            Success = succeeded;
            Message = message;
        }
        public ResponseWrapper(bool succeeded, object data, string message)
        {
            Success = succeeded;
            Message = message;
        }

        /// <summary>
        /// Constructor for Error response
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="message"></param>
        public ResponseWrapper(IDictionary<string, string[]> errors, string message)
        {
            Success = false;
            Errors = errors;
            Message = message;
        }


        public bool Success { get; set; }
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
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Data { get; set; }

        public static ResponseWrapper OnSuccess(string message) =>
            new()
            {
                Success = true,
                Message = message
            };
        public static ResponseWrapper OnSuccess(string message, object data)
            => new()
            {
                Success = true,
                Message = message,
                Data = data
            };

        public static ResponseWrapper OnSuccess<T>(string message, PaginatedList<T> data)
            => new()
            {
                Success = true,
                Message = message,
                Data = data.Items,
                PageNumber = data.PageNumber,
                TotalPages = data.TotalPages,
                TotalCount = data.TotalCount
            };
        public static ResponseWrapper OnError(string message) => new()
        {
            Success = false,
            Message = message
        };

        public static ResponseWrapper OnPaginatedListAsync<T>(PaginatedList<T> data, string message = "")
        {
            var list = data;
            return new()
            {
                Success = true,
                Message = message,
                Data = list.Items,
                PageNumber = list.PageNumber,
                TotalPages = list.TotalPages,
                TotalCount = list.TotalCount
            };
        }

        public static ResponseWrapper OnPaginatedListAsync(object data, int pageNumber, int totalPage, int totalCount)
        {
            return new()
            {
                Success = true,
                Data = data,
                PageNumber = pageNumber,
                TotalPages = totalPage,
                TotalCount = totalCount
            };
        }

        public static ResponseWrapper OnValidationError(string message, IDictionary<string, string[]> errors) => new()
        {
            Message = message,
            Success = false,
            Errors = errors
        };
        public static ResponseWrapper OnValidationError(string message, List<string> error) => new() { Success = false, Message = message, ValidationMessage = error };

        public static ResponseWrapper OnValidationError(IEnumerable<ValidationError> failures)
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
