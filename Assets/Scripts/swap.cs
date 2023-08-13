// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

using System;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        int[] arr = { 10, 2, 4, 5 };
        int size = arr.Length;

        int target = 5;
        int index = -1;

        for (int i = 0; i < size; i++)
        {
            if (arr[i] == target)
            {
                index = i;
            }
        }
        Console.WriteLine(index);
    }
}
