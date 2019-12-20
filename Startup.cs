using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MFFMS.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using MFFMS.API.Helpers;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Identity;
using MFFMS.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using MFFMS.API.Data.KhachHangRepository;
using MFFMS.API.Data.NhanVienRepository;
using MFFMS.API.Data.SanBongRepository;
using MFFMS.API.Data.DichVuRepository;
using MFFMS.API.Data.HoaDonDichVuRepository;
using MFFMS.API.Data.NhaCungCapRepository;
using MFFMS.API.Data.CaiDatRepository;
using MFFMS.API.Data.TaiSanThietBiRepository;
using MFFMS.API.Data.ChiTietHDDVRepository;
using MFFMS.API.Data.DonNhapHangRepository;
using MFFMS.API.Data.ChiTietDonNhapHangRepository;
using MFFMS.API.Data.PhieuDatSanRepository;
using MFFMS.API.Data.ChiTieuPhieuDatSanRepository;
using MFFMS.API.Data.ThongKeRepository;

namespace MFFMS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseMySQL(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MFFMS API", Description = "Mini Footbal Field Management System API" });
            });
            services.AddCors();
            services.AddAutoMapper();
            services.AddScoped<ITaiKhoanRepository, TaiKhoanRepository>();
            services.AddScoped<IKhachHangRepository, KhachHangRepository>();
            services.AddScoped<INhanVienRepository, NhanVienRepository>();
            services.AddScoped<ISanBongRepository, SanBongRepository>();
            services.AddScoped<IDichVuRepository, DichVuRepository>();
            services.AddScoped<IHoaDonDichVuRepository, HoaDonDichVuRepository>();
            services.AddScoped<INhaCungCapRepository, NhaCungCapRepository>();
            services.AddScoped<ITaiSanThietBiRepository, TaiSanThietBiRepository>();
            services.AddScoped<ICaiDatRepository, CaiDatRepository>();
            services.AddScoped<IChiTietHDDVRepository, ChiTietHDDVRepository>();
            services.AddScoped<IDonNhapHangRepository, DonNhapHangRepository>();
            services.AddScoped<IChiTietDonNhapHangRepository, ChiTietDonNhapHangRepository>();
            services.AddScoped<IPhieuDatSanRepository, PhieuDatSanRepository>();
            services.AddScoped<IChiTietPhieuDatSanRepository, ChiTietPhieuDatSanRepository>();
            services.AddScoped<IThongKeRepository, ThongKeRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
           {
               c.RoutePrefix = string.Empty;
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
           });
        }
    }
}
