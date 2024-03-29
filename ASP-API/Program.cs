using ASP_API;
using ASP_API.Model.Public;
using ASP_API.Services.Shared;
using ASP_API.Services.Staff;
using ASP_API.Services.Student;
using ASP_API.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("myCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddScoped<EmailService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = "localhost",
        ValidAudience = "localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<ISharedService, SharedService>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAdvisorService, AdvisorService>();
builder.Services.AddScoped<IGeneralManagerService, GeneralManagerService>();
builder.Services.AddScoped<IReviwerService, ReviwerService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<IHRManagerService, HRManagerService>();
builder.Services.AddScoped<IBatchServices, BatchServices>();
builder.Services.AddScoped<IBranchServices, BranchServices>();
builder.Services.AddScoped<IDomainServices, DomainServices>();
builder.Services.AddScoped<ICourseFeeServices, CourseFeeServices>();
builder.Services.AddScoped<ICondonationFeeService, CondonationFeeService>();
builder.Services.AddScoped<IStudentAgreementServices, StudentAgreementServices>();
builder.Services.AddScoped<IFeeService, FeeService>();
builder.Services.AddScoped<IAllocStudToAdvisorService, AllocStudToAdvisorService>();
builder.Services.AddScoped<ITrainingAndCommunicationService, TrainingAndCommunicationService>();
builder.Services.AddScoped<IReviewUpdateService, ReviewUpdateService>();
builder.Services.AddScoped<IStudentAttendanceService, StudentAttendanceService>();

builder.Services.AddScoped<ResponseMessages>();
builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));

app.UseHttpsRedirection();

app.UseCors("myCorsPolicy");

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "images")),
    RequestPath = new PathString("/images")
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Profiles/advisor")),
    RequestPath = new PathString("/Profiles/advisor")
});

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Documents")),
    RequestPath = new PathString("/Documents")
});

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Profiles")),
    RequestPath = new PathString("/Profiles")
});

app.MapControllers();

app.Run();
