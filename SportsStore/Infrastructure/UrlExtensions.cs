﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest request)
        {
            var test = request;
            var mytest = request.QueryString;
            var teasd = request.QueryString.Value;
            var a =   request.QueryString.HasValue ? $"{request.Path}{request.QueryString}" : request.Path.ToString();
            return a;

        }
    }
}
