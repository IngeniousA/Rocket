﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rocket.API;
using Rocket.API.Logging;

namespace Rocket.Tests
{
    public abstract class RocketTestBase
    {
        protected IRuntime Runtime { get; private set; }

        [AssemblyInitialize]
        public static void Startup(TestContext testContext) { }

        [TestInitialize]
        public virtual void Bootstrap()
        {
            Runtime = new Rocket.Runtime();
            Runtime.BootstrapAsync().GetAwaiter().GetResult();
            Runtime.Container.RegisterSingletonType<ILogger, NullLogger>("default_file_logger");
        }
    }
}