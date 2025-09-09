﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexai.Kaonashi.Core.Models.LLM
{
    public class Request
    {
        public string model { get; set; }
        public List<Message> messages { get; set; }
        public bool stream { get; set; } = false;
    }
}
