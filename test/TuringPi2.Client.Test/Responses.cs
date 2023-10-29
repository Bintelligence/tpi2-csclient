// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    internal static class Responses
    {
        public static string NodeInfo() => GetResourceContent("nodeinfo.json");

        public static string Power() => GetResourceContent("power.json");

        public static string USB() => GetResourceContent("usbmode.json");

        private static string GetResourceContent(string name)
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"TuringPi2.Client.Test.Responses.{name}");
            using var reader = new StreamReader(stream!);
            return reader.ReadToEnd();
        }
    }
}
