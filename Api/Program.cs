
using Api.Services.Implementation;
using Api.Services.Interfaces;

using Microsoft.EntityFrameworkCore;
using DbComtext;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔹 قراءة Connection String من appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("SqlServer");

            // 🔹 إضافة DbContext مع SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // 🔹 إضافة الخدمات إلى الـ Dependency Injection
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IVisitService, VisitService>();

            // 🔹 تمكين HttpContextAccessor لاستخدامه في استخراج UserId من التوكن
            builder.Services.AddHttpContextAccessor();

            // 🔹 إضافة الـ Controllers
            builder.Services.AddControllers();

            // 🔹 تمكين Swagger للتوثيق
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // 🔹 تهيئة قاعدة البيانات عند بدء التشغيل (اختياري)
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate(); // تنفيذ أي عمليات ترحيل (Migrations) عند بدء التشغيل
            }

            // 🔹 تكوين مسار الـ API
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
