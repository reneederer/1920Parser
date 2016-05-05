namespace _1920Parser
{
    partial class SaveSchemaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveSchemaForm));
            this.btnSaveSchema = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSchemaName = new System.Windows.Forms.TextBox();
            this.cbLoadSchemaAutomatically = new System.Windows.Forms.CheckBox();
            this.pnlSaveSchema = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDataIdentifier = new System.Windows.Forms.TextBox();
            this.nudDataIdentifierPosition = new System.Windows.Forms.NumericUpDown();
            this.lblFileAlreadyExists = new System.Windows.Forms.Label();
            this.pnlSaveSchema.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataIdentifierPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveSchema
            // 
            this.btnSaveSchema.Location = new System.Drawing.Point(12, 95);
            this.btnSaveSchema.Name = "btnSaveSchema";
            this.btnSaveSchema.Size = new System.Drawing.Size(116, 23);
            this.btnSaveSchema.TabIndex = 0;
            this.btnSaveSchema.Text = "Schema speichern";
            this.btnSaveSchema.UseVisualStyleBackColor = true;
            this.btnSaveSchema.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name des Schemas";
            // 
            // tbSchemaName
            // 
            this.tbSchemaName.Location = new System.Drawing.Point(118, 12);
            this.tbSchemaName.Name = "tbSchemaName";
            this.tbSchemaName.Size = new System.Drawing.Size(152, 20);
            this.tbSchemaName.TabIndex = 3;
            this.tbSchemaName.Text = "VK60";
            this.tbSchemaName.TextChanged += new System.EventHandler(this.tbSchemaName_TextChanged);
            // 
            // cbLoadSchemaAutomatically
            // 
            this.cbLoadSchemaAutomatically.AutoSize = true;
            this.cbLoadSchemaAutomatically.Checked = true;
            this.cbLoadSchemaAutomatically.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLoadSchemaAutomatically.Location = new System.Drawing.Point(12, 50);
            this.cbLoadSchemaAutomatically.Name = "cbLoadSchemaAutomatically";
            this.cbLoadSchemaAutomatically.Size = new System.Drawing.Size(305, 17);
            this.cbLoadSchemaAutomatically.TabIndex = 6;
            this.cbLoadSchemaAutomatically.Text = "Schema automatisch laden, wenn der Datenstrom ab Stelle";
            this.cbLoadSchemaAutomatically.UseVisualStyleBackColor = true;
            this.cbLoadSchemaAutomatically.CheckedChanged += new System.EventHandler(this.cbLoadSchemaAutomatically_CheckedChanged);
            // 
            // pnlSaveSchema
            // 
            this.pnlSaveSchema.Controls.Add(this.label3);
            this.pnlSaveSchema.Controls.Add(this.label2);
            this.pnlSaveSchema.Controls.Add(this.tbDataIdentifier);
            this.pnlSaveSchema.Controls.Add(this.nudDataIdentifierPosition);
            this.pnlSaveSchema.Location = new System.Drawing.Point(1, 32);
            this.pnlSaveSchema.Name = "pnlSaveSchema";
            this.pnlSaveSchema.Size = new System.Drawing.Size(628, 47);
            this.pnlSaveSchema.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(555, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "enhält";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(377, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "den Text";
            // 
            // tbDataIdentifier
            // 
            this.tbDataIdentifier.Location = new System.Drawing.Point(432, 16);
            this.tbDataIdentifier.Name = "tbDataIdentifier";
            this.tbDataIdentifier.Size = new System.Drawing.Size(117, 20);
            this.tbDataIdentifier.TabIndex = 1;
            this.tbDataIdentifier.Text = "VK60";
            // 
            // nudDataIdentifierPosition
            // 
            this.nudDataIdentifierPosition.Location = new System.Drawing.Point(317, 17);
            this.nudDataIdentifierPosition.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.nudDataIdentifierPosition.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDataIdentifierPosition.Name = "nudDataIdentifierPosition";
            this.nudDataIdentifierPosition.Size = new System.Drawing.Size(54, 20);
            this.nudDataIdentifierPosition.TabIndex = 0;
            this.nudDataIdentifierPosition.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDataIdentifierPosition.ValueChanged += new System.EventHandler(this.nudDataIdentifierPosition_ValueChanged);
            this.nudDataIdentifierPosition.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudDataIdentifierPosition_ValueChanged);
            // 
            // lblFileAlreadyExists
            // 
            this.lblFileAlreadyExists.AutoSize = true;
            this.lblFileAlreadyExists.ForeColor = System.Drawing.Color.Red;
            this.lblFileAlreadyExists.Location = new System.Drawing.Point(276, 15);
            this.lblFileAlreadyExists.Name = "lblFileAlreadyExists";
            this.lblFileAlreadyExists.Size = new System.Drawing.Size(346, 13);
            this.lblFileAlreadyExists.TabIndex = 8;
            this.lblFileAlreadyExists.Text = "Die Datei existiert bereits. Der Inhalt wird beim Speichern überschrieben.";
            this.lblFileAlreadyExists.Visible = false;
            // 
            // SaveSchemaForm
            // 
            this.AcceptButton = this.btnSaveSchema;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 127);
            this.Controls.Add(this.cbLoadSchemaAutomatically);
            this.Controls.Add(this.tbSchemaName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFileAlreadyExists);
            this.Controls.Add(this.btnSaveSchema);
            this.Controls.Add(this.pnlSaveSchema);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SaveSchemaForm";
            this.Text = "Schema speichern";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveSchemaForm_FormClosing);
            this.pnlSaveSchema.ResumeLayout(false);
            this.pnlSaveSchema.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataIdentifierPosition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveSchema;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbLoadSchemaAutomatically;
        private System.Windows.Forms.Panel pnlSaveSchema;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tbSchemaName;
        public System.Windows.Forms.TextBox tbDataIdentifier;
        public System.Windows.Forms.NumericUpDown nudDataIdentifierPosition;
        private System.Windows.Forms.Label lblFileAlreadyExists;
    }
}