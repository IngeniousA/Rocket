﻿using System;
using System.Collections.Generic;
using System.Linq;
using Rocket.API.Commands;

namespace Rocket.Core.Commands
{
    public static class CommandProviderExtensions
    {
        public static ICommand GetCommand(this IEnumerable<ICommand> commandsEnumerable,string commandName, ICommandCaller caller)
        {
            var commands = commandsEnumerable.ToList();
            var command =
                commands
                    .Where(c => caller == null || c.SupportsCaller(caller.GetType()))
                    .FirstOrDefault(c => c.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase));
            if (command != null)
                return command;

            return commands
                   .Where(c => caller == null || c.SupportsCaller(caller.GetType()))
                   .FirstOrDefault(c => c.Aliases.Any(a => a.Equals(commandName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}