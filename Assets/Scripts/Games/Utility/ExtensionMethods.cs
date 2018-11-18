using System;
using System.Collections.Generic;

public static class ExtensionMethods
{
    private static Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static T Get<T>(this IList<T> list, int index)
    {
        return list[index]; 
    }

    public static void Set<T>(this IList<T> list, int index, T valueToInsert)
    {
        list[index] = valueToInsert;
    }

    public static IEnumerable<T> GetValues<T>()
    {
        return (IEnumerable<T>)Enum.GetValues(typeof(T));
    }
}


