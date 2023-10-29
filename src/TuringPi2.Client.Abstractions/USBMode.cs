// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents the mode of the USB port.
    /// </summary>
    public enum USBMode
    {
        /// <summary>
        /// The USB port is connected to the host (i.e. external SD-card)
        /// </summary>
        Host = 0,

        /// <summary>
        /// The USB port is connected to the device (i.e. EMMC memory.)
        /// </summary>
        Device = 1,
    }
}
