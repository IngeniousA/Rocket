﻿using System;
using System.Collections.Generic;
using Rocket.API.Commands;
using Rocket.API.Configuration;
using Rocket.API.DependencyInjection;

namespace Rocket.API.Permissions
{
    /// <summary>
    ///     The Permission Provider is responsible for checking permissions.
    /// </summary>
    public interface IPermissionProvider : IProxyableService
    {
        // Note: do not add something like GetPermissions()!

        /// <summary>
        ///     Defines if the given target is supported by this provider.
        /// </summary>
        /// <param name="target">The target to check.</param>
        /// <returns><b>true</b> if the given target is supported; otherwise, <b>false</b>.</returns>
        bool SupportsTarget(IIdentifiable target);

        /// <summary>
        ///     Check if the target has the given permission.
        /// </summary>
        /// <param name="target">The target to check.</param>
        /// <param name="permission">The permission to check.</param>
        /// <returns><see cref="PermissionResult.Grant"/> if the target explicity has the permission, <see cref="PermissionResult.Deny"/> if the target explicitly does not have the permission; otherwise, <see cref="PermissionResult.Default"/></returns>
        PermissionResult CheckPermission(IIdentifiable target, string permission);

        /// <summary>
        ///     Checks if the target has all of the given permissions.
        /// </summary>
        /// <param name="target">The target to check.</param>
        /// <param name="permissions">The permissions to check.</param>
        /// <returns><see cref="PermissionResult.Grant"/> if the target explicity has access to all of the given permissions, <see cref="PermissionResult.Deny"/> if the target explicitly does not have access to any of the permissions; otherwise, <see cref="PermissionResult.Default"/></returns>
        PermissionResult CheckHasAllPermissions(IIdentifiable target, params string[] permissions);

        /// <summary>
        ///     Checks if the target has any of the given permissions.
        /// </summary>
        /// <param name="target">The target to check.</param>
        /// <param name="permissions">The permissions to check.</param>
        /// <returns><see cref="PermissionResult.Grant"/> if the target explicity has access to any of the given permissions, <see cref="PermissionResult.Deny"/> if the target explicitly does not have access to any of the permissions; otherwise, <see cref="PermissionResult.Default"/></returns>
        PermissionResult CheckHasAnyPermission(IIdentifiable target, params string[] permissions);

        /// <summary>
        ///     Adds an explicitly granted permission to the target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="permission">The permission to add.</param>
        /// <returns><b>true</b> if the permission was successfully added; otherwise, <b>false</b>.</returns>
        bool AddPermission(IIdentifiable target, string permission);

        /// <summary>
        ///     Adds an explicitly denied permission to the target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="permission">The denied permission to add.</param>
        /// <returns><b>true</b> if the permission was successfully added; otherwise, <b>false</b>.</returns>
        bool AddDeniedPermission(IIdentifiable target, string permission);

        /// <summary>
        ///     Removes an explicitly granted permission from the target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="permission">The permission to remove.</param>
        /// <returns><b>true</b> if the permission was successfully removed; otherwise, <b>false</b>.</returns>
        bool RemovePermission(IIdentifiable target, string permission);

        /// <summary>
        ///     Removes an explicitly denied permission from the target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="permission">The permission to remove.</param>
        /// <returns><b>true</b> if the permission was successfully removed; otherwise, <b>false</b>.</returns>
        bool RemoveDeniedPermission(IIdentifiable target, string permission);

        /// <summary>
        ///     Gets the primary group of the given command caller.
        /// </summary>
        /// <param name="caller">The command caller.</param>
        /// <returns>the primary group if it exists; otherwise, <b>null</b>.</returns>
        [Obsolete("Might be removed")]
        IPermissionGroup GetPrimaryGroup(ICommandCaller caller);

        /// <summary>
        ///     Gets the primary group with the given ID.
        /// </summary>
        /// <param name="id">The ID of the group.</param>
        /// <returns>the group if it exists; otherwise, <b>null</b>.</returns>
        IPermissionGroup GetGroup(string id);

        /// <summary>
        ///     Gets all inherited groups of the target. If target is a group itself, it will return the parent groups.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>the inherited groups of the target.</returns>
        IEnumerable<IPermissionGroup> GetGroups(IIdentifiable target);
        
        /// <summary>
        ///     Gets all registered groups.
        /// </summary>
        /// <returns>all registed groups of this provider.</returns>
        IEnumerable<IPermissionGroup> GetGroups();

        /// <summary>
        ///     Updates a group.
        /// </summary>
        /// <param name="group">The group to update.</param>
        void UpdateGroup(IPermissionGroup group);

        /// <summary>
        ///     Adds the given group to the command caller.
        /// </summary>
        /// <param name="caller">The command caller.</param>
        /// <param name="group">The group to add.</param>
        /// <returns><b>true</b> if the group was successfully added; otherwise, <b>false</b>.</returns>
        bool AddGroup(IIdentifiable caller, IPermissionGroup group);

        /// <summary>
        ///     Removes the given group from the command caller.
        /// </summary>
        /// <param name="caller">The command caller.</param>
        /// <param name="group">The group to remove.</param>
        /// <returns><b>true</b> if the group was successfully removed; otherwise, <b>false</b>.</returns>
        bool RemoveGroup(IIdentifiable caller, IPermissionGroup group);

        /// <summary>
        ///     Creates a new permission group.
        /// </summary>
        /// <param name="group">The group to create.</param>
        /// <returns><b>true</b> if the group was successfully created; otherwise, <b>false</b>.</returns>
        bool CreateGroup(IPermissionGroup group);

        /// <summary>
        ///     Creates a permission group.
        /// </summary>
        /// <param name="group">The group to delete.</param>
        /// <returns><b>true</b> if the group was successfully deleted; otherwise, <b>false</b>.</returns>
        bool DeleteGroup(IPermissionGroup group);

        /// <summary>
        ///     Loads the permissions from the given context.
        /// </summary>
        /// <param name="context">the configuration context to load the permissions from.</param>
        void Load(IConfigurationContext context);

        /// <summary>
        ///     Reloads the permissions from the context which was used to initially load them.<br/><br/>
        ///     May override not saved changes.
        /// </summary>
        void Reload();

        /// <summary>
        ///     Saves the changes to the permissions.
        /// </summary>
        void Save();
    }
}