using System.Windows.Forms;

namespace Localizer_Extension
{
    public partial class EditStringBox : Form
    {
        #region Show

        public static string Show(string key, string value)
        {
            using (var itb = new EditStringBox(key, value))
            {
                itb.ShowDialog();
                return itb.Value;
            }
        }

        #endregion

        #region Properties

        string Value => DialogResult == DialogResult.OK ? valueTB.Text : null;

        #endregion

        #region Constructor

        EditStringBox(string key, string value)
        {
            InitializeComponent();

            keyTB.Text = key;
            valueTB.Text = value;

            valueTB.SelectionStart = valueTB.Text.Length;
        }

        #endregion

        #region Accept and cancel

        void acceptB_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        void cancelB_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region Events

        void EdiStringBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
                valueTB.Text = string.Empty;
        }

        #endregion
    }
}
