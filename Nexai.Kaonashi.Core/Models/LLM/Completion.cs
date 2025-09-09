using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexai.Kaonashi.Core.Models.LLM
{
    public class Completion : EntityBase
    {
        public string? modelName { get; set; }
        public string? prompttext { get; set; }
        public int promptLenght { get; set; } = 0;
        public int promptNbOfWords { get; set; } = 0;
        public string? completiontext { get; set; }
        public int completionLenght { get; set; } = 0;
        public int completionNbOfWords { get; set; } = 0;
    }
}
