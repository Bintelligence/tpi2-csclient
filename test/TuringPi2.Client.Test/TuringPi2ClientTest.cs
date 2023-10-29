// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client.Test
{
    using RichardSzalay.MockHttp;
    using System.Net;
    using Xunit;

    public class TuringPi2ClientTest : ClientTest
    {
        public TuringPi2ClientTest()
        {
        }

        [Fact]
        public async Task GetNodeInfoAsync()
        {
            MockHandler
                .Expect(HttpMethod.Post, "http://turingpi/api/bmc?opt=get&type=nodeinfo")
                .Respond("application/json", Responses.NodeInfo());

            var response = await Client.GetNodeInfoAsync();

            Assert.NotNull(response);
            Assert.Equal("unknown", response.NodeInfoMessages[NodeNames.Node1]);
            Assert.Equal("[jetson]-Ubuntu 18.04.6 LTS BrotherEye ", response.NodeInfoMessages[NodeNames.Node2]);
            Assert.Equal("unknown", response.Message(NodeNames.Node1));
        }

        [Fact]
        public async Task GetPowerAsyc()
        {
            MockHandler
                .Expect(HttpMethod.Post, "http://turingpi/api/bmc?opt=get&type=power")
                .Respond("application/json", Responses.Power());

            var response = await Client.GetPowerAsync();

            Assert.NotNull(response);
            Assert.Equal(PowerSetting.On, response.GetPowerSetting(NodeNames.Node1));
            Assert.Equal(PowerSetting.Off, response.GetPowerSetting(NodeNames.Node2));
            Assert.Equal(PowerSetting.Off, response.GetPowerSetting(NodeNames.Node3));
            Assert.Equal(PowerSetting.Off, response.GetPowerSetting(NodeNames.Node4));
        }

        [Fact]
        public async Task GetUSBAsync()
        {
            MockHandler
                .Expect(HttpMethod.Post, "http://turingpi/api/bmc?opt=get&type=usb")
                .Respond("application/json", Responses.USB());

            var response = await Client.GetUSBAsync();
            Assert.NotNull(response);

            Assert.Equal(NodeNames.Node1, response.NodeName);
            Assert.Equal(USBMode.Device, response.Mode);
        }

        [Fact]
        public async Task GetSDCardAsync()
        {
            MockHandler
                .Expect(HttpMethod.Post, "http://turingpi/api/bmc?opt=get&type=sdcard")
                .Respond("application/json", Responses.SDCard());

            var response = await Client.GetSDCardAsync();
            Assert.NotNull(response);

            Assert.Equal(5, response.Free);
            Assert.Equal(10, response.InUse);
            Assert.Equal(15, response.Total);
        }

        [Fact]
        public async Task ChangePowerAsync()
        {
            MockHandler
                .Expect(HttpMethod.Post, "http://turingpi/api/bmc?opt=set&type=power&node1=1&node2=1")
                .Respond("application/json", Responses.OK());

            await Client.ChangePowerAsync(default, new NodePowerSetting(NodeNames.Node1, PowerSetting.On), new NodePowerSetting(NodeNames.Node2, PowerSetting.On));
        }

        [Fact]
        public async Task ResetNetworkAsync()
        {
            MockHandler
                .Expect(HttpMethod.Post, "http://turingpi/api/bmc?opt=set&type=network&cmd=reset")
                .Respond("application/json", Responses.OK());

            await Client.ResetNetworkAsync();
        }

        [Fact]
        public async Task UpdateFirmwareAsync()
        {
            // Generate a file with some random content
            string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".csv";

            try
            {
                var buffer = new byte[1024 * 64];
                var rng = new Random();
                rng.NextBytes(buffer);
                File.WriteAllBytes(fileName, buffer);

                MockHandler
                    .Expect(HttpMethod.Post, "http://turingpi/api/bmc?opt=set&type=firmware")
                    .With(req =>
                    {
                        var byteContents = req.Content!.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                        Assert.Equal(buffer, byteContents);
                        Assert.NotSame(buffer, byteContents);
                        return true;
                    })
                    .Respond("application/json", Responses.OK());

                await Client.UpdateFirmwareAsync(fileName);
            }
            finally
            {
                // Clean up.
                File.Delete(fileName);
            }
        }

        [Fact]
        public async Task SetUSBAsync()
        {
            MockHandler
                .Expect(HttpMethod.Post, "http://turingpi/api/bmc?opt=set&type=usb&mode=1&node=0")
                .Respond("application/json", Responses.OK());

            await Client.SetUSBAsync(new USB() { Mode = USBMode.Device, NodeName = NodeNames.Node1 });
        }
    }
}