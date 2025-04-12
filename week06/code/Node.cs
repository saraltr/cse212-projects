public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1

        // only allow unique values to be added to the tree
        if(value == Data)
        {
            return;
        }
        
        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // TODO Start Problem 2

        // check if the current node contains the value
        if (value == Data)
        {
            return true;
        }
        else if (value < Data)
        {
            // search left subtree
            if (Left != null)
            {
                return Left.Contains(value); // recursively search left
            }
            return false; // left is null, value not found
        }
        else
        {
            // search right subtree
            if (Right != null)
            {
                return Right.Contains(value); // recursively search right
            }
            return false; // right is null, value not found
        }
    }

    public int GetHeight()
    {
        // TODO Start Problem 4
        int leftHeight = 0;
        int rightHeight = 0;

        // checks if the left node is not null and calculates its height
        if (Left != null)
        {
            leftHeight = Left.GetHeight();
        }

        // checks if the right node is not null and calculates its height
        if (Right != null)
        {
            rightHeight = Right.GetHeight();
        }

        // the height of the current node is 1 plus the maximum of left and right subtree heights
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}