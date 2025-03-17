public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        var numbers = new List<double>(length); // creates the list and sets its capacity to length

        for(int i = 1; i <= length; i++) // loops form 1 to lenght and increments i while its smaller or equal to lenght
        {
            numbers.Add(number * i); // multiplicates the nb by the loop index and adds the result to the list
        }

        return numbers.ToArray(); // returns the list as an array
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        int capacity = data.Count; // gets the total capacity of the list
        int index = capacity - amount;  // calaculates the starting index for extracting the amout of numbers

        var numbers = data.GetRange(index, amount); // stores the numbers from the starting index until the last number of the list into a variable 
        data.RemoveRange(index, amount); // removes them from the back
        data.InsertRange(0, numbers); // adds them at the front

    }
}
