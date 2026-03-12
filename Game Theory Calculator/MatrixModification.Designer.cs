namespace Game_Theory_Calculator
{
    partial class MatrixModification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatrixModification));
            this.MatrixBlueprint = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveChanges = new System.Windows.Forms.Button();
            this.AddRow = new System.Windows.Forms.Button();
            this.DeleteRow = new System.Windows.Forms.Button();
            this.AddColumn = new System.Windows.Forms.Button();
            this.DeleteColumn = new System.Windows.Forms.Button();
            this.DeleteMatrixButton = new System.Windows.Forms.Button();
            this.saved_back = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MatrixBlueprint)).BeginInit();
            this.SuspendLayout();
            // 
            // MatrixBlueprint
            // 
            this.MatrixBlueprint.AllowUserToAddRows = false;
            this.MatrixBlueprint.AllowUserToDeleteRows = false;
            this.MatrixBlueprint.AllowUserToOrderColumns = true;
            this.MatrixBlueprint.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MatrixBlueprint.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.MatrixBlueprint.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.MatrixBlueprint.ColumnHeadersHeight = 46;
            this.MatrixBlueprint.ColumnHeadersVisible = false;
            this.MatrixBlueprint.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.MatrixBlueprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MatrixBlueprint.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.MatrixBlueprint.Location = new System.Drawing.Point(0, 0);
            this.MatrixBlueprint.Name = "MatrixBlueprint";
            this.MatrixBlueprint.RowHeadersWidth = 85;
            this.MatrixBlueprint.RowTemplate.Height = 33;
            this.MatrixBlueprint.Size = new System.Drawing.Size(1169, 619);
            this.MatrixBlueprint.TabIndex = 0;
            this.MatrixBlueprint.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.MinimumWidth = 10;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.MinimumWidth = 10;
            this.Column2.Name = "Column2";
            // 
            // SaveChanges
            // 
            this.SaveChanges.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SaveChanges.Location = new System.Drawing.Point(1043, 559);
            this.SaveChanges.Name = "SaveChanges";
            this.SaveChanges.Size = new System.Drawing.Size(114, 48);
            this.SaveChanges.TabIndex = 1;
            this.SaveChanges.Text = "Save";
            this.SaveChanges.UseVisualStyleBackColor = true;
            this.SaveChanges.Click += new System.EventHandler(this.SaveChanges_Click);
            // 
            // AddRow
            // 
            this.AddRow.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.AddRow.Location = new System.Drawing.Point(33, 507);
            this.AddRow.Name = "AddRow";
            this.AddRow.Size = new System.Drawing.Size(132, 35);
            this.AddRow.TabIndex = 2;
            this.AddRow.Text = "Add Row";
            this.AddRow.UseVisualStyleBackColor = true;
            this.AddRow.Click += new System.EventHandler(this.AddRow_Click);
            // 
            // DeleteRow
            // 
            this.DeleteRow.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.DeleteRow.Location = new System.Drawing.Point(22, 553);
            this.DeleteRow.Name = "DeleteRow";
            this.DeleteRow.Size = new System.Drawing.Size(153, 35);
            this.DeleteRow.TabIndex = 3;
            this.DeleteRow.Text = "Delete Row";
            this.DeleteRow.UseVisualStyleBackColor = true;
            this.DeleteRow.Click += new System.EventHandler(this.DeleteRow_Click);
            // 
            // AddColumn
            // 
            this.AddColumn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.AddColumn.Location = new System.Drawing.Point(232, 507);
            this.AddColumn.Name = "AddColumn";
            this.AddColumn.Size = new System.Drawing.Size(169, 35);
            this.AddColumn.TabIndex = 4;
            this.AddColumn.Text = "Add Column";
            this.AddColumn.UseVisualStyleBackColor = true;
            this.AddColumn.Click += new System.EventHandler(this.AddColumn_Click);
            // 
            // DeleteColumn
            // 
            this.DeleteColumn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.DeleteColumn.Location = new System.Drawing.Point(217, 553);
            this.DeleteColumn.Name = "DeleteColumn";
            this.DeleteColumn.Size = new System.Drawing.Size(193, 35);
            this.DeleteColumn.TabIndex = 5;
            this.DeleteColumn.Text = "Delete Column";
            this.DeleteColumn.UseVisualStyleBackColor = true;
            this.DeleteColumn.Click += new System.EventHandler(this.DeleteColumn_Click);
            // 
            // DeleteMatrixButton
            // 
            this.DeleteMatrixButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.DeleteMatrixButton.Location = new System.Drawing.Point(744, 559);
            this.DeleteMatrixButton.Name = "DeleteMatrixButton";
            this.DeleteMatrixButton.Size = new System.Drawing.Size(185, 48);
            this.DeleteMatrixButton.TabIndex = 8;
            this.DeleteMatrixButton.Text = "Delete Matrix";
            this.DeleteMatrixButton.UseVisualStyleBackColor = true;
            this.DeleteMatrixButton.Click += new System.EventHandler(this.DeleteMatrixButton_Click);
            // 
            // saved_back
            // 
            this.saved_back.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.saved_back.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("saved_back.BackgroundImage")));
            this.saved_back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.saved_back.Location = new System.Drawing.Point(951, 556);
            this.saved_back.Name = "saved_back";
            this.saved_back.Size = new System.Drawing.Size(69, 54);
            this.saved_back.TabIndex = 9;
            this.saved_back.UseVisualStyleBackColor = true;
            this.saved_back.Click += new System.EventHandler(this.saved_back_Click);
            // 
            // MatrixModification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 619);
            this.Controls.Add(this.saved_back);
            this.Controls.Add(this.DeleteMatrixButton);
            this.Controls.Add(this.DeleteColumn);
            this.Controls.Add(this.AddColumn);
            this.Controls.Add(this.DeleteRow);
            this.Controls.Add(this.AddRow);
            this.Controls.Add(this.SaveChanges);
            this.Controls.Add(this.MatrixBlueprint);
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MatrixModification";
            this.Text = "Matrix Modification";
            this.Load += new System.EventHandler(this.MatrixModification_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MatrixBlueprint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView MatrixBlueprint;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button SaveChanges;
        private System.Windows.Forms.Button AddRow;
        private System.Windows.Forms.Button DeleteRow;
        private System.Windows.Forms.Button AddColumn;
        private System.Windows.Forms.Button DeleteColumn;
        private System.Windows.Forms.Button DeleteMatrixButton;
        private System.Windows.Forms.Button saved_back;
    }
}