using Democrite.Framework.Core;
using Democrite.Framework.Core.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonAnalyzzzer
{
    internal class DialogCacheProcessorVGrain : VGrainBase<IDialogCacheProcessorVGrain>, IDialogCacheProcessorVGrain
    {
        public DialogCacheProcessorVGrain(ILogger<IDialogCacheProcessorVGrain> logger) : base(logger)
        {

        }

        public async Task<Models.Dialog> DoProcess(Models.Dialog d, IExecutionContext Context)
        {
            Models.Dialog dialog = new Models.Dialog();
            dialog = d;

            d.ProcessingAction.Add("Cache checked");
            return d;

        }
    }
}
