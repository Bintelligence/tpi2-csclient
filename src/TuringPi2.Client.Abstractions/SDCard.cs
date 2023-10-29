// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client
{
    /// <summary>
    /// Represents the status of the BMC SD Card.
    /// </summary>
    public class SDCard
    {
        /// <summary>
        /// Gets the total space of the SD card.
        /// </summary>
        /// <remarks>
        /// If this is 0 it probably means there's no SD card mounted by the BMC.
        /// </remarks>
        public long Total { get; set; }

        /// <summary>
        /// Gets the free space available.
        /// </summary>
        public long Free { get; set; }

        /// <summary>
        /// Gets the space that's in use on the card.
        /// </summary>
        public long InUse { get; set; }
    }
}