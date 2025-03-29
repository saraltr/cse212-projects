using System.Collections;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        // TODO Problem 1
        Node newNode = new(value);

        // If the list is empty, then point both head and tail to the new node.
        if (_tail is null) {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Prev = _tail; // set the head of the new node to the current tail
            _tail.Next = newNode; // set the tail of the current tail to the new node
            _tail = newNode; // set the tail equal to the new node
        }

    }


    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item in it, then set head and tail 
        // to null resulting in an empty list.  This condition will also
        // cover an empty list.  Its okay to set to null again.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the head
        // will be affected.
        else if (_head is not null)
        {
            _head.Next!.Prev = null; // Disconnect the second node from the first node
            _head = _head.Next; // Update the head to point to the second node
        }
    }


    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {
        // TODO Problem 2
        // if the list has only one item in it then set head and tail as null
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // if the list has more than one item in it
        // only change the tail
        else if (_tail is not null)
        {
            _tail.Prev!.Next = null; // sets the tail of the second node to the last to null
            _tail = _tail.Prev; // update the tail to point to the second to last node
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        // Search for the node that matches 'value' by starting at the 
        // head of the list.
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the location of 'value' is at the end of the list,
                // then we can call insert_tail to add 'new_value'
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                // For any other location of 'value', need to create a 
                // new node and reconnect the links to insert.
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr; // Connect new node to the node containing 'value'
                    newNode.Next = curr.Next; // Connect new node to the node after 'value'
                    curr.Next!.Prev = newNode; // Connect node after 'value' to the new node
                    curr.Next = newNode; // Connect the node containing 'value' to the new node
                }

                return; // We can exit the function after we insert
            }

            curr = curr.Next; // Go to the next node to search for 'value'
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value)
    {
        // TODO Problem 3
        // search for the node (starting at the head) that contains the value
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // if the value corresponds to the head, calls the RemoveHead function
                if (curr == _head)
                {
                    RemoveHead();
                }

                // if the value corresponds to the tail, call the RemoTail function
                else if (curr == _tail)
                {
                    RemoveTail();
                }
                // if the value is at the middle of the linked list
                else
                {
                    // checks if next is not null
                    if (curr.Next != null)
                        //set the prev of the node after current to the node before current
                        curr.Next.Prev = curr.Prev;

                    // checks if prev is not null
                    if (curr.Prev != null)
                        // set the next of the node before current to the node after current 
                        curr.Prev.Next = curr.Next;
                }

                return; // exit the function after we removed the node
            }

            curr = curr.Next; // go to the next node to search for 'value'
        }

    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        // TODO Problem 4
        // search for all nodes that are equal to oldValue
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == oldValue)
            {
                // replace the value in those nodes with newValue
                curr.Data = newValue;
            }

            curr = curr.Next; // move to the next node
        }

    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head; // Start at the beginning since this is a forward iteration.
        while (curr is not null)
        {
            yield return curr.Data; // Provide (yield) each item to the user
            curr = curr.Next; // Go forward in the linked list
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    public IEnumerable Reverse()
    {
        // TODO Problem 5
        // start at the end to create the reverse iteration
        var curr = _tail;
        while (curr is not null)
        {
            yield return curr.Data; // provide (yield) each item to the user
            curr = curr.Prev; // goes backwards in the linked list
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods {
    public static string AsString(this IEnumerable array) {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}