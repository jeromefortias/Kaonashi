using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexai.Kaonashi.Core.Models.LLM
{
    public class Response
    {
        public string model { get; set; } = string.Empty;
        public DateTime created_at { get; set; } = DateTime.Now;
        public Message message { get; set; }
        public string done_reason { get; set; } = "stop";
        public bool done { get; set; } = true;
        public int total_duration { get; set; } = 0;
        public int load_duration { get; set; } = 0;
        public int prompt_eval_count { get; set; } = 0;
        public int prompt_eval_duration { get; set; } = 0;
        public int eval_count { get; set; } = 0;
        public int eval_duration { get; set; } = 0;
    }
}
