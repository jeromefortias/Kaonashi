using Democrite.Framework.Builders;
using Democrite.Framework.Core.Abstractions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace JsonAnalyzzzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); // builder pour le host de base



            // Ajout Swagger dépendence :  Swashbuckle.AspNetCore
            builder.Services.AddSwaggerGen(s => s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Tuto Demo", Version = "v1" })).AddEndpointsApiExplorer();


            var jsonProcessSeq = Sequence.Build("JFO", fixUid: new Guid("D1F7C7EB-77F7-488A-91D7-77E4D5D16448"), metadataBuilder: m =>
                                         {
                                             m.Description("Dialog Sequence")
                                              .AddTags("chatbot", "nexai");
                                         })
                                         .RequiredInput<Models.Dialog>()
                                         .Use<IContextUpdateVGrain>().Configure("JFO-TABLE").Call((g, i, ctx) => g.DoProcess(i, ctx)).Return
                                         .Use<IEmotionnalInboundProcessorVGrain>().Configure("JFO-TABLE").Call((g, i, ctx) => g.DoProcess(i, ctx)).Return
                                         .Use<IDialogCacheProcessorVGrain>().Configure("JFO-TABLE").Call((g, i, ctx) => g.DoProcess(i, ctx)).Return
                                         .Use<IDialogPreProcessorVGrain>().Configure("JFO-TABLE").Call((g, i, ctx) => g.DoPreProcess(i, ctx)).Return
                                         .Use<IDialogProcessorVGrain>().Configure("JFO-TABLE").Call((g, i, ctx) => g.DoProcess(i, ctx)).Return
                                         .Use<IDialogPostProcessorVGrain>().Configure("JFO-TABLE").Call((g, i, ctx) => g.DoPostProcess(i, ctx)).Return
                                         .Use<IEmotionnalOutboundVGrain>().Configure("JFO-TABLE").Call((g, i, ctx) => g.DoProcess(i, ctx)).Return
                                         .Use<IDialogFeedBackVGrain>().Configure("JFO-TABLE").Call((g, i, ctx) => g.DoProcess(i, ctx)).Return
                                         .Build();

            builder.Host.UseDemocriteNode(b =>
            {
                b.WizardConfig()
                .NoCluster()
                .ConfigureLogging(c => c.AddConsole())
                .AddInMemoryDefinitionProvider(d => d.SetupSequences(jsonProcessSeq));

                b.ManualyAdvancedConfig().UseDashboard();
            }
                                                );
            var app = builder.Build();
            app.UseSwagger();



            app.MapPost("/aiassistant", async (Models.Dialog d, [FromServices] IDemocriteExecutionHandler handler) =>
            {
                var result = await handler.Sequence<Models.Dialog>(jsonProcessSeq.Uid)
                                       .SetInput(d)
                                       .RunAsync<Models.Dialog>();

                var content = result.SafeGetResult();
                return content;


            });



            app.MapGet("/", (request) =>
            {
                request.Response.Redirect("swagger");
                return Task.CompletedTask;
            });
            app.UseSwaggerUI();

            app.Run();

        }
    }
}
