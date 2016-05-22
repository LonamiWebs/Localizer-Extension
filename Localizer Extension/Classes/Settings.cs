//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.VisualStudio.Settings;

namespace Localizer_Extension
{
    public static class Settings
    {
        #region Constant values

        const string collectionName = "Localizer Extension";

        // default values
        const string default_resDirName = "res";
        const string default_resManName = "Res";
        const bool default_useXamlStatic = true;
        const bool default_setLocaleInit = true;

        #endregion

        #region Initialization

        static WritableSettingsStore settings;
        public static void Init()
        {
            settings = VsUtils.GetSettingsStore();
            if (settings.CollectionExists(collectionName))
                Load();
            else
            {
                settings.CreateCollection(collectionName);
                Save();
            }
        }

        #endregion

        #region Get settings

        public static string ResourcesFolderName  { get; set; } = default_resDirName;
        public static string ResourcesManagerName { get; set; } = default_resManName;
        public static bool UseStaticResourceXAML  { get; set; } = default_useXamlStatic;
        public static bool SetLocaleOnStartup     { get; set; } = default_setLocaleInit;

        #endregion

        #region Load and save

        public static void Load()
        {
            ResourcesFolderName = settings.GetString(collectionName, nameof(ResourcesFolderName));
            ResourcesManagerName = settings.GetString(collectionName, nameof(ResourcesManagerName));
            UseStaticResourceXAML = settings.GetBoolean(collectionName, nameof(UseStaticResourceXAML));
            SetLocaleOnStartup = settings.GetBoolean(collectionName, nameof(SetLocaleOnStartup));
        }
        public static void Save()
        {
            settings.SetString(collectionName, nameof(ResourcesFolderName), ResourcesFolderName);
            settings.SetString(collectionName, nameof(ResourcesManagerName), ResourcesManagerName);
            settings.SetBoolean(collectionName, nameof(UseStaticResourceXAML), UseStaticResourceXAML);
            settings.SetBoolean(collectionName, nameof(SetLocaleOnStartup), SetLocaleOnStartup);
        }

        #endregion

        #region Reset

        public static void Reset()
        {
            ResourcesFolderName   = default_resDirName;
            ResourcesManagerName  = default_resManName;
            UseStaticResourceXAML = default_useXamlStatic;
            SetLocaleOnStartup    = default_setLocaleInit;
        }

        #endregion
    }
}
