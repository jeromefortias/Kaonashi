using Democrite.Framework.Core.Abstractions;
using Democrite.Framework.Core.Abstractions.Attributes;
using Democrite.Framework.Core.Abstractions.Attributes.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonAnalyzzzer
{
    [VGrainCategory("Collector")]
    //[VGrainMetaData()]
    public interface IDialogPostProcessorVGrain : IVGrain
    {
        Task<Models.Dialog> DoPostProcess(Models.Dialog d, IExecutionContext Context);

    }
}
