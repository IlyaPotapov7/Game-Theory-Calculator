//Connection class that is composed of nodes
using System.Collections.Generic;
using static Game_Theory_Calculator.mainWindow;

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

            connectedComponents.Add(link);
        }

        link.AddLast(new Node(destinationModel));
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

    public void RemoveConnection(Model origin, int row, int col, Model destination)
    {
        LinkedList<Node> link = GetLinkOfCell(origin, row, col);

        if (link != null)
        {
            foreach (Node node in link)
            {

                if (node == link.First.Value)
                {
                    continue;
                }

                if (node.GetModelReference() == destination)
                {
                    link.Remove(node);
                    break;
                }
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