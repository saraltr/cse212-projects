public static class ArraySelector
{
    public static void Run()
    {
        var l1 = new[] { 1, 2, 3, 4, 5 };
        var l2 = new[] { 2, 4, 6, 8, 10};
        var select = new[] { 1, 1, 1, 2, 2, 1, 2, 2, 2, 1};
        var intResult = ListSelector(l1, l2, select);
        Console.WriteLine("<int[]>{" + string.Join(", ", intResult) + "}"); // <int[]>{1, 2, 3, 2, 4, 4, 6, 8, 10, 5}
    }

    private static int[] ListSelector(int[] list1, int[] list2, int[] select)
    {
        List<int> result = new List<int>(); // to store the final numbers
        int index1 = 0;
        int index2 = 0;

        for (int i = 0; i < select.Length; i++)  // Go through each value
        {
            if (select[i] == 1) // if 1 take from list1
            {
                result.Add(list1[index1]); // add value from list1
                index1++; // move to the next element in list1
            }
            else if (select[i] == 2) // if 2 take from list2
            {
                result.Add(list2[index2]); // add value from list2
                index2++; // move to the next element in list2
            }
        }

        return result.ToArray(); // Convert list to array
    }
}