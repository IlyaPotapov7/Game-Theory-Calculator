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