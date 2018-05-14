﻿using System.Collections.Generic;

namespace Rocket.Core.Plugins.NuGet.Client.V3
{
    public class NuGetQueryResult
    {
        public int TotalHits { get; set; }
        
        public IEnumerable<NuGetPackage> Data { get; set; }
    }
}