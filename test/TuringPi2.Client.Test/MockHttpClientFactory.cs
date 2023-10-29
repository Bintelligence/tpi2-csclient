// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client.Test
{
    using RichardSzalay.MockHttp;
    using System;
    using System.Net.Http;

    internal class MockHttpClientFactory : IHttpClientFactory, IDisposable
    {
        public MockHttpClientFactory()
        {
        }

        public MockHttpMessageHandler Handler { get; } = new MockHttpMessageHandler();


        public HttpClient CreateClient(string name)
        {
            return new HttpClient(Handler);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        /// <param name="disposing">true when disposing via <see cref="Dispose()"/>, otherwise false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Handler?.Dispose();
            }
        }
    }
}
