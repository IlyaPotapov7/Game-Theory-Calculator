using Game_Theory_Calculator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Game_Theory_Calculator
{
    public partial class MatrixModification : Form
    {
        public Models.Matrix currentMatrix = new Models.Matrix();
        public bool isSaved = false;
        public bool deleted = false;
        private string[] splitPayoff;

        public void recieveMatrix(Models.Matrix matrix)
        {
            currentMatrix = copyMatrix(matrix);
        }

        public MatrixModification()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void AccesabilityLimit()
        {
            for (int r = 1; r < MatrixBlueprint.RowCount; r++)
            {
                if (r == 2)
                {
                    continue;
                }
                MatrixBlueprint[0, r].ReadOnly = true;
            }
            for (int c = 1; c < MatrixBlueprint.ColumnCount; c++)
            {
                if (c == 2)
                {
                    continue;
                }
                MatrixBlueprint[c, 0].ReadOnly = true;
            }

        }

        public bool VerifyPayofsFloat()
        {
            splitPayoff = null;

            for (int r = 0; r < currentMatrix.GetRows(); r++)
            {
                for (int x = 0; x < currentMatrix.GetCols(); x++)
                {
                    object cellValue = MatrixBlueprint[x + 2, r + 2].Value;

                    if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                    {
                        MessageBox.Show($"Cell {x + 3},{r + 3} is empty. Please fill in the payoff");
                        return false;
                    }
                    splitPayoff = cellValue.ToString().Split(':');
                    if (splitPayoff.Length != 2 || !float.TryParse(splitPayoff[0], out _) || !float.TryParse(splitPayoff[1], out _))
                    {
                        MessageBox.Show($"The value '{(MatrixBlueprint[x + 2, r + 2]).Value}' is invalid, the payoff must be in the form 'number:number'");
                        return false;
                    }
                }
            }
            return true;
        }

        private void MatrixModification_Load(object sender, EventArgs e)
        {
            MatrixBlueprint.RowCount = currentMatrix.GetRows() + 2;
            MatrixBlueprint.ColumnCount = currentMatrix.GetCols() + 2;
            MatrixBlueprint.ColumnHeadersVisible = false;
            MatrixBlueprint.RowHeadersVisible = false;
            MatrixBlueprint[0, 0].ReadOnly = false;
            MatrixBlueprint[1, 1].ReadOnly = true;
            MatrixBlueprint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            MatrixBlueprint.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            MatrixBlueprint.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DisplayMatrix(currentMatrix);
        }

        private void SaveChanges_Click(object sender, EventArgs e)
        {
            if (VerifyPayofsFloat())
            {
                currentMatrix.PushVersionStack(copyMatrix(currentMatrix));
                SaveBeforeEdit();
                MessageBox.Show("Model Saved");
                isSaved = true;
            }
        }

        private void SaveBeforeEdit()
        {
            for (int r = 0; r < currentMatrix.GetRows(); r++)
            {
                for (int c = 0; c < currentMatrix.GetCols(); c++)
                {
                    currentMatrix.SetOnePayoff(r, c, MatrixBlueprint[c + 2, r + 2].Value.ToString());
                }
            }
            for (int c = 0; c < currentMatrix.GetCols(); c++)
            {
                currentMatrix.SetOneColStrategy(c, MatrixBlueprint[c + 2, 1].Value.ToString());
            }

            for (int r = 0; r < currentMatrix.GetRows(); r++)
            {
                currentMatrix.SetOneRowStrategy(r, MatrixBlueprint[1, r + 2].Value.ToString());
            }

            currentMatrix.SetOnePlayer(0, MatrixBlueprint[0, 2].Value.ToString());
            currentMatrix.SetOnePlayer(1, MatrixBlueprint[2, 0].Value.ToString());
            currentMatrix.SetName(MatrixBlueprint[0, 0].Value.ToString());
        }

        public void DisplayMatrix(Models.Matrix matrix)
        {
            currentMatrix = matrix;

            UpdateMatrixBlueprintProperties();

            DrawGridMatrixBluuprint();


            FillCellsBlueprint();

            DrawStrategies();

            AccesabilityLimit();
        }

        private void UpdateMatrixBlueprintProperties()
        {
            MatrixBlueprint.RowCount = currentMatrix.GetRows() + 2;
            MatrixBlueprint.ColumnCount = currentMatrix.GetCols() + 2;

            MatrixBlueprint.ColumnHeadersVisible = false;
            MatrixBlueprint.RowHeadersVisible = false;

            MatrixBlueprint.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void DrawGridMatrixBluuprint()
        {
            for (int r = 0; r < currentMatrix.GetRows(); r++)
            {
                if (r > 0)
                {
                    MatrixBlueprint[0, r + 2].Value = "";
                    MatrixBlueprint[0, r + 2].Style.BackColor = Color.LightBlue;
                }
                for (int c = 0; c < currentMatrix.GetCols(); c++)
                {
                    MatrixBlueprint[c + 2, r + 2].Value = currentMatrix.GetOnePayoff(r, c);
                    if (c > 0)
                    {
                        MatrixBlueprint[c + 2, 0].Style.BackColor = Color.LightBlue;
                    }
                }
            }
        }

        private void FillCellsBlueprint()
        {
            MatrixBlueprint.DefaultCellStyle.Font = new Font("Times New Roman", 14);
            MatrixBlueprint[1, 0].Style.BackColor = Color.LightBlue;
            MatrixBlueprint[0, 1].Style.BackColor = Color.LightBlue;
            MatrixBlueprint[1, 1].Style.BackColor = Color.LightBlue;
            MatrixBlueprint.RowTemplate.Height = 40;
            MatrixBlueprint[0, 2].Value = currentMatrix.GetOnePlayer(0);
            MatrixBlueprint[2, 0].Value = currentMatrix.GetOnePlayer(1);
            MatrixBlueprint[0, 0].Value = currentMatrix.GetName();
        }

        private void DrawStrategies()
        {
            for (int c = 0; c < currentMatrix.GetCols(); c++)
            {
                MatrixBlueprint[c + 2, 1].Value = currentMatrix.GetOneColStrategy(c);
            }

            for (int r = 0; r < currentMatrix.GetRows(); r++)
            {
                MatrixBlueprint[1, r + 2].Value = currentMatrix.GetOneRowStrategy(r);
            }
        }
        private void AddRow_Click(object sender, EventArgs e)
        {
            SaveBeforeEdit();

            currentMatrix.ChangeRows(1);

            string[,] temporaryPayoffs = new string[currentMatrix.GetRows(), currentMatrix.GetCols()];
            string[] temporaryRowStrategies = new string[currentMatrix.GetRows()];

            for (int r = 0; r < currentMatrix.GetRows() - 1; r++)
            {
                temporaryRowStrategies[r] = currentMatrix.GetOneRowStrategy(r);
            }

            for (int r = 0; r < currentMatrix.GetRows() - 1; r++)
            {
                for (int c = 0; c < currentMatrix.GetCols(); c++)
                {
                    temporaryPayoffs[r, c] = currentMatrix.GetOnePayoff(r, c);
                }
            }

            temporaryRowStrategies[currentMatrix.GetRows() - 1] = "New Strategy";

            for (int c = 0; c < currentMatrix.GetCols(); c++)
            {
                temporaryPayoffs[currentMatrix.GetRows() - 1, c] = "empty payoff";
            }

            currentMatrix.SetRowStrategies(temporaryRowStrategies);
            currentMatrix.SetPayoffs(temporaryPayoffs);
            DisplayMatrix(currentMatrix);
        }
        private void DeleteRow_Click(object sender, EventArgs e)
        {
            if (currentMatrix.GetRows() <= 1)
            {
                MessageBox.Show("Cannot have fewer than 1 strategy.");
                return;
            }
            SaveBeforeEdit();
            currentMatrix.ChangeRows(-1);
            string[,] temporaryPayoffs = new string[currentMatrix.GetRows(), currentMatrix.GetCols()];
            string[] temporaryRowStrategies = new string[currentMatrix.GetRows()];
            MatrixBlueprint[0, 2].Value = currentMatrix.GetOnePlayer(0);
            for (int r = 0; r < currentMatrix.GetRows(); r++)
            {
                temporaryRowStrategies[r] = currentMatrix.GetOneRowStrategy(r);
            }
            for (int r = 0; r < currentMatrix.GetRows(); r++)
            {
                for (int c = 0; c < currentMatrix.GetCols(); c++)
                {
                    temporaryPayoffs[r, c] = currentMatrix.GetOnePayoff(r, c);
                }
            }
            currentMatrix.SetRowStrategies(temporaryRowStrategies);
            currentMatrix.SetPayoffs(temporaryPayoffs);
            DisplayMatrix(currentMatrix);
        }

        private void AddColumn_Click(object sender, EventArgs e)
        {
            SaveBeforeEdit();
            currentMatrix.ChangeCols(1);
            string[,] temporaryPayoffs = new string[currentMatrix.GetRows(), currentMatrix.GetCols()];
            string[] temporaryColStrategies = new string[currentMatrix.GetCols()];
            for (int c = 0; c < currentMatrix.GetCols() - 1; c++)
            {
                temporaryColStrategies[c] = currentMatrix.GetOneColStrategy(c);
            }
            for (int r = 0; r < currentMatrix.GetRows(); r++)
            {
                for (int c = 0; c < currentMatrix.GetCols() - 1; c++)
                {
                    temporaryPayoffs[r, c] = currentMatrix.GetOnePayoff(r, c);
                }
            }
            temporaryColStrategies[currentMatrix.GetCols() - 1] = "New Strategy";
            for (int r = 0; r < currentMatrix.GetRows(); r++)
            {
                temporaryPayoffs[r, currentMatrix.GetCols() - 1] = "empty payoff";
            }
            currentMatrix.SetColStrategies(temporaryColStrategies);
            currentMatrix.SetPayoffs(temporaryPayoffs);
            DisplayMatrix(currentMatrix);
        }

        private void DeleteColumn_Click(object sender, EventArgs e)
        {
            if (currentMatrix.GetCols() <= 1)
            {
                MessageBox.Show("Cannot have fewer than 1 strategy.");
                return;
            }

            SaveBeforeEdit();
            currentMatrix.ChangeCols(-1);
            string[,] temporaryPayoffs = new string[currentMatrix.GetRows(), currentMatrix.GetCols()];
            string[] temporaryColStrategies = new string[currentMatrix.GetCols()];
            for (int c = 0; c < currentMatrix.GetCols(); c++)
            {
                temporaryColStrategies[c] = currentMatrix.GetOneColStrategy(c);
            }
            for (int r = 0; r < currentMatrix.GetRows(); r++)
            {
                for (int c = 0; c < currentMatrix.GetCols(); c++)
                {
                    temporaryPayoffs[r, c] = currentMatrix.GetOnePayoff(r, c);
                }
            }
            currentMatrix.SetColStrategies(temporaryColStrategies);
            currentMatrix.SetPayoffs(temporaryPayoffs);
            DisplayMatrix(currentMatrix);
        }

        private Models.Matrix copyMatrix(Models.Matrix originalMatrix)
        {
            Models.Matrix updatedMatrix = new Models.Matrix();

            updatedMatrix.SetCols(originalMatrix.GetCols());
            updatedMatrix.SetRows(originalMatrix.GetRows());

            updatedMatrix.SetX(originalMatrix.GetX());
            updatedMatrix.SetY(originalMatrix.GetY());

            updatedMatrix.SetPayoffs(originalMatrix.GetPayoffs());

            updatedMatrix.SetPlayers(originalMatrix.GetPlayers());

            updatedMatrix.SetRowStrategies(originalMatrix.GetRowStrategies());

            updatedMatrix.SetColStrategies(originalMatrix.GetColStrategies());

            updatedMatrix.SetName(originalMatrix.GetName());

            updatedMatrix.SetVersionStack(originalMatrix.GetVersionStack());

            return updatedMatrix;
        }

        private void DeleteMatrixButton_Click(object sender, EventArgs e)
        {
            DialogResult check_beofre_delete = MessageBox.Show("Are you sure you want to delete currently selected matrix?", "Deleting Matrix", MessageBoxButtons.YesNo);
            if (check_beofre_delete == DialogResult.Yes)
            {
                deleted = true;
                MessageBox.Show("Matrix Deleted");
                Close();
            }
        }

        private void saved_back_Click(object sender, EventArgs e)
        {
            if (currentMatrix.GetVersionStack().Count > 1)
            {
                currentMatrix = copyMatrix(currentMatrix.PopVersionStack());
            }
            else
            {
                MessageBox.Show("First saved version displayed");
            }
            DisplayMatrix(currentMatrix);
        }

    }
}
