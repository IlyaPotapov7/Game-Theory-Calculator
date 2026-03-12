using Game_Theory_Calculator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Game_Theory_Calculator
{
    public partial class mainWindow : Form
    {
        // Here I will initialise variables for all subroutines to make them easier to organise

        private Models.Matrix currentMatrix;
        public List<Models.Matrix> savedMaticies;
        public Models.Matrix movingMatrix;
        public List<Connection> existingConnections;

        private Connection currentConnection;
        public int newModelID = 0;
        private int newConnectionID;
        private Node originNode;
        private Node currentNode;
        private Models.Matrix destinationMatrix;

        public Font text_font = new Font("Times New Roman", 11, FontStyle.Regular);
        public Font payoff_font = new Font("Times New Roman", 12, FontStyle.Regular);
        public Font player_font = new Font("Times New Roman", 12, FontStyle.Bold);
        public Font name_font = new Font("Times New Roman", 14, FontStyle.Italic);
        public Font origin_font = new Font("Times New Roman", 25, FontStyle.Bold);
        private AdjustableArrowCap connectionArrow = new AdjustableArrowCap(5, 5);

        private float zoomDelta = 0.9f;

        private PointF connectionStart;
        private PointF connectionEnd;
        private PointF zoomFocus = new PointF(0, 0);
        private PointF originPoint = new PointF(0, 0);
        private PointF selectPoint = new PointF(0, 0);
        private PointF startingPosition = new PointF(0, 0);
        private PointF previousPoint;
        private PointF worldMouseCoord;

        private bool connectionSelection;
        private bool panning = false;
        private bool matrixSelection = false;
        private bool EditingMatrix;
        private bool positionVerified;
        private bool collision;
        private bool isDragged = false;
        private bool chossingPayoffToDeleteConnection = false;
        private bool chossingMatrixToDeleteConnection = false;
        private bool choosingMatrixToDeleteEntireConnection = false;
        private bool solvingConnection = false;
        private bool selectingMatrixToSolveConnection = false;

        public mainWindow()
        {
            InitializeComponent();
            Canvas.MouseWheel += Canvas_MouseWheel;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            savedMaticies = new List<Models.Matrix>();
            existingConnections = new List<Connection>();
        }

        // This subroutine will handle the initialisation of a new matrix
        private void MatrixInitialise_Click(object sender, EventArgs e)
        {
            if (!stopBRESelection())
            {
                currentMatrix = new Models.Matrix();
                currentMatrix = currentMatrix.defaultMatrix(savedMaticies, newModelID);
                currentMatrix.SetID(newModelID);
                newModelID++;
                editMatrix(currentMatrix);
            }
        }

        public void localise_matrix(Models.Matrix matrix)
        {
            positionVerified = false;

            using (Graphics g = this.CreateGraphics())
            {
                //try different locaations until it is not taken
                while (!positionVerified)
                {
                    //assume true, and if not change back to false
                    positionVerified = true;
                    currentMatrix.CalculateMatrixBounds(matrix, g, text_font, payoff_font);
                    UpdateLocation(matrix);

                }

            }
        }

        private void UpdateLocation(Models.Matrix matrix)
        {
            foreach (Models.Matrix m in savedMaticies)
            {
                if (m == matrix)
                {
                    continue;
                }
                else if (matrix.GetHitbox().IntersectsWith(m.GetHitbox()))
                {
                    positionVerified = false;
                    matrix.ChangeX(20);
                    matrix.ChangeY(20);
                    break;
                }

            }
        }

        public void editMatrix(Models.Matrix matrix)
        {
            currentMatrix = matrix;
            MatrixModification MM = OpenMatrixEditWindow();
            SaveMatrixModification(MM);
            Canvas.Invalidate();
        }

        private MatrixModification OpenMatrixEditWindow()
        {
            EditingMatrix = true;
            MatrixModification MM = new MatrixModification();
            MM.recieveMatrix(currentMatrix);
            MM.ShowDialog();
            return MM;
        }

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
        private void tutorialButton_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=rA57mAI6cKc");

        }

        private void Canvas_Click(object sender, EventArgs e)
        {

        }
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

        private void DragMatrix(MouseEventArgs e)
        {
            //get current world mouse position
            worldMouseCoord = screenToWorldTranslate(e.Location);

            //new matrix position
            movingMatrix.SetX(worldMouseCoord.X - selectPoint.X);
            movingMatrix.SetY(worldMouseCoord.Y - selectPoint.Y);

            Canvas.Invalidate();
        }

        private void PanCanvas(MouseEventArgs e)
        {
            zoomFocus.X += e.X - previousPoint.X;
            zoomFocus.Y += e.Y - previousPoint.Y;
            previousPoint = e.Location;
            Canvas.Invalidate();
        }
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragged && movingMatrix != null)
            {
                collision = false;

                using (Graphics g = this.CreateGraphics())
                {
                    movingMatrix.CalculateMatrixBounds(movingMatrix, g, text_font, payoff_font);

                    CheckLocationForMatrix(g);

                    //if the collision happens, the moving matrix returns back to where it was before the movement

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

        private void CheckLocationForMatrix(Graphics g)
        {
            foreach (Models.Matrix savedMatrix in savedMaticies)
            {
                {
                    //avoid checking coordinates of the dragged matrix with it's old position
                    if (savedMatrix.IsMoving())
                    {
                        continue;
                    }

                    //get the dymentions of the matrix that is being compared with

                    savedMatrix.CalculateMatrixBounds(savedMatrix, g, text_font, payoff_font);

                    if (savedMatrix.GetHitbox().IntersectsWith(movingMatrix.GetHitbox()))
                    {
                        collision = true;
                        break;
                    }

                }
            }
        }

        private void ReturnToStartingPosition()
        {
            movingMatrix.SetX(startingPosition.X);
            movingMatrix.SetY(startingPosition.Y);
            MessageBox.Show("Matrices cannot overlap");
        }

        private void TerminateMatrixMoving()
        {
            isDragged = false;
            movingMatrix.SetIsMoving(false);
            movingMatrix = null;
        }
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            worldMouseCoord = screenToWorldTranslate(e.Location);

            using (Graphics g = this.CreateGraphics())
            {

                foreach (Models.Matrix matrix in savedMaticies)
                {
                    if (matrix != null)
                    {
                        matrix.CalculateMatrixBounds(matrix, g, text_font, payoff_font);

                        if (e.Button == MouseButtons.Right && matrix.GetHitbox().Contains(worldMouseCoord))
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

                                int[] cellIndex = matrix.IdentifyCellClicked(worldMouseCoord);
                                originNode = new Node(matrix, cellIndex[0], cellIndex[1]);

                                chossingPayoffToDeleteConnection = false;
                                chossingMatrixToDeleteConnection = true;

                                MessageBox.Show($"Payoff selected: [{matrix.GetOnePayoff(cellIndex[0], cellIndex[1])}] from [{matrix.GetName()}]. Please select the destination matrix.");

                            }
                            else if (chossingMatrixToDeleteConnection)
                            {
                                destinationMatrix = matrix;
                                chossingMatrixToDeleteConnection = false;
                                ComponentDeletion((Models.Matrix)originNode.GetModelReference(), originNode.GetRowIndex(), originNode.GetColIndex(), destinationMatrix);
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
                        else if (matrix.GetHitbox().Contains(worldMouseCoord))
                        {
                            movingMatrix = matrix;
                            movingMatrix.SetIsMoving(true);
                            isDragged = true;

                            selectPoint = new PointF(worldMouseCoord.X - matrix.GetX(), worldMouseCoord.Y - matrix.GetY());

                            startingPosition = new PointF(matrix.GetX(), matrix.GetY());

                            break;
                        }
                    }

                }
            }

            if (movingMatrix == null && e.Button == MouseButtons.Left)
            {
                panning = true;
                previousPoint = e.Location;
            }

        }

        public void ConnectionModelSelection(Models.Matrix model)
        {
            if (connectionSelection && currentConnection != null)
            {
                if (model != null)
                {
                    if (currentConnection.GetRootModel() == null)
                    {
                        int[] cellIndex = model.IdentifyCellClicked(worldMouseCoord);

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

                        currentMatrix = (Models.Matrix)currentConnection.GetRootModel();
                        currentConnection.AddConection(currentMatrix, model, currentMatrix.GetconnectionRowIndeex(), currentMatrix.GetconnectionColIndeex());
                        currentConnection.SetRootModel(null);
                        Canvas.Invalidate();
                    }
                }
            }
        }
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(zoomFocus.X, zoomFocus.Y);
            e.Graphics.ScaleTransform(zoomDelta, zoomDelta);

            DrawAllMatricies(e);
            DrawAllConnections(e);
        }

        private void DrawAllMatricies(PaintEventArgs e)
        {
            foreach (Models.Matrix matrix in savedMaticies)
            {
                if (matrix != null)
                {
                    DrawMatrix(e.Graphics, matrix);
                }
            }
        }

        private void DrawAllConnections(PaintEventArgs e)
        {
            foreach (Models.Connection connection in existingConnections)
            {
                DrawConnection(e.Graphics, connection);
            }

            if (currentConnection != null)
            {
                DrawConnection(e.Graphics, currentConnection);
            }
        }
        private bool CellConnected(Models.Matrix matrix, int row, int col)
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

        //this method will contain all drawing logic for the canvas
        private void DrawMatrix(Graphics g, Models.Matrix matrix)
        {
            if (matrix != null)
            {
                matrix.SetCellWidth(matrix.DetermineCellWidth(g, matrix, text_font, payoff_font));
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

        public void FillCells(Graphics g, Models.Matrix matrix)
        {
            g.FillRectangle(Brushes.White, matrix.GetCurrentGrid());
        }
        private void DrawNames(Models.Matrix matrix, Graphics g, float cellWidth, float gridW, StringFormat format, float cellHeight, float gridH)
        {
            g.DrawString(matrix.GetOnePlayer(0), player_font, Brushes.Black, new PointF(matrix.GetX() - (g.MeasureString(matrix.GetOnePlayer(0), player_font).Width / 2) - matrix.GetCellBuffer(), matrix.GetY() + cellHeight + (gridH / 2)), format); // I couldnt work out how to allign the name vertically as it cept on clashing with strategies so I asked Gemeni for a robust maths that ensures perfect allignment 
            g.DrawString(matrix.GetOnePlayer(1), player_font, Brushes.Black, new PointF(matrix.GetX() + cellWidth + (gridW / 2), matrix.GetY() - 10), format);
            g.DrawString(matrix.GetName(), name_font, Brushes.Black, new PointF(matrix.GetX(), matrix.GetY()), format);
        }

        private void DrawOrigin(Graphics g)
        {
            g.DrawString("*", origin_font, Brushes.Red, originPoint);
        }

        private void DrawGridOuterBounds(RectangleF currentGrid, Graphics g)
        {
            g.FillRectangle(Brushes.White, currentGrid);
            g.DrawRectangle(Pens.Black, currentGrid.X, currentGrid.Y, currentGrid.Width, currentGrid.Height);
        }

        private void DrawGridInnerBounds(Pen gridPen, float colXcrd, float rowYcord, float cellWidth, float cellHight, Graphics g, Models.Matrix matrix)
        {
            g.DrawRectangle(gridPen, colXcrd, rowYcord, cellWidth, matrix.GetCellHeight());
        }

        private void DrawOneStrategy(Models.Matrix matrix, int x, RectangleF rowStrategies, StringFormat format, Graphics g)
        {
            g.DrawString(matrix.GetOneRowStrategy(x), text_font, Brushes.Black, rowStrategies, format);
        }

        private void DrawOnePayoff(Models.Matrix matrix, int r, int c, RectangleF cellPic, StringFormat format, Graphics g)
        {
            g.DrawString(matrix.GetOnePayoff(r, c), payoff_font, Brushes.Black, cellPic, format);
        }
        //this method will handel the mousewheel input and in responce change the level of zoom on the canvas
        private void Canvas_MouseWheel(object sender, MouseEventArgs e)
        {
            //i need to convert the world coordinates into screen coordinates
            //first, handle the shift of the focus point X - ZoomFocus and account for the zoom by deviding the current difference by the change in zoom
            PointF currentMousePoint = new PointF(e.X, e.Y);
            PointF currentCanvasPoint = screenToWorldTranslate(currentMousePoint);

            //if zoom is positive, increment by 10% ech time, if zoom is negative, fecrease by 10%
            ZoomChangeSelection(e);

            //make the actual change to how the image looks, e.X and e.Y are the centre or the focus - saceld world distance (as a reuslt the point of zoom is alligned with the location of the mouse
            ApplyZoom(currentMousePoint, currentCanvasPoint);

            //update the changes to the canvas
            Canvas.Invalidate();
        }

        //this subroutine will convert screen coordinates to world coordinates which is nessecary for selecting objects on a canvas by clicking on them 
        private PointF screenToWorldTranslate(PointF screenCoord)
        {
            float worldX = (screenCoord.X - zoomFocus.X) / zoomDelta;
            float worldY = (screenCoord.Y - zoomFocus.Y) / zoomDelta;
            return new PointF(worldX, worldY);
        }

        private void ApplyZoom(PointF currentMousePoint, PointF currentCanvasPoint)
        {
            zoomFocus.X = currentMousePoint.X - (currentCanvasPoint.X * zoomDelta);
            zoomFocus.Y = currentMousePoint.Y - (currentCanvasPoint.Y * zoomDelta);
        }

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
                    Models.Matrix originMatrix = (Models.Matrix)originNode.GetModelReference();

                    connectionStart = CellCenter(g, originMatrix, originNode.GetRowIndex(), originNode.GetColIndex());
                    connectionStart.X += 30;
                    connectionStart.Y += -20;

                    while (currentNode != null)
                    {
                        destinationMatrix = (Models.Matrix)currentNode.GetModelReference();

                        connectionEnd = MatrixNameLocation(destinationMatrix);
                        connectionEnd.X -= g.MeasureString(destinationMatrix.GetName(), name_font).Width / 2;
                        connectionEnd.Y -= 12;

                        if (originMatrix == destinationMatrix)
                        {
                            ConnectCellToItsMatrix(g, pen, connectionStart, connectionEnd);
                        }
                        else
                        {
                            g.DrawLine(pen, connectionStart, connectionEnd);
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
        //calculate where the arrow should start from
        private PointF CellCenter(Graphics g, Models.Matrix matrix, int row, int col)
        {
            float centerX = matrix.GetX() + matrix.GetCellWidth() + (col * matrix.GetCellWidth()) + (matrix.GetCellWidth() / 2);
            float centerY = matrix.GetY() + matrix.GetCellHeight() + (row * matrix.GetCellHeight()) + (matrix.GetCellHeight() / 2);

            return new PointF(centerX, centerY);
        }
        //find where the arrow should go to
        private PointF MatrixNameLocation(Models.Matrix matrix)
        {
            return new PointF(matrix.GetX(), matrix.GetY());
        }
        private void ConnectCellToItsMatrix(Graphics g, Pen p, PointF cellCoord, PointF nameCoord)
        {
            g.DrawBezier(p, cellCoord, new PointF(cellCoord.X + 100, cellCoord.Y), new PointF(nameCoord.X + 100, nameCoord.Y), nameCoord);
        }


        //I will use this method as a safety boundariy so that is the user clicks anything else during matrix selection process, he will have to either terminate selection or be remined that he is selecting a m atrix
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
        private void solveButton_Click(object sender, EventArgs e)
        {
            select_Matrix();
            //now, the current matrix is the matrix that the user wants to solve, so we can implement the solving algorithm
        }

        public void BestResponceEnumeration(Models.Matrix matrix)
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

        private void CreateDataStrucutresForBRE(Models.Matrix matrix)
        {
            matrix.SetPlayer1BestResponses(new bool[matrix.GetRows(), matrix.GetCols()]);
            matrix.SetPlayer2BestResponses(new bool[matrix.GetRows(), matrix.GetCols()]);

            matrix.SetPlayer1Payoffs(new float[matrix.GetRows(), matrix.GetCols()]);
            matrix.SetPlayer2Payoffs(new float[matrix.GetRows(), matrix.GetCols()]);
        }

        private void RowPlayerBRE(Models.Matrix matrix)
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

        private void ColPlayerBRE(Models.Matrix matrix)
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

        private void FindIntersectionsOfBRE(Models.Matrix matrix)
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

        private void ReturnBREResults(Models.Matrix matrix)
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



        //now i will add methods for easier canvas navigation 

        // origin is the red dot in the top left corner next to which matricies appear when they are initialised
        private void return_to_origin_Click(object sender, EventArgs e)
        {
            zoomFocus = originPoint;
            Canvas.Invalidate();
        }


        //make all matricies appear close to origin so no matricies are lost
        private void lockalise_matricies_Click(object sender, EventArgs e)
        {
            foreach (Models.Matrix matrix in savedMaticies)
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

        private void ChoosingMatrixBool_Click(object sender, EventArgs e)
        {

        }

        private void ConnectionInitialiseIndicator_Click(object sender, EventArgs e)
        {

        }

        private void CancelSelection_Click(object sender, EventArgs e)
        {
            if (!stopBRESelection())
            {
                connectionSelection = false;
                ConnectionInitialiseIndicator.BackColor = Color.White;
            }
        }

        private void ExitMatrixSelection_Click(object sender, EventArgs e)
        {
            matrixSelection = false;
            ChoosingMatrixBool.BackColor = Color.White;

        }

        private void ExitConnectionSelection_Click(object sender, EventArgs e)
        {
            ConnectionInitialiseIndicator.BackColor = Color.White;
            connectionSelection = false;
            currentConnection = null;
            Canvas.Invalidate();
        }



        private void CancelSelectedCell_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void DeleteComponent_Click(object sender, EventArgs e)
        {
            if (!stopBRESelection() && !stopConnectionSelection())
            {
                GetPayoffToDelete();

            }
        }

        private void ComponentDeletion(Models.Matrix originMatrix, int row, int col, Models.Matrix destinationMatrix)
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

        private void GetPayoffToDelete()
        {
            chossingPayoffToDeleteConnection = true;
            MessageBox.Show("Please select the payoff of the connection you want to delete.");
        }

        private void DeleteEntireConnection_Click(object sender, EventArgs e)
        {
            if (!stopBRESelection() && !stopConnectionSelection())
            {

                choosingMatrixToDeleteEntireConnection = true;
                MessageBox.Show("Please choose any matrix from the connection you want to delete.");
            }
        }

        private void DeleteAllNodes(Models.Matrix matrix)
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

        private bool FindConnectionContainingMatrix(Models.Connection connection, Models.Matrix matrix)
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

        //similarly to BRE this algorithm will use a queue to traverse the matricies
        private void TraverseConnectionToNash(Models.Matrix matrix)
        {
            MessageBox.Show("The connection which starts at matrix '" + matrix.GetName() + "' will now be solved via the Best Responce Enumeration", matrix.GetName());
            solvingConnection = true;

            Queue<Models.Matrix> matrixQueue = new Queue<Models.Matrix>();
            matrixQueue.Enqueue(matrix);

            List<Models.Matrix> visitedMatricies = new List<Models.Matrix>();//track visited matricies to prevent cycles

            List<Models.Matrix> finalMatricies = new List<Models.Matrix>();

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
                    Models.Matrix nextMatrix = GetNextConnectedMatrix(currentMatrix, cell.X, cell.Y);

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
                foreach (Models.Matrix finalMatrix in finalMatricies)
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

        private List<Point> GetNashEquilibriaCells(Models.Matrix matrix)
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

        private Models.Matrix GetNextConnectedMatrix(Models.Matrix originMatrix, int row, int col)
        {
            foreach (Connection connection in existingConnections)
            {
                LinkedList<Node> link = connection.GetLinkOfCell(originMatrix, row, col);

                if (link != null && link.First != null && link.First.Next != null)
                {
                    return (Models.Matrix)link.First.Next.Value.GetModelReference();
                }
            }

            return null;
        }
    }
}