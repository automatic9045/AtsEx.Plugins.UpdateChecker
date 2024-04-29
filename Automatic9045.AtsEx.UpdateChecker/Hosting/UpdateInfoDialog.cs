using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automatic9045.AtsEx.UpdateChecker.Hosting
{
    internal sealed partial class UpdateInfoDialog : Form
    {
        public bool DoNotShowAgain => DoNotShowAgainCheckBox.Checked;

        public UpdateInfoDialog(Version currentVersion, Version newVersion, string updateDetailsHtml)
        {
            InitializeComponent(currentVersion, newVersion, updateDetailsHtml);
        }
    }
}
