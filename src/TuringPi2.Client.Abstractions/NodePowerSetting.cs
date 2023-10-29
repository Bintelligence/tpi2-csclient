// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client
{
    using System;

    public class NodePowerSetting
    {
        public NodePowerSetting(string nodeName, PowerSetting powerSetting)
        {
            if (string.IsNullOrWhiteSpace(nodeName))
            {
                throw new ArgumentException($"'{nameof(nodeName)}' cannot be null or whitespace.", nameof(nodeName));
            }

            if (!NodeNames.Validate(nodeName))
            {
                throw new ArgumentException($"{nameof(nodeName)} contains an invalid value.");
            }

            if (!Enum.IsDefined(typeof(NodePowerSetting), powerSetting))
            {
                throw new ArgumentException($"{nameof(powerSetting)} contains an invalid value.");
            }

            this.NodeName = nodeName;
            this.PowerSetting = powerSetting;
        }

        /// <summary>
        /// Gets the name of the node.
        /// </summary>
        public string NodeName { get; }

        /// <summary>
        /// Gets the power setting used for this node.
        /// </summary>
        public PowerSetting PowerSetting { get; }
    }
}
