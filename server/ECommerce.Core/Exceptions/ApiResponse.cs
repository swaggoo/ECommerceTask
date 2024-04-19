﻿namespace ECommerce.Core.Exceptions;

public class ApiResponse
{
    protected ApiResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }

    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "You have made a bad request",
            401 => "You are not authorized",
            404 => "Resource not found",
            500 => "An unhandled error occurred",
            _ => "Unexpected error occured" 
        };
    }
}