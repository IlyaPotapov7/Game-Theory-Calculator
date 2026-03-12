using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Theory_Calculator.Models
{
    public class Connection
    {
        private List<LinkedList<Node>> connectedComponents;
        private int connectionID;
        Model rootModel;

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

        public Model GetRootModel()
        {
            return rootModel;
        }

        public void SetRootModel(Model rootModel)
        {
            this.rootModel = rootModel;
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
    }

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
}
