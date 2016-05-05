using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _1920Parser
{
    public partial class SaveSchemaForm : Form
    {
        private string data;

        public string Data
        {
            get { return data; }
            set { 
                data = value;
                tbDataIdentifier.Text = value.Substring(0, Math.Min(4, value.Length));
                tbSchemaName.Text = tbDataIdentifier.Text + ".txt";
                nudDataIdentifierPosition.Value = 1;
            }
        }
        private IEnumerable<string> filesAlreadyUsed = new string[0];
        private string name;

        public string Name1
        {
          get { return name; }
          set { name = value; }
        }
        private int position;

        public int Position
        {
          get { return position; }
          set { position = value; }
        }
        private string value;

        public string Value
        {
          get { return this.value; }
          set { this.value = value; }
        }

        public void setFilesAlreadyUsed(IEnumerable<string> files)
        {
            this.filesAlreadyUsed = files;
        }

        public SaveSchemaForm()
        {
            InitializeComponent();
            CenterToParent();
        }


        private void cbLoadSchemaAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            pnlSaveSchema.Enabled = cbLoadSchemaAutomatically.Checked;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Hide();
        }

        private void SaveSchemaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None) { DialogResult = DialogResult.Cancel; }
            Hide();
        }



        private void tbSchemaName_TextChanged(object sender, EventArgs e)
        {
            lblFileAlreadyExists.Visible = filesAlreadyUsed.Select(x => x.ToUpper()).Contains(tbSchemaName.Text.ToUpper());
        }


        private void nudDataIdentifierPosition_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int startIndex = int.Parse(nudDataIdentifierPosition.Text) - 1;
                tbDataIdentifier.Text = data.Substring(startIndex, Math.Min(data.Length - startIndex, 50));
            }
            catch
            {
                tbDataIdentifier.Text = "";
            }
        }
    }
}
