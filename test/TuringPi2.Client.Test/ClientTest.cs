// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client.Test
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using RichardSzalay.MockHttp;
    using System;

    public abstract class ClientTest : IDisposable
    {
        private ServiceProvider serviceProvider;
        private MockHttpClientFactory httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientTest"/> class.
        /// </summary>
        protected ClientTest()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>()
                {
                    ["Url"] = "http://turingpi",
                });

            httpClientFactory = new MockHttpClientFactory();
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingleton<IConfiguration>(configurationBuilder.Build())
                .AddSingleton<IHttpClientFactory>(httpClientFactory)
                .AddTuringPi2Client();

            serviceProvider = serviceCollection.BuildServiceProvider();
            Client = serviceProvider.GetRequiredService<ITuringPi2Client>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ClientTest"/> class.
        /// </summary>
        ~ClientTest()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                // This is done because XUnit uses Dispose() as a test finalizer.
                // We can do some final checks to see if nothing is left dangling.
                MockHandler.VerifyNoOutstandingExpectation();
                MockHandler.VerifyNoOutstandingRequest();
            }
            finally
            {
                if (disposing && !IsDisposed)
                {
                    httpClientFactory?.Dispose();
                    serviceProvider?.Dispose();
                }

                IsDisposed = true;
            }
        }

        public ITuringPi2Client Client { get; private set; }

        public bool IsDisposed { get; private set; }

        protected MockHttpMessageHandler MockHandler => httpClientFactory.Handler;
    }
}
