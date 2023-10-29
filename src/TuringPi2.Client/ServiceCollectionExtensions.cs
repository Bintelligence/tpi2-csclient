// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTuringPi2Client(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddOptions<TuringPi2ClientOptions>();
            services.TryAddTransient<IConfigureOptions<TuringPi2ClientOptions>, ConfigureClientOptions>();
            services.TryAddTransient<IValidateOptions<TuringPi2ClientOptions>, ConfigureClientOptions>();
            services.TryAddTransient<ITuringPi2Client, TuringPi2Client>();

            return services;
        }
    }
}

