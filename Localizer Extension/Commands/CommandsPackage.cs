//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Localizer_Extension
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(CommandsPackage.PackageGuidString)]
    [ProvideAutoLoad(UIContextGuids.SolutionExists)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class CommandsPackage : Package
    {
        // commandsPackage GUID string
        public const string PackageGuidString = "ad2d64c8-fa3b-4c73-b078-35bbb8bb1354";
        
        // initialization of the package
        protected override void Initialize()
        {
            VsUtils.Init(this);
            Settings.Init();

            ExtractStringCmd.Initialize(this);
            ManageStringsCmd.Initialize(this);

            base.Initialize();
            AddResourceCmd.Initialize(this);
        }
    }
}
