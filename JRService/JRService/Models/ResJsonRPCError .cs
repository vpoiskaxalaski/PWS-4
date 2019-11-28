using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JRService.Models
{
    public class ResJsonRPCError
    {
        public string Id { get; set; }
        public string JsonRPC { get; set; }
        public ErrorJsonRPC Error { get; set; }
    }
}