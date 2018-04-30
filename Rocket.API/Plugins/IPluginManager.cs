using System;
using System.Collections.Generic;
using Rocket.API.ServiceProxies;

namespace Rocket.API.Plugins
{
    public interface IPluginManager : IEnumerable<IPlugin>, IProxyableService
    {
        IEnumerable<IPlugin> Plugins { get; }
        IPlugin GetPlugin(string name);

        bool PluginExists(string name);

        void Init();

        bool LoadPlugin(string name);

        bool UnloadPlugin(string name);

        /// <summary>
        ///     Executes code that depends on another plugin being available.
        ///     Because directly referencing another plugin breaks a plugin if the referenced plugin is unavailable
        ///     you should pass the name of the expected plugin to this method and write all code dependend on that
        ///     assembly inside of the delegate you pass to this method.
        /// </summary>
        /// <example>
        ///     pluginManager.ExecutePluginDependendCode("FancyFeast, (IRocketPlugin plugin) =>{
        ///     Feast pluginInstance = (Feast) plugin;
        ///     pluginInstance.StartFeast();
        ///     });
        /// </example>
        /// <param name="pluginName">Name of the plugin to look for</param>
        /// <param name="action">Delegate method to be invoked</param>
        /// <returns>If true then plugin was found and delegate executed</returns>
        bool ExecutePluginDependendCode(string pluginName, Action<IPlugin> action);
    }
}