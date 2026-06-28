
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Build.Execution;
using Microsoft.CodeAnalysis.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MS3_LMS.IRepository;
using MS3_LMS.IRepository.V1;
using MS3_LMS.IService.V1;
using MS3_LMS.LMSDbcontext;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Repository.V1;
using MS3_LMS.Service.V1;
using System.Text;

namespace MS3_LMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddDbContext<LMSContext>(opt => opt
            .UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));


            builder.Services.AddCors(opt =>
            opt.AddPolicy(
                name: "CorsOpenPolicy",
                builder =>
                {
                    builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                }
                ));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            builder.Services.AddControllers()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                  options.JsonSerializerOptions.MaxDepth = 64; 
                 });


            var builders = WebApplication.CreateBuilder(args);
            builder.Services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 50 * 1024 * 1024; // 50 MB
            });
            builders.Logging.ClearProviders();
            builders.Logging.AddConsole();
            builders.Logging.AddDebug();

            //var app= builder.Build();

            builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));
            builder.Services.AddTransient<EmailService>();



            builder.Services.AddScoped<IBookRepository,BookRepo>();
            builder.Services.AddScoped<IBookService,BookService>();
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IBookLendRepository, BookLendRepository>();
            builder.Services.AddScoped<IBookLendService, BookLendService>();
            builder.Services.AddScoped<INotificationRepository, Notification>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IAothorRepository , Author>();
            builder.Services.AddScoped<IPublisherRepository,PublisherRepository>();
            builder.Services.AddScoped<IPublisherService,PublisherService>();
            builder.Services.AddScoped<IGenreRepository,GenreRepository>();
            builder.Services.AddScoped<IGenreService,GenreService>();
            builder.Services.AddScoped<IService.V1.ILanguageService, LanguageService>();
            builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();
            builder.Services.AddScoped<IRatingService, RatingService>();
            builder.Services.AddTransient<OTPService>();
             builder. Services.AddTransient<EmailService>();
            builder.Services.AddScoped<ISubcriptionRepository,SubcriptionRepository>();
            builder.Services.AddScoped<ISubcriptionService,SubcriptionService>();
            builder.Services.AddScoped<IPaymentRepository,PaymentRepository>();


            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsOpenPolicy");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
