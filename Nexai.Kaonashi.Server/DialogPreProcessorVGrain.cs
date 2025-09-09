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
    internal class DialogPreProcessorVGrain : VGrainBase<IDialogPreProcessorVGrain>, IDialogPreProcessorVGrain
    {
        public DialogPreProcessorVGrain(ILogger<IDialogPreProcessorVGrain> logger) : base(logger)
        {

        }

        public async Task<Models.Dialog> DoPreProcess(Models.Dialog d, IExecutionContext Context)
        {
            Models.Dialog dialog = new Models.Dialog();
            dialog = d;

            d.ProcessingAction.Add("PreprocessingDone");
            return d;

        }
    }
}
