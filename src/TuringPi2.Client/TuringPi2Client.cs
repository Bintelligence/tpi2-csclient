// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client
{
    using Microsoft.Extensions.Options;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json.Nodes;
    using System.Threading;
    using System.Threading.Tasks;
    using TuringPi2.Client;
    using static TuringPi2.Client.OperationConstants;

    internal class TuringPi2Client : ITuringPi2Client
    {
        private HttpClient httpClient;

        public TuringPi2Client(IHttpClientFactory httpClientFactory, IOptions<TuringPi2ClientOptions> turingPi2Options)
        {
            if (httpClientFactory is null)
            {
                throw new ArgumentNullException(nameof(httpClientFactory));
            }

            if (turingPi2Options is null)
            {
                throw new ArgumentNullException(nameof(turingPi2Options));
            }

            httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = turingPi2Options.Value.Url;
        }

        public async Task ChangePowerAsync(CancellationToken cancellationToken = default, params NodePowerSetting[] powerSetting)
        {
            var path = BuildUrl(OperationSet, OperationTypePower, powerSetting);
            var response = await httpClient.PostAsync(path, new ByteArrayContent(new byte[0]), cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task<NodeInfo> GetNodeInfoAsync(CancellationToken cancellationToken = default)
        {
            var path = BuildUrl(OperationGet, OperationTypeNodeInfo);
            var response = await httpClient.PostAsync(path, new ByteArrayContent(new byte[0]), cancellationToken);
            response.EnsureSuccessStatusCode();

            // The JSON responses are a bit weird, having multiple responses in a response with various objects.
            var json = JsonNode.Parse(await response.Content.ReadAsStreamAsync());
            var nodeInfo = json["response"].AsArray().Single();
            return new NodeInfo()
            {
                NodeInfoMessages =
                {
                    [NodeNames.Node1] = nodeInfo[NodeNames.Node1].GetValue<string>(),
                    [NodeNames.Node2] = nodeInfo[NodeNames.Node2].GetValue<string>(),
                    [NodeNames.Node3] = nodeInfo[NodeNames.Node3].GetValue<string>(),
                    [NodeNames.Node4] = nodeInfo[NodeNames.Node4].GetValue<string>(),
                },
            };
        }

        public async Task<Power> GetPowerAsync(CancellationToken cancellationToken = default)
        {
            var path = BuildUrl(OperationGet, OperationTypePower);
            var response = await httpClient.PostAsync(path, new ByteArrayContent(new byte[0]), cancellationToken);
            response.EnsureSuccessStatusCode();

            var json = JsonNode.Parse(await response.Content.ReadAsStreamAsync());
            var jsonResponse = json["response"].AsArray().Single();
            return new Power()
            {
                PowerSettings =
                {
                    [NodeNames.Node1] = (PowerSetting)jsonResponse[NodeNames.Node1].GetValue<int>(),
                    [NodeNames.Node2] = (PowerSetting)jsonResponse[NodeNames.Node2].GetValue<int>(),
                    [NodeNames.Node3] = (PowerSetting)jsonResponse[NodeNames.Node3].GetValue<int>(),
                    [NodeNames.Node4] = (PowerSetting)jsonResponse[NodeNames.Node4].GetValue<int>(),
                },
            };
        }

        public async Task<SDCard> GetSDCardAsync(CancellationToken cancellationToken = default)
        {
            var path = BuildUrl(OperationGet, OperationTypeSDCard);
            var response = await httpClient.PostAsync(path, new ByteArrayContent(new byte[0]), cancellationToken);
            response.EnsureSuccessStatusCode();

            var json = JsonNode.Parse(await response.Content.ReadAsStreamAsync());
            var jsonResponse = json["response"].AsArray().Single();
            return new SDCard()
            {
                InUse = jsonResponse["use"].GetValue<long>(),
                Free = jsonResponse["free"].GetValue<long>(),
                Total = jsonResponse["total"].GetValue<long>(),
            };
        }

        public async Task<USB> GetUSBAsync(CancellationToken cancellationToken = default)
        {
            var path = BuildUrl(OperationGet, OperationTypeUSB);
            var response = await httpClient.PostAsync(path, new ByteArrayContent(new byte[0]), cancellationToken);
            response.EnsureSuccessStatusCode();

            var json = JsonNode.Parse(await response.Content.ReadAsStreamAsync());
            var jsonResponse = json["response"].AsArray().Single();
            return new USB()
            {
                Mode = (USBMode)jsonResponse["mode"].GetValue<int>(),
                NodeName = NodeNames.Name(jsonResponse["node"].GetValue<int>()),
            };
        }

        public async Task ResetNetworkAsync(CancellationToken cancellationToken = default)
        {
            var path = new StringBuilder(BuildUrl(OperationGet, OperationTypeNetwork))
                .Append("&cmd=reset");

            var response = await httpClient.PostAsync(path.ToString(), new ByteArrayContent(new byte[0]), cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task SetUSB(USB usb, CancellationToken cancellationToken = default)
        {
            var path = BuildUrl(OperationSet, OperationTypeFirmware, usb);

            var response = await httpClient.PostAsync(path.ToString(), new ByteArrayContent(new byte[0]), cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateFirmwareAsync(string filePath, CancellationToken cancellationToken = default)
        {
            var path = BuildUrl(OperationSet, OperationTypeFirmware);

            HttpResponseMessage response;
            using (var stream = File.OpenRead(filePath))
            {
                response = await httpClient.PostAsync(path, new StreamContent(stream), cancellationToken);
            }

            response.EnsureSuccessStatusCode();
        }

        private string BuildUrl(string operation, string operationType)
        {
            StringBuilder sb = new StringBuilder()
                .Append("/api/bmc")
                .Append("?opt=")
                .Append(operation)
                .Append("&type=")
                .Append(operationType);

            return sb.ToString();
        }

        private string BuildUrl(string operation, string operationType, NodePowerSetting[] powerSetting)
        {
            StringBuilder sb = new StringBuilder(BuildUrl(operation, operationType));

            foreach (var item in powerSetting)
            {
                sb.Append("&");
                sb.Append(item.NodeName);
                sb.Append("=");
                sb.Append((int)item.PowerSetting);
            }

            return sb.ToString();
        }

        private string BuildUrl(string operation, string operationType, USB usb)
        {
            StringBuilder sb = new StringBuilder(BuildUrl(operation, operationType))
                .Append("&mode=")
                .Append((int)usb.Mode)
                .Append("&node")
                .Append(NodeNames.Index(usb.NodeName) - 1);

            return sb.ToString();
        }
    }
}