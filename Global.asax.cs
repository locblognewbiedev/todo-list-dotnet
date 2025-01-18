using AutoMapper;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using todo.Models;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using todo.Data;
using Unity.WebApi;
using todo.Models.DTOs;
using Serilog;
using System.IO;
using System;
namespace todo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            /*************logging ***************/
            string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            Console.WriteLine(logDirectory);

            if (!Directory.Exists(logDirectory))
            {
                try
                {
                    Directory.CreateDirectory(logDirectory);
                    Console.WriteLine("Thư mục đã được tạo.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi tạo thư mục: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Thư mục đã tồn tại.");
            }

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(logDirectory, "app.log"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            /************* mapper config**********/
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<YourMappingProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper(); // Tạo instance mapper

            /***********unity config ************/
            var container = new UnityContainer();

            
            container.RegisterType<DBContext, DBContext>(new HierarchicalLifetimeManager());
            container.RegisterInstance<IMapper>(mapper);


            // Gắn container vào Web API
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            // Cấu hình các phần khác của ứng dụng
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            // Đảm bảo đóng log khi ứng dụng kết thúc
            Log.CloseAndFlush();
        }
    }
}
