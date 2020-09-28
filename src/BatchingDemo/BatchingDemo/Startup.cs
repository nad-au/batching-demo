using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Funq;
using ServiceStack;
using ServiceStack.Configuration;
using BatchingDemo.ServiceInterface;
using BatchingDemo.ServiceModel;
using ServiceStack.Messaging;

namespace BatchingDemo
{
    public class Startup : ModularStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public new void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceStack(new AppHost
            {
                AppSettings = new NetCoreAppSettings(Configuration)
            });
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost() : base("BatchingDemo", typeof(PaymentService).Assembly) { }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
            {
                DefaultRedirectPath = "/metadata",
                DebugMode = AppSettings.Get(nameof(HostConfig.DebugMode), false)
            });
            
            var builder = new ContainerBuilder();

            builder.RegisterType<WinOrLose>().AsImplementedInterfaces();
            builder.RegisterType<TenantBankingProvider>().AsImplementedInterfaces();
            builder.Register(c => new BackgroundMqService()).AsImplementedInterfaces().SingleInstance();
            
            IContainerAdapter adapter = new AutofacIocAdapter(builder.Build());
            container.Adapter = adapter;

            var mqServer = container.Resolve<IMessageService>();

            mqServer.RegisterHandler<ProcessPaymentRequest>(ExecuteMessage);
            mqServer.RegisterHandler<TenantBankingSetupRequest>(ExecuteMessage);
            mqServer.RegisterHandler<TenantBankingSetupError>(ExecuteMessage);
            mqServer.RegisterHandler<EnqueuePaymentRequest>(ExecuteMessage, 4);

            AfterInitCallbacks.Add(host => {
                mqServer.Start();
                //ExecuteService(new RetryPendingNotifications());
            });

        }
    }
}
