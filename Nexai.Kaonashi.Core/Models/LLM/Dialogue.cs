using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexai.Kaonashi.Core.Models.LLM
{
    using Nexai.Kaonashi.Core.Models;
/// <summary>
/// 
/// </summary>
    public class Dialog
    {
        public string? SessionId { get; set; }
        public Request? Request { get; set; }
        public Response? Response { get; set; }
        public List<MetaData>? MetaDatas { get; set; }
        public bool? Cached { get; set; } = false;        
        public string? PreviousQuestion { get; set; }
        public string? PreviousAnswer { get; set; }
    }
}
