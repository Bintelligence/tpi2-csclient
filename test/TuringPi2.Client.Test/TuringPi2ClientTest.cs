// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client.Test
{
    using RichardSzalay.MockHttp;

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
    }
}