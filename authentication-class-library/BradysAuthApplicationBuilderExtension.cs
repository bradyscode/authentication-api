using authentication_class_library.Models;
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
        public static IApplicationBuilder UserBradysAuthenticationService(
        this IApplicationBuilder app,
        string issuer)
        => app.UserBradysAuthenticationService(opts => opts.Issuer = issuer);


        public static IApplicationBuilder UserBradysAuthenticationService(
        this IApplicationBuilder app,
        Action<BradysAuthenticationSettings>? configureOptions = null)
        {
            //if (app == null) throw new ArgumentNullException(nameof(app));

            var opts = app.ApplicationServices.GetService<IOptions<BradysAuthenticationSettings>>()?.Value ?? new BradysAuthenticationSettings();

            DependencyInjection.bas = opts;

            configureOptions?.Invoke(opts);
            return app.UseMiddleware<BradysAuthenticationSettings>(opts);
        }
    }
}
