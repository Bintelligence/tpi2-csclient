// Copyright (C) 2023 Bintelligence - The Netherlands.
// Licensed under the MIT license, see LICENSE.TXT for details.

namespace TuringPi2.Client
{
    using System.Collections.Generic;

    public class NodeInfo
    {
        public NodeInfo()
        {
            NodeInfoMessages = new Dictionary<string, string>();
        }

        public IDictionary<string, string> NodeInfoMessages { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public string Message(string nodeName)
        {
            if (this.NodeInfoMessages.TryGetValue(nodeName, out var message))
            {
                return message;
            }
            
            return default;
        }
    }
}