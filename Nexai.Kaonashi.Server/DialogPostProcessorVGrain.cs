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
    public class DialogPostProcessorVGrain : VGrainBase<IDialogPostProcessorVGrain>, IDialogPostProcessorVGrain
    {
        public DialogPostProcessorVGrain(ILogger<IDialogPostProcessorVGrain> logger) : base(logger)
        {

        }

        public async Task<Models.Dialog> DoPostProcess(Models.Dialog d, IExecutionContext Context)
        {
            Models.Dialog dialog = new Models.Dialog();
            dialog = d;

            d.ProcessingAction.Add("PostProcessing done");
            return d;

        }
    }
}
