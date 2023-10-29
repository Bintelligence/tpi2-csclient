// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client
{
    using System;

    /// <summary>
    /// The settings for the Turing Pi 2 client.
    /// </summary>
    public class TuringPi2ClientOptions
    {
        /// <summary>
        /// The URL of the Turing Pi 2. (Including http://...)
        /// </summary>
        public Uri Url { get; set; }
    }
}
