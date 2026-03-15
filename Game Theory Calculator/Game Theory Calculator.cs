using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Game_Theory_Calculator
{
    public partial class mainWindow : Form
    {
        public mainWindow()
        {
            InitializeComponent();
            Canvas.MouseWheel += Canvas_MouseWheel;
        }

        // Global Variables

        // Placeholders and Saved Models

        private Matrix currentMatrix;
        private List<Matrix> savedMaticies;
        private Connection currentConnection;
        private List<Connection> existingConnections;

        private Matrix movingMatrix;
        private Matrix destinationMatrix;
        private Node originNode;
        private Node currentNode;

        // Integers for ID assignement

        private int newModelID = 0;
        private int newConnectionID = 0;

        private AdjustableArrowCap connectionArrow = new AdjustableArrowCap(5, 5);

        private float zoomDelta = 0.9f;

        // Booleans that determine the mode of the program

        private bool connectionSelection;
        private bool panning = false;
        private bool matrixSelection = false;
        private bool EditingMatrix;
        private bool isDragged = false;
        private bool chossingPayoffToDeleteConnection = false;
        private bool chossingMatrixToDeleteConnection = false;
        private bool choosingMatrixToDeleteEntireConnection = false;
        private bool solvingConnection = false;
        private bool selectingMatrixToSolveConnection = false;

        // Classes

        // General Model class
        public class Model
        {
            protected float masterX;
            protected float masterY;
            protected string[] players;
            protected string name;
            protected int ID;
            protected string defaultName = "Default Name";

            public string[] GetPlayers()
            {
                return players;
            }

            public string GetOnePlayer(int index)
            {
                return players[index];
            }

            public void SetPlayers(string[] Players)
            {
                players = Players;
            }

            public void SetOnePlayer(int index, string Player)
            {
                players[index] = Player;
            }
            public string GetName()
            {
                return name;
            }

            public void SetName(string Name)
            {
                name = Name;
            }

            public int GetID()
            {
                return ID;
            }

            public void SetID(int ID)
            {
                this.ID = ID;
            }
        }

        // Matrix class 
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
            private bool moving;
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
            public float DetermineCellWidth(Graphics g, Matrix matrix, Font text_font, Font payoff_font)
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

            public float LongestCol(Matrix matrix, Graphics g, Font text_font, Font payoff_font)
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
            public void CalculateMatrixBounds(Matrix matrix, Graphics g, Font text_font, Font payoff_font)
            {
                float currentCellHeight = matrix.GetCellHeight();
                float currentCellWidth = matrix.DetermineCellWidth(g, matrix, text_font, payoff_font);
                float totalWidth = (matrix.GetCols() * currentCellWidth) + currentCellWidth;
                float totalHeight = (matrix.GetRows() * currentCellHeight) + currentCellHeight;
                hitbox = new RectangleF(matrix.GetX(), matrix.GetY(), totalWidth + 30f, totalHeight + 30f);
            }


            public Matrix defaultMatrix(List<Matrix> savedMatricies, int count)
            {
                string[,] defaultPayoffs = { { "3:3", "0:4" }, { "4:0", "2:2" } };
                string[] defaultRowStrategies = { "Cooperate", "Defect" };
                string[] defaultColStrategies = { "Cooperate", "Defect" };
                string[] defaultPlayers = { "Player 1", "Player 2" };

                foreach (Matrix matrix in savedMatricies)
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
        }

        //Connection class that is composed of nodes
        public class Connection
        {
            private List<LinkedList<Node>> connectedComponents;
            private int connectionID;
            private Model rootModel;

            public Connection(int newID)
            {
                connectedComponents = new List<LinkedList<Node>>();
                connectionID = newID;
                rootModel = null;
            }

            public int GetConnectionID()
            {
                return connectionID;
            }

            public List<LinkedList<Node>> GetConnectedComponents()
            {
                return connectedComponents;
            }

            public void AddConection(Model originModel, Model destinationModel, int originRow, int originCol)
            {
                LinkedList<Node> link = GetLinkOfCell(originModel, originRow, originCol);

                if (link == null)
                {
                    link = new LinkedList<Node>();
                    link.AddFirst(new Node(originModel, originRow, originCol));
                    link.AddLast(new Node(destinationModel));
                    connectedComponents.Add(link);
                }
                else
                {
                    if (!CheckChainForDestination(link, destinationModel))
                    {
                        link.AddLast(new Node(destinationModel));
                    }
                }
            }

            public LinkedList<Node> GetLinkOfCell(Model originModel, int originRow, int originCol)
            {
                foreach (var nodesList in connectedComponents)
                {
                    Node listHead = nodesList.First.Value;

                    if (listHead.GetModelReference() == originModel)
                    {
                        if (listHead.GetRowIndex() == originRow && listHead.GetColIndex() == originCol)
                        {
                            return nodesList;
                        }
                    }
                }
                return null;
            }

            private bool CheckChainForDestination(LinkedList<Node> link, Model target)
            {
                foreach (var node in link)
                {
                    if (node != link.First.Value && node.GetModelReference() == target)
                    {
                        return true;
                    }
                }

                return false;
            }
            public void RemoveConnection(Model origin, int row, int col, Model destination)
            {
                LinkedList<Node> link = GetLinkOfCell(origin, row, col);

                if (link != null)
                {
                    Node nodeToRemove = null;
                    foreach (Node node in link)
                    {

                        if (node == link.First.Value)
                        {
                            continue;
                        }

                        if (node.GetModelReference() == destination)
                        {
                            nodeToRemove = node;
                            break;
                        }
                    }

                    if (nodeToRemove != null)
                    {
                        link.Remove(nodeToRemove);
                    }

                    if (link.Count == 1)
                    {
                        connectedComponents.Remove(link);
                    }
                }
            }

            public void RefreshRefference(Model previousVersion, Model newVersion)
            {
                foreach (LinkedList<Node> link in connectedComponents)
                {
                    foreach (Node node in link)
                    {
                        if (node.GetModelReference() == previousVersion)
                        {
                            node.SetModelReference(newVersion);
                        }
                    }
                }
            }

            public Model GetRootModel()
            {
                return rootModel;
            }

            public void SetRootModel(Model rootModel)
            {
                this.rootModel = rootModel;
            }
        }

        //Node class that can be either a cell of a matrix or a matrix itself
        public class Node : Model
        {
            private Model ModelReference;
            private int RowIndex;
            private int ColIndex;

            public Model GetModelReference()
            {
                return ModelReference;
            }

            public void SetModelReference(Model modelRef)
            {
                ModelReference = modelRef;
            }

            public int GetRowIndex()
            {
                return RowIndex;
            }

            public void SetRowIndex(int rowIndex)
            {
                RowIndex = rowIndex;
            }

            public int GetColIndex()
            {
                return ColIndex;
            }

            public void SetColIndex(int colIndex)
            {
                ColIndex = colIndex;
            }
            public Node(Model model, int row, int col)
            {
                ModelReference = model;
                RowIndex = row;
                ColIndex = col;
            }

            public Node(Model model)
            {
                ModelReference = model;
            }
        }

        //Points placeholders used for algorithms
        static class Points
        {
            public static PointF connectionStart;
            public static PointF connectionEnd;
            public static PointF zoomFocus = new PointF(0, 0);
            public static PointF originPoint = new PointF(0, 0);
            public static PointF selectPoint = new PointF(0, 0);
            public static PointF startingPosition = new PointF(0, 0);
            public static PointF previousPoint;
            public static PointF worldMouseCoord;
        }

        //Class of fonts that will be used in the program
        static class Fonts
        {
            public static readonly Font text_font = new Font("Times New Roman", 11, FontStyle.Regular);
            public static readonly Font payoff_font = new Font("Times New Roman", 12, FontStyle.Regular);
            public static readonly Font player_font = new Font("Times New Roman", 12, FontStyle.Bold);
            public static readonly Font name_font = new Font("Times New Roman", 14, FontStyle.Italic);
            public static readonly Font origin_font = new Font("Times New Roman", 25, FontStyle.Bold);
        }



        // Setup subroutines

        private void Form1_Load(object sender, EventArgs e)
        {
            savedMaticies = new List<Matrix>();
            existingConnections = new List<Connection>();
        }

        // This subroutine will handle the initialisation of a new matrix
        private void MatrixInitialise_Click(object sender, EventArgs e)
        {
            if (!stopBRESelection())
            {
                currentMatrix = new Matrix();
                currentMatrix = currentMatrix.defaultMatrix(savedMaticies, newModelID);
                currentMatrix.SetID(newModelID);
                newModelID++;
                editMatrix(currentMatrix);
            }
        }

        // This subroutine calls the matrix modifiaction window and passes it the matrix and then recieves it and saves it
        public void editMatrix(Matrix matrix)
        {
            currentMatrix = matrix;
            MatrixModification MM = OpenMatrixEditWindow();
            SaveMatrixModification(MM);
            Canvas.Invalidate();
        }

        // This subroutine opens the matrix modification window
        private MatrixModification OpenMatrixEditWindow()
        {
            EditingMatrix = true;
            MatrixModification MM = new MatrixModification();
            MM.recieveMatrix(currentMatrix);
            MM.ShowDialog();
            return MM;
        }

        // This subroutine saves the changes of the matrix and updates the connected structures based on the changes
        private void SaveMatrixModification(MatrixModification MM)
        {
            while (EditingMatrix)
            {
                EditingMatrix = false;
                if (MM.deleted)
                {
                    savedMaticies.Remove(currentMatrix);

                    for (int i = existingConnections.Count - 1; i >= 0; i--)
                    {
                        if (FindConnectionContainingMatrix(existingConnections[i], currentMatrix))
                        {
                            existingConnections.RemoveAt(i);
                        }
                    }
                }
                else if (MM.isSaved)
                {
                    if (MM.VerifyPayofsFloat())
                    {
                        savedMaticies.Remove(currentMatrix);

                        foreach (Connection conn in existingConnections)
                        {
                            conn.RefreshRefference(currentMatrix, MM.currentMatrix);
                        }
                        currentMatrix = MM.currentMatrix;
                        localise_matrix(currentMatrix);
                        savedMaticies.Add(MM.currentMatrix);
                    }
                    else
                    {
                        MM.ShowDialog();
                        EditingMatrix = true;
                    }
                }
            }
        }



        // Matrix Location Subroutines

        // This subroutine will ensure that the matricies do not collide
        public void localise_matrix(Matrix matrix)
        {
            bool positionVerified = false;

            using (Graphics g = this.CreateGraphics())
            {
                while (!positionVerified)
                {
                    positionVerified = true;
                    currentMatrix.CalculateMatrixBounds(matrix, g, Fonts.text_font, Fonts.payoff_font);
                    positionVerified = UpdateLocation(matrix);
                }
            }
        }

        // This subroutine shifts the matrix once when it's bounds intersect with other matricies
        private bool UpdateLocation(Matrix matrix)
        {
            foreach (Matrix matrixInLoop in savedMaticies)
            {
                if (matrixInLoop == matrix) continue;
                if (matrix.GetHitbox().IntersectsWith(matrixInLoop.GetHitbox()))
                {
                    matrix.ChangeX(20);
                    matrix.ChangeY(20);
                    return false;
                }
            }
            return true;
        }

        // This subroutine checks that the new location of a matrix does not contain another matrix
        private bool CheckLocationForMatrix(Graphics g)
        {
            foreach (Matrix savedMatrix in savedMaticies)
            {
                {
                    //avoid checking coordinates of the dragged matrix with it's old position
                    if (savedMatrix.IsMoving())
                    {
                        continue;
                    }

                    //get the dymentions of the matrix that is being compared with

                    savedMatrix.CalculateMatrixBounds(savedMatrix, g, Fonts.text_font, Fonts.payoff_font);

                    if (savedMatrix.GetHitbox().IntersectsWith(movingMatrix.GetHitbox()))
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        // This subroutine returns the matrix to it's location before it was dragged
        private void ReturnToStartingPosition()
        {
            movingMatrix.SetX(Points.startingPosition.X);
            movingMatrix.SetY(Points.startingPosition.Y);
            MessageBox.Show("Matrices cannot overlap");
        }

        // This subroutine sets all booleans that are responsible for moving a matrix to false and finishes the process
        private void TerminateMatrixMoving()
        {
            isDragged = false;
            movingMatrix.SetIsMoving(false);
            movingMatrix = null;
        }

        // This subroutine opens a pre-recorded tutorial video
        private void tutorialButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=rA57mAI6cKc");
        }


        // Cnavas Input response

        // This subroutine responds to a user clicking on the canvas and calling appropriate response subroutines
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            Points.worldMouseCoord = screenToWorldTranslate(e.Location);

            using (Graphics g = this.CreateGraphics())
            {

                foreach (Matrix matrix in savedMaticies)
                {
                    if (matrix != null)
                    {
                        matrix.CalculateMatrixBounds(matrix, g, Fonts.text_font, Fonts.payoff_font);

                        if (e.Button == MouseButtons.Right && matrix.GetHitbox().Contains(Points.worldMouseCoord))
                        {

                            if (connectionSelection)
                            {
                                ConnectionModelSelection(matrix);
                            }
                            else if (matrixSelection)
                            {
                                matrixSelection = false;
                                BestResponceEnumeration(matrix);
                                ChoosingMatrixBool.BackColor = Color.White;
                            }
                            else if (chossingPayoffToDeleteConnection)
                            {

                                int[] cellIndex = matrix.IdentifyCellClicked(Points.worldMouseCoord);
                                originNode = new Node(matrix, cellIndex[0], cellIndex[1]);

                                chossingPayoffToDeleteConnection = false;
                                chossingMatrixToDeleteConnection = true;

                                MessageBox.Show($"Payoff selected: [{matrix.GetOnePayoff(cellIndex[0], cellIndex[1])}] from [{matrix.GetName()}]. Please select the destination matrix.");

                            }
                            else if (chossingMatrixToDeleteConnection)
                            {
                                destinationMatrix = matrix;
                                chossingMatrixToDeleteConnection = false;
                                ComponentDeletion((Matrix)originNode.GetModelReference(), originNode.GetRowIndex(), originNode.GetColIndex(), destinationMatrix);
                            }
                            else if (choosingMatrixToDeleteEntireConnection)
                            {
                                choosingMatrixToDeleteEntireConnection = false;
                                DeleteAllNodes(matrix);
                            }
                            else if (selectingMatrixToSolveConnection)
                            {
                                selectingMatrixToSolveConnection = false;
                                ConnectionInitialiseIndicator.BackColor = Color.White;
                                TraverseConnectionToNash(matrix);
                            }
                            else
                            {
                                editMatrix(matrix);
                            }
                            break;

                        }
                        else if (matrix.GetHitbox().Contains(Points.worldMouseCoord))
                        {
                            movingMatrix = matrix;
                            movingMatrix.SetIsMoving(true);
                            isDragged = true;

                            Points.selectPoint = new PointF(Points.worldMouseCoord.X - matrix.GetX(), Points.worldMouseCoord.Y - matrix.GetY());

                            Points.startingPosition = new PointF(matrix.GetX(), matrix.GetY());

                            break;
                        }
                    }

                }
            }

            if (movingMatrix == null && e.Button == MouseButtons.Left)
            {
                panning = true;
                Points.previousPoint = e.Location;
            }

        }

        // This subroutine handles mouse movement on the canvas
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragged && movingMatrix != null)
            {
                DragMatrix(e);
            }
            else if (panning)
            {
                PanCanvas(e);
            }
        }

        // This subroutine finalises panning or matrix movement when the user releases the mouse button
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragged && movingMatrix != null)
            {

                using (Graphics g = this.CreateGraphics())
                {
                    movingMatrix.CalculateMatrixBounds(movingMatrix, g, Fonts.text_font, Fonts.payoff_font);
                    bool collision = CheckLocationForMatrix(g);

                    if (collision)
                    {
                        ReturnToStartingPosition();
                    }

                    TerminateMatrixMoving();
                    Canvas.Invalidate();
                }
            }
            panning = false;
        }

        // This subroutine responds to user scrolling the mousewheel by scaling the canvas
        private void Canvas_MouseWheel(object sender, MouseEventArgs e)
        {
            PointF currentMousePoint = new PointF(e.X, e.Y);
            PointF currentCanvasPoint = screenToWorldTranslate(currentMousePoint);

            //if zoom is positive, increment by 10% ech time, if zoom is negative, decrease by 10%
            ZoomChangeSelection(e);

            ApplyZoom(currentMousePoint, currentCanvasPoint);

            Canvas.Invalidate();
        }

        // This subroutine is used in Canvas_MouseWheel to determine by how much the canvas should be scaled
        private void ZoomChangeSelection(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                zoomDelta = zoomDelta * 1.1f;
            }
            else
            {
                zoomDelta = zoomDelta * 0.9f;
            }

            // Set limits on how big or small zoom can be
            if (zoomDelta < 0.05f)
            {
                zoomDelta = 0.05f;
            }
            if (zoomDelta > 4.0f)
            {
                zoomDelta = 4.0f;
            }
        }

        // This subroutine is used in Canvas_MouseWheel to apply the inputed change to the canvas
        private void ApplyZoom(PointF currentMousePoint, PointF currentCanvasPoint)
        {
            Points.zoomFocus.X = currentMousePoint.X - (currentCanvasPoint.X * zoomDelta);
            Points.zoomFocus.Y = currentMousePoint.Y - (currentCanvasPoint.Y * zoomDelta);
        }

        // This subroutine adjusts the x and y coordinates of the moving matrix when it is dragged
        private void DragMatrix(MouseEventArgs e)
        {
            //get current world mouse position
            Points.worldMouseCoord = screenToWorldTranslate(e.Location);

            //new matrix position
            movingMatrix.SetX(Points.worldMouseCoord.X - Points.selectPoint.X);
            movingMatrix.SetY(Points.worldMouseCoord.Y - Points.selectPoint.Y);

            Canvas.Invalidate();
        }

        // This subroutine adjusts the X and Y coordinates of the canvas when the user is dragging on an empty space
        private void PanCanvas(MouseEventArgs e)
        {
            Points.zoomFocus.X += e.X - Points.previousPoint.X;
            Points.zoomFocus.Y += e.Y - Points.previousPoint.Y;
            Points.previousPoint = e.Location;
            Canvas.Invalidate();
        }


        //Draw Canvas methods


        // This subroutine adjusts the canvas to the zoom and calls subroutines to draw all components
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(Points.zoomFocus.X, Points.zoomFocus.Y);
            e.Graphics.ScaleTransform(zoomDelta, zoomDelta);

            DrawAllMatricies(e);
            DrawAllConnections(e);
        }

        // This subroutine draws all existing matricies each time a canvas is updated
        private void DrawAllMatricies(PaintEventArgs e)
        {
            foreach (Matrix matrix in savedMaticies)
            {
                if (matrix != null)
                {
                    DrawMatrix(e.Graphics, matrix);
                }
            }
        }

        // This subroutine draws all existing connections each time a canvas is updated
        private void DrawAllConnections(PaintEventArgs e)
        {
            foreach (Connection connection in existingConnections)
            {
                DrawConnection(e.Graphics, connection);
            }

            if (currentConnection != null)
            {
                DrawConnection(e.Graphics, currentConnection);
            }
        }

        // This subroutine draws a matrix on a canvas which is passed as a parameter
        private void DrawMatrix(Graphics g, Matrix matrix)
        {
            if (matrix != null)
            {
                matrix.SetCellWidth(matrix.DetermineCellWidth(g, matrix, Fonts.text_font, Fonts.payoff_font));
                matrix.SetGridWidth(matrix.GetCols() * matrix.GetCellWidth());
                matrix.SetGridHight(matrix.GetRows() * matrix.GetCellHeight());
                matrix.SetCurrentGrid(new RectangleF(matrix.GetX() + matrix.GetCellWidth(), matrix.GetY() + matrix.GetCellHeight(), matrix.GetGridWidth(), matrix.GetGridHight()));

                DrawOrigin(g);

                DrawGridOuterBounds(matrix.GetCurrentGrid(), g);

                FillCells(g, matrix);

                using (Pen gridPen = new Pen(Color.Black, 1))
                using (StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {

                    //Draw the player names and the name of the game
                    DrawNames(matrix, g, matrix.GetCellWidth(), matrix.GetGridWidth(), format, matrix.GetCellHeight(), matrix.GetGridHight());

                    for (int r = 0; r < matrix.GetRows(); r++)
                    {
                        matrix.SetRowYCord(matrix.GetY() + matrix.GetCellHeight() + (r * matrix.GetCellHeight()));
                        matrix.SetRowStrategyRectangle(new RectangleF((matrix.GetX()) - matrix.GetCellBuffer(), matrix.GetRowYCord(), matrix.GetCellWidth(), matrix.GetCellHeight()));

                        DrawOneStrategy(matrix, r, matrix.GetRowStrategyRectangle(), format, g);

                        for (int c = 0; c < matrix.GetCols(); c++)
                        {
                            matrix.SetColXCord(matrix.GetX() + matrix.GetCellWidth() + (c * matrix.GetCellWidth()));
                            matrix.SetCellRectangle(new RectangleF(matrix.GetColXCord(), matrix.GetRowYCord(), matrix.GetCellWidth(), matrix.GetCellHeight()));

                            if (r == 0)
                            {
                                matrix.SetColStrategyRectangle(new RectangleF(matrix.GetColXCord(), matrix.GetY(), matrix.GetCellWidth(), matrix.GetCellHeight()));
                                DrawOneStrategy(matrix, c, matrix.GetColStrategyRectangle(), format, g);
                            }

                            DrawGridInnerBounds(gridPen, matrix.GetColXCord(), matrix.GetRowYCord(), matrix.GetCellWidth(), matrix.GetCellHeight(), g, matrix);
                            DrawOnePayoff(matrix, r, c, matrix.GetCellRectangle(), format, g);
                        }
                    }
                }
            }

        }

        // This subroutine draws one connection which is passed as a parameter
        private void DrawConnection(Graphics g, Connection connection)
        {
            using (Pen pen = new Pen(Color.Black, 2))
            {
                pen.CustomEndCap = connectionArrow;

                foreach (LinkedList<Node> chain in connection.GetConnectedComponents())
                {
                    if (chain.Count < 2)
                    {
                        continue;
                    }

                    originNode = chain.First.Value;
                    currentNode = chain.First.Next.Value;
                    Matrix originMatrix = (Matrix)originNode.GetModelReference();

                    Points.connectionStart = CellCenter(g, originMatrix, originNode.GetRowIndex(), originNode.GetColIndex());
                    Points.connectionStart.X += 30;
                    Points.connectionStart.Y += -20;

                    while (currentNode != null)
                    {
                        destinationMatrix = (Matrix)currentNode.GetModelReference();

                        Points.connectionEnd = MatrixNameLocation(destinationMatrix);
                        Points.connectionEnd.X -= g.MeasureString(destinationMatrix.GetName(), Fonts.name_font).Width / 2;
                        Points.connectionEnd.Y -= 12;

                        if (originMatrix == destinationMatrix)
                        {
                            ConnectCellToItsMatrix(g, pen, Points.connectionStart, Points.connectionEnd);
                        }
                        else
                        {
                            g.DrawLine(pen, Points.connectionStart, Points.connectionEnd);
                        }

                        if (chain.Find(currentNode).Next != null)
                        {
                            currentNode = chain.Find(currentNode).Next.Value;
                        }
                        else
                        {
                            currentNode = null;
                        }
                    }
                }

            }
        }

        // This subroutine draws the arrow between a connected cell and matrix
        private void ConnectCellToItsMatrix(Graphics g, Pen p, PointF cellCoord, PointF nameCoord)
        {
            g.DrawBezier(p, cellCoord, new PointF(cellCoord.X + 100, cellCoord.Y), new PointF(nameCoord.X + 100, nameCoord.Y), nameCoord);
        }

        // This subroutine fills the background of the matrix cells with white colour
        public void FillCells(Graphics g, Matrix matrix)
        {
            g.FillRectangle(Brushes.White, matrix.GetCurrentGrid());
        }

        // This subroutine draws names of the players and the name of the matrix
        private void DrawNames(Matrix matrix, Graphics g, float cellWidth, float gridW, StringFormat format, float cellHeight, float gridH)
        {
            g.DrawString(matrix.GetOnePlayer(0), Fonts.player_font, Brushes.Black, new PointF(matrix.GetX() - (g.MeasureString(matrix.GetOnePlayer(0), Fonts.player_font).Width / 2) - matrix.GetCellBuffer(), matrix.GetY() + cellHeight + (gridH / 2)), format); // I couldnt work out how to allign the name vertically as it cept on clashing with strategies so I asked Gemeni for a robust maths that ensures perfect allignment 
            g.DrawString(matrix.GetOnePlayer(1), Fonts.payoff_font, Brushes.Black, new PointF(matrix.GetX() + cellWidth + (gridW / 2), matrix.GetY() - 10), format);
            g.DrawString(matrix.GetName(), Fonts.name_font, Brushes.Black, new PointF(matrix.GetX(), matrix.GetY()), format);
        }

        // This subroutine draws the origin red *
        private void DrawOrigin(Graphics g)
        {
            g.DrawString("*", Fonts.origin_font, Brushes.Red, Points.originPoint);
        }

        // This subroutine draws the outer rectangle of the matrix
        private void DrawGridOuterBounds(RectangleF currentGrid, Graphics g)
        {
            g.FillRectangle(Brushes.White, currentGrid);
            g.DrawRectangle(Pens.Black, currentGrid.X, currentGrid.Y, currentGrid.Width, currentGrid.Height);
        }

        // This soubroutine draws boarders of individual cell
        private void DrawGridInnerBounds(Pen gridPen, float colXcrd, float rowYcord, float cellWidth, float cellHight, Graphics g, Matrix matrix)
        {
            g.DrawRectangle(gridPen, colXcrd, rowYcord, cellWidth, matrix.GetCellHeight());
        }

        // This subroutine draws one strategy name in a matrix
        private void DrawOneStrategy(Matrix matrix, int x, RectangleF rowStrategies, StringFormat format, Graphics g)
        {
            g.DrawString(matrix.GetOneRowStrategy(x), Fonts.text_font, Brushes.Black, rowStrategies, format);
        }

        // This subroutine draws one payoff within one cell of a matrix
        private void DrawOnePayoff(Matrix matrix, int r, int c, RectangleF cellPic, StringFormat format, Graphics g)
        {
            g.DrawString(matrix.GetOnePayoff(r, c), Fonts.payoff_font, Brushes.Black, cellPic, format);
        }

        // This subroutine converts screen coordinates to world coordinates
        private PointF screenToWorldTranslate(PointF screenCoord)
        {
            float worldX = (screenCoord.X - Points.zoomFocus.X) / zoomDelta;
            float worldY = (screenCoord.Y - Points.zoomFocus.Y) / zoomDelta;
            return new PointF(worldX, worldY);
        }




        // Connection Methods


        // This subroutine initialises a connection object and begins the connection selection process
        private void ConnectionInitialise_Click(object sender, EventArgs e)
        {
            if (!stopBRESelection() && !stopConnectionSelection())
            {
                currentConnection = new Connection(newConnectionID);
                newConnectionID++;
                connectionSelection = true;
                ConnectionInitialiseIndicator.BackColor = Color.Orange;
                MessageBox.Show("Connection Initialised, Please select cells and matricies that you would like to connect");
            }
        }
        // This subroutine connects a cell of a matrix and a matrix according to user selection
        public void ConnectionModelSelection(Matrix model)
        {
            if (connectionSelection && currentConnection != null)
            {
                if (model != null)
                {
                    if (currentConnection.GetRootModel() == null)
                    {
                        int[] cellIndex = model.IdentifyCellClicked(Points.worldMouseCoord);

                        if (cellIndex[0] != -1)
                        {
                            if (CellConnected(model, cellIndex[0], cellIndex[1]))
                            {
                                return;
                            }
                            currentConnection.SetRootModel(model);
                            model.SetConnectionRowIndeex(cellIndex[0]);
                            model.SetConnectionColIndeex(cellIndex[1]);

                            MessageBox.Show($"Connection origin: payoff [{model.GetOnePayoff(cellIndex[0], cellIndex[1])}] in the model [{model.GetName()}]. Please select the destination model.");
                        }
                    }
                    else
                    {

                        currentMatrix = (Matrix)currentConnection.GetRootModel();
                        currentConnection.AddConection(currentMatrix, model, currentMatrix.GetconnectionRowIndeex(), currentMatrix.GetconnectionColIndeex());
                        currentConnection.SetRootModel(null);
                        Canvas.Invalidate();
                    }
                }
            }
        }

        // This subroutine determines the center of the cell that is within a connection which is used for drawing a connecting arrow
        private PointF CellCenter(Graphics g, Matrix matrix, int row, int col)
        {
            float centerX = matrix.GetX() + matrix.GetCellWidth() + (col * matrix.GetCellWidth()) + (matrix.GetCellWidth() / 2);
            float centerY = matrix.GetY() + matrix.GetCellHeight() + (row * matrix.GetCellHeight()) + (matrix.GetCellHeight() / 2);

            return new PointF(centerX, centerY);
        }

        // This subroutine determines the location of the matrix name within a connection which is used for drawing a connecting arrow
        private PointF MatrixNameLocation(Matrix matrix)
        {
            return new PointF(matrix.GetX(), matrix.GetY());
        }

        // This subroutine prevents a cell being connected to more than one matrix to prevent errors
        private bool CellConnected(Matrix matrix, int row, int col)
        {
            foreach (Connection connection in existingConnections)
            {
                if (connection.GetLinkOfCell(matrix, row, col) != null)
                {
                    MessageBox.Show("One cell can not have more than one connection", "Cell Selection");
                    return true;
                }
            }

            if (currentConnection != null && currentConnection.GetLinkOfCell(matrix, row, col) != null)
            {
                MessageBox.Show("One cell can not have more than one connection.", "Cell Selection");
                return true;
            }

            return false;
        }

        // This subroutine allows the user to deletes a matrix from a connection, the conection is adjusted appropriately
        private void DeleteComponent_Click(object sender, EventArgs e)
        {
            if (!stopBRESelection() && !stopConnectionSelection())
            {
                GetPayoffToDelete();
            }
        }

        // This subroutine takes the user input to determine which connection is about to be deleted
        private void GetPayoffToDelete()
        {
            chossingPayoffToDeleteConnection = true;
            MessageBox.Show("Please select the payoff of the connection you want to delete.");
        }

        // This subroutine deletes a matrix from a connection
        private void ComponentDeletion(Matrix originMatrix, int row, int col, Matrix destinationMatrix)
        {
            for (int i = existingConnections.Count - 1; i >= 0; i--)
            {
                if (existingConnections[i] != null)
                {
                    Connection connection = existingConnections[i];


                    connection.RemoveConnection(originMatrix, row, col, destinationMatrix);

                    if (connection.GetConnectedComponents().Count == 0)
                    {
                        existingConnections.RemoveAt(i);
                    }
                }
            }

            originNode = null;
            destinationMatrix = null;
            MessageBox.Show("Component deletion processed.");
            Canvas.Invalidate();
        }

        // This subroutine initiates deletion of all nodes in a connection
        private void DeleteEntireConnection_Click(object sender, EventArgs e)
        {
            if (!stopBRESelection() && !stopConnectionSelection())
            {
                choosingMatrixToDeleteEntireConnection = true;
                MessageBox.Show("Please choose any matrix from the connection you want to delete.");
            }
        }

        // This subroutine deletes the whole connection by selection one of its components
        private void DeleteAllNodes(Matrix matrix)
        {
            bool connectionDeleted = false;

            for (int i = existingConnections.Count - 1; i >= 0; i--)
            {
                if (FindConnectionContainingMatrix(existingConnections[i], matrix))
                {
                    existingConnections.RemoveAt(i);
                    connectionDeleted = true;
                }
            }

            if (currentConnection != null && FindConnectionContainingMatrix(currentConnection, matrix))
            {
                currentConnection = null;
                connectionSelection = false;
                ConnectionInitialiseIndicator.BackColor = Color.White;
                connectionDeleted = true;
            }

            if (connectionDeleted)
            {
                MessageBox.Show("Connection deleted");
            }
            else
            {
                MessageBox.Show("Deletion unsuccesfull");
            }

            Canvas.Invalidate();
        }

        // This subroutine identifies a connection which contains the matrix that was passed as a parameter
        private bool FindConnectionContainingMatrix(Connection connection, Matrix matrix)
        {
            foreach (LinkedList<Node> link in connection.GetConnectedComponents())
            {
                foreach (Node node in link)
                {
                    if (node.GetModelReference() == matrix)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        // This subroutine is a safety boundary that prevents the user from clicking anything else during connection selection process
        private bool stopConnectionSelection()
        {
            if (connectionSelection)
            {
                DialogResult responce = MessageBox.Show("You have to finish connection selection before proceeding. Would you like to cancel selection?", "Selection in Progress", MessageBoxButtons.YesNo);
                if (responce == DialogResult.Yes)
                {
                    connectionSelection = false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        // This subroutine saves the connection that is being initiated to the list of saved connections
        private void saveConnection_Click(object sender, EventArgs e)
        {
            if (currentConnection != null && (!stopBRESelection() || existingConnections.Count > 0))
            {
                existingConnections.Add(currentConnection);
                currentConnection = null;
                connectionSelection = false;
                ConnectionInitialiseIndicator.BackColor = Color.White;
            }
        }



        // BRE Algorithm


        // This subroutine is a safety boundary that prevents the user from clicking anything else during matrix selection process
        private bool stopBRESelection()
        {
            if (matrixSelection)
            {
                DialogResult responce = MessageBox.Show("You have to finish matrix selection before proceeding. Would you like to cancel selection?", "Selection in Progress", MessageBoxButtons.YesNo);
                if (responce == DialogResult.Yes)
                {
                    matrixSelection = false;
                    ChoosingMatrixBool.BackColor = Color.White;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        // This subroutine is a collection of method calls that make up the BRE algorithm 
        public void BestResponceEnumeration(Matrix matrix)
        {
            if (!solvingConnection)
            {
                MessageBox.Show(matrix.GetName() + " will now be solved via the Best Responce Enumeration Algorithm");
            }


            //prepare data for comparison, i need some way of marking cells in rows and columns as best responces in order to compare them later to find the intersection and also do maths with them
            CreateDataStrucutresForBRE(matrix);

            //Convert payoffs stored as string into float in order to run math with them
            matrix.ConvertPayoffsToFloat(matrix, matrix.GetPlayer1Payoffs(), matrix.GetPlayer2Payoffs(), matrix.GetNashEqualibria());

            // now actual logic of the algorithm 

            //fix the column for player 2
            RowPlayerBRE(matrix);

            ///Player 2 analysis
            //fix a row
            ColPlayerBRE(matrix);


            FindIntersectionsOfBRE(matrix);

            //return results
            if (!solvingConnection)
            {
                ReturnBREResults(matrix);
            }
        }

        // This subroutine prepares the data structures needed for BRE
        private void CreateDataStrucutresForBRE(Matrix matrix)
        {
            matrix.SetPlayer1BestResponses(new bool[matrix.GetRows(), matrix.GetCols()]);
            matrix.SetPlayer2BestResponses(new bool[matrix.GetRows(), matrix.GetCols()]);

            matrix.SetPlayer1Payoffs(new float[matrix.GetRows(), matrix.GetCols()]);
            matrix.SetPlayer2Payoffs(new float[matrix.GetRows(), matrix.GetCols()]);
        }

        // This subroutine fixes the column strategy and finds the row which maximises the payoffs of the player with row strategies
        private void RowPlayerBRE(Matrix matrix)
        {
            for (int col = 0; col < matrix.GetCols(); col++)
            {
                //finds the payoff for player 1 for a currently fixed column
                float maxPlayer1 = float.MinValue; //assume the current payoff for player1 is as bad as bossible as so in the first comparsion the first payoff will always be better not matter how bad it is
                for (int row = 0; row < matrix.GetRows(); row++)
                {
                    if (row == 0)
                    {
                        maxPlayer1 = matrix.GetOnePlayer1Payoff(row, col);
                    }
                    else if (matrix.GetOnePlayer1Payoff(row, col) > maxPlayer1)
                    {
                        maxPlayer1 = matrix.GetOnePlayer1Payoff(row, col);
                    }

                }

                //Highlight each cell with the max payoff for a given cell
                for (int row = 0; row < matrix.GetRows(); row++)
                {
                    if (matrix.GetOnePlayer1Payoff(row, col) == maxPlayer1)
                    {
                        matrix.SetOnePlayer1BestResponse(row, col, true);
                    }
                }
            }
        }

        // This subroutine fixes the row strategy and finds the column which maximises the payoffs of the player with column strategies
        private void ColPlayerBRE(Matrix matrix)
        {
            for (int row = 0; row < matrix.GetRows(); row++)
            {
                //compare possible payoffs
                float maxPlayer2 = int.MinValue;
                for (int columns = 0; columns < matrix.GetCols(); columns++)
                {
                    if (matrix.GetOnePlayer2Payoff(row, columns) > maxPlayer2)
                    {
                        maxPlayer2 = matrix.GetOnePlayer2Payoff(row, columns);
                    }
                }

                //highlight every payoff-maximising cell per given row for player 2
                for (int c = 0; c < matrix.GetCols(); c++)
                {
                    if (matrix.GetOnePlayer2Payoff(row, c) == maxPlayer2)
                    {
                        matrix.SetOnePlayer2BestResponse(row, c, true);
                    }
                }
            }
        }

        // This subroutine finds the cells where the best responding strategies for both players intersect
        private void FindIntersectionsOfBRE(Matrix matrix)
        {
            for (int r = 0; r < matrix.GetRows(); r++)
            {
                for (int c = 0; c < matrix.GetCols(); c++)
                {
                    // if both cells are true, they intersect
                    if (matrix.GetOnePlayer1BestResponse(r, c) && matrix.GetOnePlayer2BestResponse(r, c))
                    {
                        matrix.AddToNashEqualibria($"{matrix.GetOnePlayer(0)} chooses {matrix.GetOneRowStrategy(r)}\n{matrix.GetOnePlayer(1)} chooses {matrix.GetOneColStrategy(r)}\nThe payoffs are: {matrix.GetOnePayoff(r, c)}");
                    }
                }
            }
        }

        // This subroutine returns a text output of the BRE algorithm
        private void ReturnBREResults(Matrix matrix)
        {
            if (matrix.GetNashEqualibria().Count > 0)
            {
                string outputString = "Pure Strategy Nash Equilibria in " + matrix.GetName() + " are:\n\n";
                foreach (string val in matrix.GetNashEqualibria())
                {
                    outputString += val + "\n\n";
                }
                MessageBox.Show(outputString, "Output");
            }
            else
            {
                MessageBox.Show("In " + matrix.GetName() + " no Pure Strategy Nash Equilibrium exists.", "Output");
            }
        }

        // Connection Solving methods

        // This subroutine aggregates the order of solving individual matricies in a connection and returns all final Nash Equalibria
        private void TraverseConnectionToNash(Matrix matrix)
        {
            MessageBox.Show("The connection which starts at matrix '" + matrix.GetName() + "' will now be solved via the Best Responce Enumeration", matrix.GetName());
            solvingConnection = true;

            Queue<Matrix> matrixQueue = new Queue<Matrix>();
            matrixQueue.Enqueue(matrix);

            List<Matrix> visitedMatricies = new List<Matrix>();//track visited matricies to prevent cycles

            List<Matrix> finalMatricies = new List<Matrix>();

            while (matrixQueue.Count > 0)
            {
                currentMatrix = matrixQueue.Dequeue();

                bool pathContinued = false;

                //check for a cycle
                if (visitedMatricies.Contains(currentMatrix))
                {
                    MessageBox.Show($"Cycle at [{currentMatrix.GetName()}]. Infinite loop prevented. Please correct and run the program again.");
                    continue;
                }
                visitedMatricies.Add(currentMatrix);

                //clear global variable before solving a matrix
                currentMatrix.GetNashEqualibria().Clear();
                BestResponceEnumeration(currentMatrix);

                List<Point> NashEqualibriaCells = GetNashEquilibriaCells(currentMatrix);

                //avoid matrticies with no equalibrium
                if (NashEqualibriaCells.Count == 0)
                {
                    MessageBox.Show($"Traversal terminated for one of the branches: no Pure Strategy Nash Equilibrium exists in [{currentMatrix.GetName()}] matrix.");
                    finalMatricies.Add(currentMatrix);
                    continue;
                }

                foreach (Point cell in NashEqualibriaCells)
                {
                    Matrix nextMatrix = GetNextConnectedMatrix(currentMatrix, cell.X, cell.Y);

                    //enqueue all existing connections
                    if (nextMatrix != null)
                    {
                        matrixQueue.Enqueue(nextMatrix);
                        pathContinued = true;
                    }
                }

                //check if the matrix is final
                if (!pathContinued)
                {
                    finalMatricies.Add(currentMatrix);
                }
            }

            solvingConnection = false;

            //output the nash equalibria
            if (finalMatricies.Count > 0)
            {
                //avoid duplicates of solutions
                finalMatricies = finalMatricies.Distinct().ToList();

                string combinedOutput = null;

                //combine all outcomes and present in one window
                foreach (Matrix finalMatrix in finalMatricies)
                {
                    if (finalMatrix.GetNashEqualibria().Count > 0)
                    {
                        combinedOutput += "Pure Strategy Nash Equilibria in '" + finalMatrix.GetName() + "':\n";
                        foreach (string outcome in finalMatrix.GetNashEqualibria())
                        {
                            combinedOutput += outcome + "\n" + "\n----------------------------------------\n\n";
                        }
                    }
                }
                MessageBox.Show(combinedOutput);
            }
        }

        // This subroutine returns a list of cells which are the Nash Equalibria
        private List<Point> GetNashEquilibriaCells(Matrix matrix)
        {
            List<Point> nashEquilibria = new List<Point>();

            for (int row = 0; row < matrix.GetRows(); row++)
            {
                for (int col = 0; col < matrix.GetCols(); col++)
                {
                    if (matrix.GetOnePlayer1BestResponse(row, col) && matrix.GetOnePlayer2BestResponse(row, col))
                    {
                        nashEquilibria.Add(new Point(row, col));
                    }
                }
            }
            return nashEquilibria;
        }

        // This subroutine determines the matrix that is connected to the matrix passed as a parameter
        private Matrix GetNextConnectedMatrix(Matrix originMatrix, int row, int col)
        {
            foreach (Connection connection in existingConnections)
            {
                LinkedList<Node> link = connection.GetLinkOfCell(originMatrix, row, col);

                if (link != null && link.First != null && link.First.Next != null)
                {
                    return (Matrix)link.First.Next.Value.GetModelReference();
                }
            }

            return null;
        }

        // Cnavas coordination

        // This subroutine returns the user's "window" of the canvas to the origin where new matrices appear
        private void return_to_origin_Click(object sender, EventArgs e)
        {
            Points.zoomFocus = Points.originPoint;
            Canvas.Invalidate();
        }

        // This subroutine moves all existing matricies as close to the origin as possible and avoids matricies overlapping
        private void lockalise_matricies_Click(object sender, EventArgs e)
        {
            foreach (Matrix matrix in savedMaticies)
            {
                matrix.SetX(150);
                matrix.SetY(80);
                localise_matrix(matrix);
            }
            Canvas.Invalidate();
        }

        //return zoom to default value if too zoomed in on something too much or out too much, more of a shortcut than anything else
        private void zoom_to_default_Click(object sender, EventArgs e)
        {
            zoomDelta = 0.9f;
            Canvas.Invalidate();
        }


        // Exit Modes Buttons

        // This subroutine completely exits the connection selection process
        private void CancelSelection_Click(object sender, EventArgs e)
        {
            if (!stopBRESelection())
            {
                connectionSelection = false;
                ConnectionInitialiseIndicator.BackColor = Color.White;
            }
        }

        // This subroutine calls a method which completely exits the matrix selection process
        private void ExitMatrixSelection_Click(object sender, EventArgs e)
        {
            matrixSelection = false;
            ChoosingMatrixBool.BackColor = Color.White;

        }

        // This subroutine completely exits the connection creation mode and delets the unsaved connection from memory
        private void ExitConnectionSelection_Click(object sender, EventArgs e)
        {
            ConnectionInitialiseIndicator.BackColor = Color.White;
            connectionSelection = false;
            currentConnection = null;
            Canvas.Invalidate();
        }


        // Solve Buttons

        // This subroutine initiates the process of solving a connection of matrices
        private void SolveConnection_Click(object sender, EventArgs e)
        {
            if (existingConnections.Count == 0)
            {
                MessageBox.Show("Please create at least one connection to solve");
                return;
            }

            MessageBox.Show("Please select the origin model of the connection to solve.");
            selectingMatrixToSolveConnection = true;
            ConnectionInitialiseIndicator.BackColor = Color.Orange;
        }

        // This subroutine initiates the selection of a matrix
        public void select_Matrix()
        {
            //first i have to identify what matrix to solve, if there are more than 1, user will have to select which one
            currentMatrix = null;
            int matrixCount = savedMaticies.Count();

            if (matrixCount == 0)
            {
                MessageBox.Show("There are currently no existing matrix");
            }
            else
            {
                MessageBox.Show("Please select a matrix to solve");
                matrixSelection = true;
                ChoosingMatrixBool.BackColor = Color.Orange;
            }

        }

        // This subroutine calls select_Matrix when the user wishes to solve a matrix
        private void solveButton_Click(object sender, EventArgs e)
        {
            select_Matrix();
        }

    }
}