using Stochastic_Game_Theory_Calculator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Game_Theory_Calculator.Models
{
    public class Matrix : Model
    {
        //define all variables
        private int rows;
        private int cols;
        private string[,] payoffs;
        private string[] rowStrategies;
        private string[] colStrategies;
        private Stack<Matrix> versionsStack;
        private RectangleF hitbox;
        private float cellHeight = 60;
        private float cellWidth = 100;
        bool moving;
        private const int cellBuffer = 5; //constant

        private float gridWidth;
        private float gridHight;
        private float rowYcord;
        private float colXcrd;
        private float colYcrd;
        private float rowXcrd;
        private int connectionOutcomeRowIndeex;
        private int connectionOutcomeColIndeex;

        private RectangleF currentGrid;
        private RectangleF rowStrategyRectangle;
        private RectangleF cellRectangle;
        private RectangleF colStrategyRectangle;

        private bool[,] Player1BestResponses;
        private bool[,] Player2BestResponses;
        private float[,] Player1Payoffs;
        private float[,] Player2Payoffs;
        private List<string> NashEqualibria = new List<string>();


        public Matrix(int Rows, int Cols, string[,] Payoffs, string[] RowStrategies, string[] ColStrategies, string Name, Stack<Matrix> VersionsStack, string[] Players, float X, float Y, RectangleF Hitbox)
        {
            rows = Rows;
            cols = Cols;
            payoffs = Payoffs;
            rowStrategies = RowStrategies;
            colStrategies = ColStrategies;
            name = Name;
            versionsStack = VersionsStack;
            players = Players;
            masterY = Y;
            masterX = X;
            hitbox = Hitbox;
        }

        public Matrix()
        {
            //default constructor that allows the program to go to the default matrix
        }
        public int GetRows()
        {
            return rows;
        }

        public void SetRows(int Rows)
        {
            rows = Rows;
        }

        public void ChangeRows(int Rows)
        {
            rows += Rows;
        }

        public int GetCols()
        {
            return cols;
        }

        public void SetCols(int Cols)
        {
            cols = Cols;
        }

        public void ChangeCols(int Cols)
        {
            cols += Cols;
        }

        public string[,] GetPayoffs()
        {
            return payoffs;
        }

        public string GetOnePayoff(int row, int col)
        {
            return payoffs[row, col];
        }

        public void SetPayoffs(string[,] newPayoff)
        {
            payoffs = newPayoff;
        }

        public void SetOnePayoff(int Row, int Col, string newPayoff)
        {
            payoffs[Row, Col] = newPayoff;
        }

        public string[] GetRowStrategies()
        {
            return rowStrategies;
        }

        public string GetOneRowStrategy(int index)
        {
            return rowStrategies[index];
        }

        public void SetRowStrategies(string[] strategy)
        {
            rowStrategies = strategy;
        }

        public void SetOneRowStrategy(int index, string strategy)
        {
            rowStrategies[index] = strategy;
        }

        public string[] GetColStrategies()
        {
            return colStrategies;
        }

        public string GetOneColStrategy(int index)
        {
            return colStrategies[index];
        }
        public void SetColStrategies(string[] strategy)
        {
            colStrategies = strategy;
        }

        public void SetOneColStrategy(int index, string strategy)
        {
            colStrategies[index] = strategy;
        }
        public Stack<Matrix> GetVersionStack()
        {
            return versionsStack;
        }

        public void SetVersionStack(Stack<Matrix> Stack)
        {
            versionsStack = Stack;
        }

        public void PushVersionStack(Matrix pushedMatrix)
        {
            versionsStack.Push(pushedMatrix);
        }

        public Matrix PopVersionStack()
        {
            return versionsStack.Pop();
        }

        public float GetX()
        {
            return masterX;
        }

        public void SetX(float X)
        {
            masterX = X;
        }

        public void ChangeX(float X)
        {
            masterX += X;
        }

        public float GetY()
        {
            return masterY;
        }

        public void SetY(float Y)
        {
            masterY = Y;
        }

        public void ChangeY(float Y)
        {
            masterY += Y;
        }
        public RectangleF GetHitbox()
        {
            return hitbox;
        }

        public void SetHitbox(RectangleF Hitbox)
        {
            hitbox = Hitbox;
        }

        public bool IsMoving()
        {
            return moving;
        }

        public void SetIsMoving(bool Moving)
        {
            moving = Moving;
        }

        public float GetCellHeight()
        {
            return cellHeight;
        }

        public void SetCellHeight(float cellHeight)
        {
            this.cellHeight = cellHeight;
        }

        public float GetCellWidth()
        {
            return cellWidth;
        }

        public void SetCellWidth(float cellWidth)
        {
            this.cellWidth = cellWidth;
        }
        public float GetCellBuffer()
        {
            return cellBuffer;
        }

        public float GetGridWidth()
        {
            return gridWidth;
        }

        public void SetGridWidth(float GridWidth)
        {
            gridWidth = GridWidth;
        }

        public float GetGridHight()
        {
            return gridHight;
        }

        public void SetGridHight(float GridHight)
        {
            gridHight = GridHight;
        }
        public float GetRowYCord()
        {
            return rowYcord;
        }
        public void SetRowYCord(float rowYcord)
        {
            this.rowYcord = rowYcord;
        }

        public float GetColXCord()
        {
            return colXcrd;
        }

        public void SetColXCord(float ColXCord)
        {
            colXcrd = ColXCord;
        }
        public float DetermineCellWidth(Graphics g, Models.Matrix matrix, Font text_font, Font payoff_font)
        {
            float contentWidth = LongestCol(matrix, g, text_font, payoff_font);
            float cellWidth = Math.Max(100, contentWidth + (cellBuffer * 2));
            return (cellWidth);
        }

        public float GetColYCord()
        {
            return rowYcord;
        }
        public void SetColYCord(float colYcrd)
        {
            this.colYcrd = colYcrd;
        }

        public float GetRowXCord()
        {
            return rowXcrd;
        }
        public void SetRowXCord(float rowXcord)
        {
            rowXcrd = rowXcord;
        }

        public void SetCurrentGrid(RectangleF rect)
        {
            currentGrid = rect;
        }

        public RectangleF GetCurrentGrid()
        {
            return currentGrid;
        }

        public void SetRowStrategyRectangle(RectangleF rect)
        {
            rowStrategyRectangle = rect;
        }

        public RectangleF GetRowStrategyRectangle()
        {
            return rowStrategyRectangle;
        }
        public void SetColStrategyRectangle(RectangleF rect)
        {
            colStrategyRectangle = rect;
        }

        public RectangleF GetColStrategyRectangle()
        {
            return colStrategyRectangle;
        }

        public void SetCellRectangle(RectangleF rect)
        {
            cellRectangle = rect;
        }

        public RectangleF GetCellRectangle()
        {
            return cellRectangle;
        }

        public bool[,] GetPlayer1BestResponses()
        {
            return Player1BestResponses;
        }

        public bool GetOnePlayer1BestResponse(int row, int col)
        {
            return Player1BestResponses[row, col];
        }

        public void SetPlayer1BestResponses(bool[,] Player1BestResponses)
        {
            this.Player1BestResponses = Player1BestResponses;
        }

        public void SetOnePlayer1BestResponse(int row, int col, bool value)
        {
            Player1BestResponses[row, col] = value;
        }

        public bool[,] GetPlayer2BestResponses()
        {
            return Player2BestResponses;
        }

        public bool GetOnePlayer2BestResponse(int row, int col)
        {
            return Player2BestResponses[row, col];
        }

        public void SetPlayer2BestResponses(bool[,] Player2BestResponses)
        {
            this.Player2BestResponses = Player2BestResponses;
        }
        public void SetOnePlayer2BestResponse(int row, int col, bool value)
        {
            Player2BestResponses[row, col] = value;
        }

        public float[,] GetPlayer1Payoffs()
        {
            return Player1Payoffs;
        }

        public float GetOnePlayer1Payoff(int row, int col)
        {
            return Player1Payoffs[row, col];
        }

        public float GetOnePlayer2Payoff(int row, int col)
        {
            return Player2Payoffs[row, col];
        }

        public void SetPlayer1Payoffs(float[,] Player1Payoffs)
        {
            this.Player1Payoffs = Player1Payoffs;
        }

        public float[,] GetPlayer2Payoffs()
        {
            return Player2Payoffs;
        }

        public void SetPlayer2Payoffs(float[,] Player2Payoffs)
        {
            this.Player2Payoffs = Player2Payoffs;
        }

        public List<string> GetNashEqualibria()
        {
            return NashEqualibria;
        }

        public void SetNashEqualibria(List<string> nashequalibria)
        {
            NashEqualibria = nashequalibria;
        }

        public void AddToNashEqualibria(string output)
        {
            NashEqualibria.Add(output);
        }

        public int GetconnectionRowIndeex()
        {
            return connectionOutcomeRowIndeex;
        }

        public void SetConnectionRowIndeex(int index)
        {
            connectionOutcomeRowIndeex = index;
        }
        public int GetconnectionColIndeex()
        {
            return connectionOutcomeColIndeex;
        }

        public void SetConnectionColIndeex(int index)
        {
            connectionOutcomeColIndeex = index;
        }

        public float LongestCol(Models.Matrix matrix, Graphics g, Font text_font, Font payoff_font)
        {
            float maxWidth = 0;
            foreach (string s in matrix.GetColStrategies())
            {
                SizeF size = g.MeasureString(s, text_font);
                if (size.Width > maxWidth)
                {
                    maxWidth = size.Width;
                }
            }

            for (int r = 0; r < matrix.GetRows(); r++)
            {
                for (int c = 0; c < matrix.GetCols(); c++)
                {
                    string text = matrix.GetOnePayoff(r, c);

                    SizeF size = g.MeasureString(text, payoff_font);
                    if (size.Width > maxWidth)
                    {
                        maxWidth = size.Width;
                    }
                }
            }

            return maxWidth;

        }
        public void ConvertPayoffsToFloat(Matrix matrix, float[,] Player1Payoffs, float[,] Player2Payoffs, List<string> NashEqualibria)
        {
            for (int row = 0; row < matrix.GetRows(); row++)
            {
                for (int column = 0; column < matrix.GetCols(); column++)
                {
                    string stringPayoff = matrix.GetOnePayoff(row, column);
                    string[] parts = stringPayoff.Split(':');

                    Player1Payoffs[row, column] = float.Parse(parts[0]);
                    Player2Payoffs[row, column] = float.Parse(parts[1]);

                    NashEqualibria = new List<string>(); //tenporary output
                }
            }
        }
        public int[] IdentifyCellClicked(PointF mousePointer)
        {
            int[] indecies = new int[2];
            float horisontalCellBound = masterX + cellWidth;
            float verticalCellBound = masterY + cellHeight;

            int colIndex = (int)((mousePointer.X - horisontalCellBound) / cellWidth);
            int rowIndex = (int)((mousePointer.Y - verticalCellBound) / cellHeight);

            if (rowIndex >= 0 && rowIndex < rows && colIndex >= 0 && colIndex < cols)
            {
                indecies[0] = rowIndex;
                indecies[1] = colIndex;
                return (indecies);
            }

            indecies[0] = -1;
            indecies[1] = -1;

            return indecies;
        }
        public void CalculateMatrixBounds(Models.Matrix matrix, Graphics g, Font text_font, Font payoff_font)
        {
            float currentCellHeight = matrix.GetCellHeight();
            float currentCellWidth = matrix.DetermineCellWidth(g, matrix, text_font, payoff_font);
            float totalWidth = (matrix.GetCols() * currentCellWidth) + currentCellWidth;
            float totalHeight = (matrix.GetRows() * currentCellHeight) + currentCellHeight;
            hitbox = new RectangleF(matrix.GetX(), matrix.GetY(), totalWidth + 30f, totalHeight + 30f);
        }


        public Matrix defaultMatrix(List<Models.Matrix> savedMatricies, int count)
        {
            string[,] defaultPayoffs = { { "3:3", "0:4" }, { "4:0", "2:2" } };
            string[] defaultRowStrategies = { "Cooperate", "Defect" };
            string[] defaultColStrategies = { "Cooperate", "Defect" };
            string[] defaultPlayers = { "Player 1", "Player 2" };

            foreach (Models.Matrix matrix in savedMatricies)
            {
                if (savedMatricies.Count == 0)
                {
                    break;
                }
                else if (matrix.GetName() == "Default Name")
                {
                    defaultName = "Default Name (" + count + ")";
                    break;
                }
                else
                {
                    defaultName = "Default Name";
                    break;
                }

            }
            Stack<Matrix> defaultStack = new Stack<Matrix>();
            RectangleF defaultRectangle = new RectangleF();
            Matrix defaultMatrix = new Matrix(2, 2, defaultPayoffs, defaultRowStrategies, defaultColStrategies, defaultName, defaultStack, defaultPlayers, 150, 80, defaultRectangle);
            return defaultMatrix;
        }

        public void GiveName()
        {

        }
    }
}