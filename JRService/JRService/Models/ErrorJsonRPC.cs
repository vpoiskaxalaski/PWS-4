﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JRService.Models
{
    public class ErrorJsonRPC
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }
}