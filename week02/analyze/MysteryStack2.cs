public static class MysteryStack2 {
    private static bool IsFloat(string text) { // true or false
        return float.TryParse(text, out _); // text into float?
    }

    public static float Run(string text) {
        var stack = new Stack<float>();
        foreach (var item in text.Split(' ')) { // devide by spaces?
            if (item == "+" || item == "-" || item == "*" || item == "/") { // they would be symbols
                if (stack.Count < 2)
                    throw new ApplicationException("Invalid Case 1!"); // invalid if it is bigger than 2 

                var op2 = stack.Pop(); // removes the last item added to the stack
                var op1 = stack.Pop();
                float res;
                if (item == "+") {
                    res = op1 + op2; // if + removes 2 items from the stack and adds them togheter
                }
                else if (item == "-") {
                    res = op1 - op2; // if - removes then - the 2 items
                }
                else if (item == "*") {
                    res = op1 * op2; // if * removes the 2 items then multiply them
                }
                else {
                    if (op2 == 0) // if 0
                        throw new ApplicationException("Invalid Case 2!");

                    res = op1 / op2; // can only devide so keeps the other nb
                }

                stack.Push(res); // push the calculations to the stack
            }
            else if (IsFloat(item))
            { // if the item is a nb and not a symbol and  checks if item is a valid float (but it's still a string)
                stack.Push(float.Parse(item)); // Converts the string into a float and pushes it
            }
            else if (item == "") {
            }
            else {
                throw new ApplicationException("Invalid Case 3!");
            }
        }

        if (stack.Count != 1)
            throw new ApplicationException("Invalid Case 4!");

        return stack.Pop();
    }
}