using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineMall.API.Services;
using OnlineMall.API.Settings;
using System.IO;
using System.Text;

namespace OnlineMall.API
{
    public class Startup
    {
        readonly string MyAllowOrigins = "_myAllowOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            // them ket noi db
            var connectionString = Configuration.GetConnectionString("OnlineMall");
            services.AddDbContextPool<Models.SystemDbContext>(options => options.UseSqlServer(connectionString));

            //mail
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();

            //upload IMG
            services.AddTransient<IUploadSevice, UploadSevices>();

            // add CORS
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowOrigins,
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                    );
            });

            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson();

            // add Authorize with token JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlineMall.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineMall.API v1"));
            }
            app.UseStaticFiles(new StaticFileOptions { 
            FileProvider = new  PhysicalFileProvider(Path.Combine(env.ContentRootPath,"Images")),
            RequestPath = "/Images"
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
