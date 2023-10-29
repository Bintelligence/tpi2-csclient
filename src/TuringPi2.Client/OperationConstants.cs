// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal static class OperationConstants
    {
        public const string OperationSet = "set";
        public const string OperationGet = "get";

        public const string OperationTypePower = "power";
        public const string OperationTypeNodeInfo = "nodeinfo";
        public const string OperationTypeFirmware = "firmware";
        public const string OperationTypeUSB = "usb";
        public const string OperationTypeSDCard = "sdcard";
        public const string OperationTypeNetwork = "network";
    }
}
