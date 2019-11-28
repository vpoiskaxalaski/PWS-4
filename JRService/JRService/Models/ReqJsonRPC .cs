﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JRService.Models
{
    public class ReqJsonRPC
    {
        public string Id { get; set; }
        public string JsonRPC { get; set; }
        public string Method { get; set; }
        public DataModel Params { get; set; }
    }
}