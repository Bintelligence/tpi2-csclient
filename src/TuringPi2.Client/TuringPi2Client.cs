// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPI2.Client
{
    using Microsoft.Extensions.Options;
    using System.Threading.Tasks;
    using TuringPi2.Client;

    internal class TuringPi2Client : ITuringPi2Client
    {
        private HttpClient httpClient;

        internal TuringPi2Client(IHttpClientFactory httpClientFactory, IOptions<TuringPi2ClientOptions> turingPi2Options)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = turingPi2Options.Value.Url;
        }

        public Task ChangePowerAsync(params NodePowerSetting[] powerSetting)
        {
            throw new NotImplementedException();
        }

        public Task<NodeInfo> GetNodeInfoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Power> GetPowerAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SDCard> GetSDCardAsync()
        {
            throw new NotImplementedException();
        }

        public Task<USB> GetUsbAsync()
        {
            throw new NotImplementedException();
        }

        public Task ResetNetworkAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateFirmwareAsync(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}