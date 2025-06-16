public static class MaximumDifference
{
    public static (int, int, int) DetermineDifference(int[] nums)
    {
        if (nums.Length < 2)
        {
            return (-1, -1, -1); // Not enough elements to find a difference
        }

        int maxDifference = -1;
        int smallestIndex = 0;
        int largestIndex = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            if (i + 1 == nums.Length)
            {
                // We are at the end of the list, so we should only check numbers[i]
                if (nums[i] > nums[smallestIndex])
                {
                    maxDifference = nums[i] - nums[smallestIndex];
                }

                continue;
            }

            // For each other situation, we can compare numbers[i] with numbers[i + 1]

            // First, let's check if the i + 1 is a smaller number, if so we can set that as the new smallestIndex and continue
            if (i + 1 < nums.Length && nums[i + 1] < nums[smallestIndex])
            {
                smallestIndex = i + 1;
                continue;
            }

            // Now, we can assume numbers[i + 1] is larger then numbers[i], but we have to check if the difference is larger than what we previously had
            if (nums[i + 1] - nums[smallestIndex] > maxDifference)
            {
                maxDifference = nums[i + 1] - nums[smallestIndex];
                largestIndex = i + 1;
            }
        }


        return (maxDifference, smallestIndex, largestIndex);
    }

    public static (int, int, int) DetermineDifference2(int[] nums)
    {
        if (nums.Length < 2)
        {
            return (-1, -1, -1); // Not enough elements to find a difference
        }

        int maxDifference = -1;
        int lowIndex = 0;
        int maxIndex = 0;

        int currentLowIndex = 0;
        int currentLowValue = nums[0];

        for (int i = 1; i < nums.Length; i++)
        {
            int currentDiff = nums[i] - currentLowValue;

            if (currentDiff > maxDifference && currentDiff > 0)
            {
                maxDifference = nums[i] - currentLowValue;
                lowIndex = currentLowIndex;
                maxIndex = i;
            }

            if (nums[i] < currentLowValue)
            {
                currentLowValue = nums[i];
                currentLowIndex = i;
            }
        }

        return (maxDifference, lowIndex, maxIndex);
    }
}