using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Repositories;
using StolovkyZilina.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StolovkyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StolovkyDbConnectionString")));
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StolovkyAuthDbConnectionString")));

//builder.Services.AddDbContext<StolovkyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StolovkyDbConnectionStringServer")));
//builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StolovkyAuthDbConnectionStringServer")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = Constants.MinimumCharactersInPassword;
    options.Password.RequiredUniqueChars = 0;
});

builder.Services.AddScoped<IRepository<Content>, ContentRepository>();
builder.Services.AddScoped<IRepository<BlogPost>, BlogPostRepository>();
builder.Services.AddScoped<IRepository<Game>, GameRepository>();
builder.Services.AddScoped<IRepository<UserProfile>, UserProfileRepository>();
builder.Services.AddScoped<IRepository<GameLanguage>, LanguageRepository>();
builder.Services.AddScoped<IRepository<Location>, LocationRepository>();
builder.Services.AddScoped<IRepository<GamePlay>, GamePlayRepository>();
builder.Services.AddScoped<IRepository<Feed>, FeedRepository>();
builder.Services.AddScoped<IRepository<AdminMessage>, AdminMessagesRepository>();
builder.Services.AddScoped<IRepository<Event>, EventRepository>();
builder.Services.AddScoped<IRepository<ParticipationVote>, ParticipationRepository>();
builder.Services.AddScoped<IRepository<GamePoll>, GamePollRepository>();
builder.Services.AddScoped<IRepository<GameVote>, GameVoteRepository>();
builder.Services.AddScoped<IRepository<PlayerMmr>, PlayerMmrRepository>();
builder.Services.AddScoped<IRepository<AuctionOffer>, AuctionOfferRepository>();
builder.Services.AddScoped<IRepository<GameCategory>, GameCategoryRepository>();

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IGameRelationRepository<GameOwner>, GameOwnerRepository>();
builder.Services.AddScoped<IContentRatingRepository, ContentRatingRepository>();
builder.Services.AddScoped<IContentCommentRepository, ContentCommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
