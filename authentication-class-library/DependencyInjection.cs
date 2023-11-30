using authentication_class_library.Interfaces.UserInterface;
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
    public static class DependencyInjection
    {

        public static BradysAuthenticationSettings bas;
        public static IServiceCollection AddBradysAuthentication(this IServiceCollection services, Action<BradysAuthenticationSettings> setupAction)
        {
            //add all the interfaces and implementations
            services.AddScoped<IUserInterface, UserInterface>();

            services.Configure(setupAction);

            return services;
        }


    }

}
