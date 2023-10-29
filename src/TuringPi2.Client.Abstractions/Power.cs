// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client
{
    using System;
    using System.Collections.Generic;

    public class Power
    {
        public Power()
        {
            PowerSettings = new Dictionary<string, PowerSetting>();
        }

        public IDictionary<string, PowerSetting> PowerSettings { get; }

        public PowerSetting GetPowerSetting(string nodeName)
        {
            if (string.IsNullOrWhiteSpace(nodeName))
            {
                throw new ArgumentException($"'{nameof(nodeName)}' cannot be null or whitespace.", nameof(nodeName));
            }

            if (PowerSettings.TryGetValue(nodeName, out PowerSetting setting))
            {
                return setting;
            }

            throw new ArgumentException($"{nameof(nodeName)} does not contain a valid node name or this node is not part of the results.");
        }
    }
}