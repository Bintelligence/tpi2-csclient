// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client
{
    using System;

    public static class NodeNames
    {
        public const string Node1 = "node1";
        public const string Node2 = "node2";
        public const string Node3 = "node3";
        public const string Node4 = "node4";

        public static int Index(string nodeName)
        {
            if (string.IsNullOrWhiteSpace(nodeName))
            {
                throw new ArgumentException($"'{nameof(nodeName)}' cannot be null or whitespace.", nameof(nodeName));
            }

            switch (nodeName)
            {
                case Node1: return 1;
                case Node2: return 2;
                case Node3: return 3;
                case Node4: return 4;
                default: return default;
            }
        }

        public static string Name(int index)
        {
            switch (index)
            {
                case 1: return Node1;
                case 2: return Node2;
                case 3: return Node3;
                case 4: return Node4;
                default: throw new ArgumentOutOfRangeException(nameof(index), index, $"{nameof(index)} must be between 1 and 4");
            }
        }

        public static bool Validate(string nodeName)
        {
            switch (nodeName)
            {
                case Node1: return true;
                case Node2: return true;
                case Node3: return true;
                case Node4: return true;
                default: return false;
            }
        }
    }
}
