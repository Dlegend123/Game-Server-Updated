namespace GameServer
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Identity.Web;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.Swagger;

    public enum ItemType { SWORD, POTION, SHIELD }
    public class Startup
    {
        readonly RealTimeCityBikeDataFetcher onlineFetcher;
        readonly OfflineCityBikeDataFetcher offlineFetcher;

        public Startup()
        {
            onlineFetcher = new RealTimeCityBikeDataFetcher(); ;
            offlineFetcher = new OfflineCityBikeDataFetcher();
        }
        static void Main(string[] args)
        {
            //Assignment 1
            /*
            Program p = new ();
            string _bikeStationName;

            Console.WriteLine("Do you want to use the application (On)line or (Of)fline?");
            var answer = Console.ReadLine();
            if (answer.ToLower().Contains("on"))
            {
                Console.Write("Using online mode, fetcing data in real time..\n");
            }
            else if (answer.ToLower().Contains("of"))
            {
                Console.Write("Using offline mode, loading data from a pre-loaded file.\n");
            }
            else
            {
                Console.Write("Defaulting online mode, fetcing data in real time.. \n");
            }
            //Console.WriteLine(args[0]);
            bool running = true;
            while (running)
            {

                Console.WriteLine("Enter station's name or type 'exit' to exit: ");
                _bikeStationName = Console.ReadLine();

                if (_bikeStationName.ToLower().Contains("exit"))
                {
                    running = false;
                    break;
                }
                int x;
                x=(answer.ToLower().Contains("of"))? 
                    await p.offlineFetcher.GetBikeCountInStation(_bikeStationName) :
                    await p.onlineFetcher.GetBikeCountInStation(_bikeStationName);

                if (x != -1)
                    Console.WriteLine(_bikeStationName + " has " + x + " as the bike count.\n");
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            */
            //Assignment 2
            /*
            List<Player> players = GameHelpers.InstPlayers();
            players = GameHelpers.SetPlayerData(players);

            GameHelpers.TryGetItemHigh(players);
            GameHelpers.TryFirstItem(players);
            GameHelpers.TryFirstItemLinq(players);
            GameHelpers.TryGetItems(players);
            GameHelpers.TryGetItemsLinq(players);
            GameHelpers.TryDelegates(players);

            GameHelpers.InstGame(players);

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine("Player {0}", i);
                GameHelpers.printItem(players[i]);
            }
          */
            //Assignment 3
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IRepository, MongoDbRepository>();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameWebAPI", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.TagActionsBy(api => new[] { api.GroupName });
                c.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));
            });

            var app = builder.Build();
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new ErrorHandlingMiddleware().Invoke
            });
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameWebAPI v1"));
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run();
        }
    }
}