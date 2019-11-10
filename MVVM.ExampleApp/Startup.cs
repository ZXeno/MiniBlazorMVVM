namespace MVVM.ExampleApp
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MVVM.ExampleApp.Data;
    using MVVM.ExampleApp.ViewModels;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            // Here, we can create and call a method to register all of our viewmodels.
            this.ViewModelBootstrapper(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // We want to bootstrap the IViewModelLocator instance here.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        /// <summary>
        /// Method for registering all of our viewmodels to the DI container.
        /// </summary>
        /// <param name="serviceCollection"></param>
        private void ViewModelBootstrapper(IServiceCollection serviceCollection)
        {
            // Use scoped/transient lifetimes depending on use-case for your application.
            serviceCollection.AddTransient<CodeBehindExampleViewModel>();
            serviceCollection.AddTransient<FetchDataPageViewModel>();
        }
    }
}
