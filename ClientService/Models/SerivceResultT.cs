﻿namespace ClientService.Models;

public class ServiceResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static ServiceResult<T> Ok(T data, string message = "Operation successful")
    {
        return new ServiceResult<T> { Success = true, Data = data, Message = message };
    }
    public static ServiceResult<T> Fail(string message)
    {
        return new ServiceResult<T> { Success = false, Message = message };
    }
}