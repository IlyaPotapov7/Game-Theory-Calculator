namespace Game_Theory_Calculator
{
    partial class mainWindow
    {

        private System.ComponentModel.IContainer components = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.ModelSelection = new System.Windows.Forms.Label();
            this.MatrixInitialise = new System.Windows.Forms.Button();
            this.tutorialButton = new System.Windows.Forms.Button();
            this.ModelSelectionPannel = new System.Windows.Forms.Panel();
            this.SolveConnection = new System.Windows.Forms.Button();
            this.DeleteEntireConnection = new System.Windows.Forms.Button();
            this.DeleteComponent = new System.Windows.Forms.Button();
            this.CancelSelectedCell = new System.Windows.Forms.Button();
            this.saveConnection = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.zoom_to_default = new System.Windows.Forms.Button();
            this.lockalise_matricies = new System.Windows.Forms.Button();
            this.return_to_origin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.solveButton = new System.Windows.Forms.Button();
            this.ConnectionInitialise = new System.Windows.Forms.Button();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ExitMatrixSelection = new System.Windows.Forms.Button();
            this.ExitConnectionSelection = new System.Windows.Forms.Button();
            this.ConnectionInitialiseIndicator = new System.Windows.Forms.Label();
            this.ChoosingMatrixBool = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ModelSelectionPannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ModelSelection
            // 
            this.ModelSelection.AutoSize = true;
            this.ModelSelection.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModelSelection.Location = new System.Drawing.Point(46, 108);
            this.ModelSelection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ModelSelection.Name = "ModelSelection";
            this.ModelSelection.Size = new System.Drawing.Size(261, 42);
            this.ModelSelection.TabIndex = 2;
            this.ModelSelection.Text = "Model Asembly";
            // 
            // MatrixInitialise
            // 
            this.MatrixInitialise.AutoSize = true;
            this.MatrixInitialise.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.MatrixInitialise.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MatrixInitialise.Location = new System.Drawing.Point(82, 203);
            this.MatrixInitialise.Name = "MatrixInitialise";
            this.MatrixInitialise.Size = new System.Drawing.Size(172, 49);
            this.MatrixInitialise.TabIndex = 3;
            this.MatrixInitialise.Text = "New Matrix";
            this.MatrixInitialise.UseVisualStyleBackColor = false;
            this.MatrixInitialise.Click += new System.EventHandler(this.MatrixInitialise_Click);
            // 
            // tutorialButton
            // 
            this.tutorialButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tutorialButton.Location = new System.Drawing.Point(77, 27);
            this.tutorialButton.Name = "tutorialButton";
            this.tutorialButton.Size = new System.Drawing.Size(198, 39);
            this.tutorialButton.TabIndex = 4;
            this.tutorialButton.Text = "Tutorial Video";
            this.tutorialButton.UseVisualStyleBackColor = true;
            this.tutorialButton.Click += new System.EventHandler(this.tutorialButton_Click);
            // 
            // ModelSelectionPannel
            // 
            this.ModelSelectionPannel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ModelSelectionPannel.AutoScrollMargin = new System.Drawing.Size(50, 60);
            this.ModelSelectionPannel.BackColor = System.Drawing.SystemColors.Window;
            this.ModelSelectionPannel.Controls.Add(this.SolveConnection);
            this.ModelSelectionPannel.Controls.Add(this.DeleteEntireConnection);
            this.ModelSelectionPannel.Controls.Add(this.DeleteComponent);
            this.ModelSelectionPannel.Controls.Add(this.CancelSelectedCell);
            this.ModelSelectionPannel.Controls.Add(this.saveConnection);
            this.ModelSelectionPannel.Controls.Add(this.label2);
            this.ModelSelectionPannel.Controls.Add(this.zoom_to_default);
            this.ModelSelectionPannel.Controls.Add(this.lockalise_matricies);
            this.ModelSelectionPannel.Controls.Add(this.return_to_origin);
            this.ModelSelectionPannel.Controls.Add(this.label1);
            this.ModelSelectionPannel.Controls.Add(this.solveButton);
            this.ModelSelectionPannel.Controls.Add(this.ConnectionInitialise);
            this.ModelSelectionPannel.Controls.Add(this.ModelSelection);
            this.ModelSelectionPannel.Controls.Add(this.tutorialButton);
            this.ModelSelectionPannel.Controls.Add(this.MatrixInitialise);
            this.ModelSelectionPannel.Location = new System.Drawing.Point(12, 12);
            this.ModelSelectionPannel.Name = "ModelSelectionPannel";
            this.ModelSelectionPannel.Size = new System.Drawing.Size(345, 1037);
            this.ModelSelectionPannel.TabIndex = 5;
            // 
            // SolveConnection
            // 
            this.SolveConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SolveConnection.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.SolveConnection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SolveConnection.Location = new System.Drawing.Point(67, 931);
            this.SolveConnection.Name = "SolveConnection";
            this.SolveConnection.Size = new System.Drawing.Size(208, 55);
            this.SolveConnection.TabIndex = 26;
            this.SolveConnection.Text = "Solve Connection";
            this.SolveConnection.UseVisualStyleBackColor = false;
            this.SolveConnection.Click += new System.EventHandler(this.SolveConnection_Click);
            // 
            // DeleteEntireConnection
            // 
            this.DeleteEntireConnection.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.DeleteEntireConnection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteEntireConnection.Location = new System.Drawing.Point(181, 432);
            this.DeleteEntireConnection.Name = "DeleteEntireConnection";
            this.DeleteEntireConnection.Size = new System.Drawing.Size(141, 64);
            this.DeleteEntireConnection.TabIndex = 22;
            this.DeleteEntireConnection.Text = "Delete Connection";
            this.DeleteEntireConnection.UseVisualStyleBackColor = false;
            this.DeleteEntireConnection.Click += new System.EventHandler(this.DeleteEntireConnection_Click);
            // 
            // DeleteComponent
            // 
            this.DeleteComponent.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.DeleteComponent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DeleteComponent.Location = new System.Drawing.Point(35, 432);
            this.DeleteComponent.Name = "DeleteComponent";
            this.DeleteComponent.Size = new System.Drawing.Size(140, 64);
            this.DeleteComponent.TabIndex = 26;
            this.DeleteComponent.Text = "Delete Component";
            this.DeleteComponent.UseVisualStyleBackColor = false;
            this.DeleteComponent.Click += new System.EventHandler(this.DeleteComponent_Click);
            // 
            // CancelSelectedCell
            // 
            this.CancelSelectedCell.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CancelSelectedCell.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelSelectedCell.Location = new System.Drawing.Point(115, 516);
            this.CancelSelectedCell.Name = "CancelSelectedCell";
            this.CancelSelectedCell.Size = new System.Drawing.Size(128, 45);
            this.CancelSelectedCell.TabIndex = 24;
            this.CancelSelectedCell.Text = "Unselect";
            this.CancelSelectedCell.UseVisualStyleBackColor = false;
            this.CancelSelectedCell.Click += new System.EventHandler(this.CancelSelectedCell_Click);
            // 
            // saveConnection
            // 
            this.saveConnection.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.saveConnection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveConnection.Location = new System.Drawing.Point(181, 367);
            this.saveConnection.Name = "saveConnection";
            this.saveConnection.Size = new System.Drawing.Size(84, 47);
            this.saveConnection.TabIndex = 19;
            this.saveConnection.Text = "Save";
            this.saveConnection.UseVisualStyleBackColor = false;
            this.saveConnection.Click += new System.EventHandler(this.saveConnection_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.875F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(89, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 33);
            this.label2.TabIndex = 18;
            this.label2.Text = "Connection";
            // 
            // zoom_to_default
            // 
            this.zoom_to_default.Location = new System.Drawing.Point(77, 728);
            this.zoom_to_default.Name = "zoom_to_default";
            this.zoom_to_default.Size = new System.Drawing.Size(208, 53);
            this.zoom_to_default.TabIndex = 17;
            this.zoom_to_default.Text = "Default Zoom";
            this.zoom_to_default.UseVisualStyleBackColor = true;
            this.zoom_to_default.Click += new System.EventHandler(this.zoom_to_default_Click);
            // 
            // lockalise_matricies
            // 
            this.lockalise_matricies.Location = new System.Drawing.Point(75, 787);
            this.lockalise_matricies.Name = "lockalise_matricies";
            this.lockalise_matricies.Size = new System.Drawing.Size(208, 53);
            this.lockalise_matricies.TabIndex = 16;
            this.lockalise_matricies.Text = "Localise Matricies";
            this.lockalise_matricies.UseVisualStyleBackColor = true;
            this.lockalise_matricies.Click += new System.EventHandler(this.lockalise_matricies_Click);
            // 
            // return_to_origin
            // 
            this.return_to_origin.Location = new System.Drawing.Point(75, 652);
            this.return_to_origin.Name = "return_to_origin";
            this.return_to_origin.Size = new System.Drawing.Size(208, 54);
            this.return_to_origin.TabIndex = 14;
            this.return_to_origin.Text = "Return to origin";
            this.return_to_origin.UseVisualStyleBackColor = true;
            this.return_to_origin.Click += new System.EventHandler(this.return_to_origin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 598);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 32);
            this.label1.TabIndex = 13;
            this.label1.Text = "Canvas Coordination";
            // 
            // solveButton
            // 
            this.solveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.solveButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.solveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.solveButton.Location = new System.Drawing.Point(77, 870);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(194, 55);
            this.solveButton.TabIndex = 12;
            this.solveButton.Text = "Solve Model";
            this.solveButton.UseVisualStyleBackColor = false;
            this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
            // 
            // ConnectionInitialise
            // 
            this.ConnectionInitialise.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ConnectionInitialise.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConnectionInitialise.Location = new System.Drawing.Point(71, 367);
            this.ConnectionInitialise.Name = "ConnectionInitialise";
            this.ConnectionInitialise.Size = new System.Drawing.Size(84, 47);
            this.ConnectionInitialise.TabIndex = 7;
            this.ConnectionInitialise.Text = "New";
            this.ConnectionInitialise.UseVisualStyleBackColor = false;
            this.ConnectionInitialise.Click += new System.EventHandler(this.ConnectionInitialise_Click);
            // 
            // Canvas
            // 
            this.Canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Canvas.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Canvas.Location = new System.Drawing.Point(363, 12);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(1268, 1106);
            this.Canvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Canvas.TabIndex = 6;
            this.Canvas.TabStop = false;
            this.Canvas.Click += new System.EventHandler(this.Canvas_Click);
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.ExitMatrixSelection);
            this.panel1.Controls.Add(this.ExitConnectionSelection);
            this.panel1.Controls.Add(this.ConnectionInitialiseIndicator);
            this.panel1.Controls.Add(this.ChoosingMatrixBool);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(1183, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(437, 72);
            this.panel1.TabIndex = 7;
            // 
            // ExitMatrixSelection
            // 
            this.ExitMatrixSelection.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ExitMatrixSelection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitMatrixSelection.Location = new System.Drawing.Point(85, 34);
            this.ExitMatrixSelection.Name = "ExitMatrixSelection";
            this.ExitMatrixSelection.Size = new System.Drawing.Size(90, 32);
            this.ExitMatrixSelection.TabIndex = 23;
            this.ExitMatrixSelection.Text = "Exit";
            this.ExitMatrixSelection.UseVisualStyleBackColor = false;
            this.ExitMatrixSelection.Click += new System.EventHandler(this.ExitMatrixSelection_Click);
            // 
            // ExitConnectionSelection
            // 
            this.ExitConnectionSelection.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ExitConnectionSelection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitConnectionSelection.Location = new System.Drawing.Point(289, 34);
            this.ExitConnectionSelection.Name = "ExitConnectionSelection";
            this.ExitConnectionSelection.Size = new System.Drawing.Size(106, 30);
            this.ExitConnectionSelection.TabIndex = 24;
            this.ExitConnectionSelection.Text = "Exit";
            this.ExitConnectionSelection.UseVisualStyleBackColor = false;
            this.ExitConnectionSelection.Click += new System.EventHandler(this.ExitConnectionSelection_Click);
            // 
            // ConnectionInitialiseIndicator
            // 
            this.ConnectionInitialiseIndicator.AutoSize = true;
            this.ConnectionInitialiseIndicator.BackColor = System.Drawing.Color.White;
            this.ConnectionInitialiseIndicator.Font = new System.Drawing.Font("Times New Roman", 7.875F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectionInitialiseIndicator.Location = new System.Drawing.Point(243, 37);
            this.ConnectionInitialiseIndicator.Name = "ConnectionInitialiseIndicator";
            this.ConnectionInitialiseIndicator.Size = new System.Drawing.Size(25, 24);
            this.ConnectionInitialiseIndicator.TabIndex = 12;
            this.ConnectionInitialiseIndicator.Text = "   ";
            this.ConnectionInitialiseIndicator.Click += new System.EventHandler(this.ConnectionInitialiseIndicator_Click);
            // 
            // ChoosingMatrixBool
            // 
            this.ChoosingMatrixBool.AutoSize = true;
            this.ChoosingMatrixBool.BackColor = System.Drawing.Color.White;
            this.ChoosingMatrixBool.Font = new System.Drawing.Font("Times New Roman", 7.875F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChoosingMatrixBool.Location = new System.Drawing.Point(44, 38);
            this.ChoosingMatrixBool.Name = "ChoosingMatrixBool";
            this.ChoosingMatrixBool.Size = new System.Drawing.Size(25, 24);
            this.ChoosingMatrixBool.TabIndex = 11;
            this.ChoosingMatrixBool.Text = "   ";
            this.ChoosingMatrixBool.Click += new System.EventHandler(this.ChoosingMatrixBool_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(181, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 49);
            this.label5.TabIndex = 10;
            this.label5.Text = "|";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 7.875F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(229, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "Connection Selection";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 7.875F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(44, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "Choose Matrix";
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1643, 1061);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Canvas);
            this.Controls.Add(this.ModelSelectionPannel);
            this.Font = new System.Drawing.Font("Times New Roman", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(700, 600);
            this.Name = "mainWindow";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Theory Calculator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ModelSelectionPannel.ResumeLayout(false);
            this.ModelSelectionPannel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label ModelSelection;
        private System.Windows.Forms.Button MatrixInitialise;
        private System.Windows.Forms.Button tutorialButton;
        private System.Windows.Forms.Panel ModelSelectionPannel;
        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.Button ConnectionInitialise;
        private System.Windows.Forms.Button solveButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button return_to_origin;
        private System.Windows.Forms.Button lockalise_matricies;
        private System.Windows.Forms.Button zoom_to_default;
        private System.Windows.Forms.Button saveConnection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ChoosingMatrixBool;
        private System.Windows.Forms.Label ConnectionInitialiseIndicator;
        private System.Windows.Forms.Button ExitMatrixSelection;
        private System.Windows.Forms.Button ExitConnectionSelection;
        private System.Windows.Forms.Button CancelSelectedCell;
        private System.Windows.Forms.Button DeleteComponent;
        private System.Windows.Forms.Button DeleteEntireConnection;
        private System.Windows.Forms.Button SolveConnection;
    }
}

