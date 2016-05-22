//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Drawing;
//------------------------------------------------------------------------------
//---------- Made by Lonami Exo | http://lonamiwebs.github.io | January 2016 ----------
//------------------------------------------------------------------------------
// <copyright file="ExtractStringCmd.cs" company="LonamiWebs">
//     Copyright (c) LonamiWebs.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Windows.Forms;

namespace Localizer_Extension
{

    class ColoredStatusLabel : ToolStripStatusLabel
    {
        #region Enumerations

        public enum ErrorLevel { OK, Warning, Error }

        #endregion

        #region Set statuses

        public void ClearStatus() { SetStatus(string.Empty, ErrorLevel.OK); }

        public void SetStatus(string status) { SetStatus(status, ErrorLevel.OK); }
        public void SetWarn(string warning) { SetStatus(warning, ErrorLevel.Warning); }
        public void SetError(string error) { SetStatus(error, ErrorLevel.Error); }

        public void SetStatus(string status, ErrorLevel errorLevel)
        {
            Text = status;
            switch (errorLevel)
            {
                case ErrorLevel.OK: ForeColor = Color.DarkGreen; break;
                case ErrorLevel.Warning: ForeColor = Color.DarkOrange; break;
                case ErrorLevel.Error: ForeColor = Color.DarkRed; break;
            }
        }

        #endregion
    }
}
