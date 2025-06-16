namespace LeetCode.PascalsTriangle;

public static class PascalsTriangle
{
    public static IList<IList<int>> Generate(int numRows)
    {
        if (numRows == 1)
        {
            return [[1]];
        }
        var list = new int[numRows][];
        list[0] = [1];
    
        // The number of rows we need
        for(int i = 1; i < numRows; i++){
            var oldList = list[i - 1];
            var newList = new int[i + 1];

            for (int j = 0; j < i + 1; j++)
            {
                // If we are at the first or last element, it is always 1
                if (j == 0 || j == i)
                {
                    newList[j] = 1;
                }
                else
                {
                    // Otherwise, it is the sum of the two elements above it
                    newList[j] = oldList[j - 1] + oldList[j];
                }
            }
        
            list[i] = newList;
        }
    
        return list.ToList<IList<int>>();
    }
}