﻿using System.Net;

namespace ASP_API.Model.Public
{
    public class ResponseMessages
    {
        public ResponseMessages()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string>? ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
