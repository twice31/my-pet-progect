using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookExchange.Presenters.Envelope
{
    public class Envelope<T> : IResult
    {
        [JsonInclude]
        public T? Data { get; init; }

        [JsonInclude]
        public string? Error { get; init; }

        private Envelope(T? data, string? error)
        {
            Data = data;
            Error = error;
        }


        [JsonIgnore]
        public int StatusCode { get; private set; } = StatusCodes.Status200OK;

        public Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = this.StatusCode;

            return httpContext.Response.WriteAsJsonAsync(this, this.GetType());
        }


        public static Envelope<T> Ok(T data) => new(data, null);

        public static Envelope<T> Created(T data) => new(data, null) { StatusCode = StatusCodes.Status201Created };

        public static Envelope<T> NotFound(string errorMessage) => new(default, errorMessage) { StatusCode = StatusCodes.Status404NotFound };

        public static Envelope<T> BadRequest(string errorMessage) => new(default, errorMessage) { StatusCode = StatusCodes.Status400BadRequest };

        public static Envelope<T> InternalServerError(string errorMessage) => new(default, errorMessage) { StatusCode = StatusCodes.Status500InternalServerError };

        public static Envelope<T> Conflict(string errorMessage) => new(default, errorMessage) { StatusCode = StatusCodes.Status409Conflict };
    }

    public class Envelope : IResult
    {
        [JsonInclude]
        public string? Error { get; init; }

        private Envelope(string? error)
        {
            Error = error;
        }

        [JsonIgnore]
        public int StatusCode { get; private set; } = StatusCodes.Status204NoContent;

        public Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = this.StatusCode;
            return httpContext.Response.WriteAsJsonAsync(this, this.GetType());
        }

        public static Envelope NoContent() => new(null) { StatusCode = StatusCodes.Status204NoContent };
        public static Envelope NotFound(string errorMessage) => new(errorMessage) { StatusCode = StatusCodes.Status404NotFound };
    }
}