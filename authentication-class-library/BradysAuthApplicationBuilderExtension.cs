using authentication_class_library.Models;
using authentication_class_library.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace authentication_class_library
{
    public static class BradysAuthApplicationBuilderExtension
    {
        public static void UserBradysAuthenticationService(
        this IApplicationBuilder app,
        string issuer)
        => app.UserBradysAuthenticationService(opts => opts.Issuer = issuer);


        public static void UserBradysAuthenticationService(
        this IApplicationBuilder app,
        Action<BradysAuthenticationSettings>? configureOptions = null)
        {
            //if (app == null) throw new ArgumentNullException(nameof(app));

            var opts = app.ApplicationServices.GetService<IOptions<BradysAuthenticationSettings>>()?.Value ?? new BradysAuthenticationSettings();

            var dbOptions = new DatabaseOptions()
            {
                Database = opts.Database,
                Password = opts.Password,
                MultipleActiveResultSets = true,
                Server = opts.Server,
                UserId = opts.UserId
            };

            var test = new DatabaseSetup(dbOptions);
            test.CheckDatabaseExistsAndCreateDatabase();

            DependencyInjection.bas = opts;



            configureOptions?.Invoke(opts);
            //return app.UseMiddleware<BradysAuthenticationSettings>(opts);
        }
    }
}
