// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class ConfigureClientOptions : IConfigureOptions<TuringPi2ClientOptions>, IValidateOptions<TuringPi2ClientOptions>
    {
        private readonly IConfiguration configuration;

        public ConfigureClientOptions(IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            this.configuration = configuration;
        }

        /// <inheritdoc/>
        public void Configure(TuringPi2ClientOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            configuration.Bind(options);
        }

        /// <inheritdoc/>
        public ValidateOptionsResult Validate(string name, TuringPi2ClientOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var errors = new List<string>();

            if (options.Url == default)
            {
                errors.Add($"{nameof(TuringPi2ClientOptions.Url)} is required.");
            }

            if (errors.Any())
            {
                return ValidateOptionsResult.Fail(errors);
            }

            return ValidateOptionsResult.Success;
        }
    }
}
