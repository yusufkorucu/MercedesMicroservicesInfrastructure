﻿namespace EmailService.Api.Core.Domain.Base
{
    public class ResultModel<T> where T : new()
    {
        public T Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get;set; }
    }
}
