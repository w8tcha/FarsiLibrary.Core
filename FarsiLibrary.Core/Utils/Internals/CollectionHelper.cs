namespace FarsiLibrary.Core.Utils.Internals;

using System;
using System.Collections.Generic;

public static class CollectionHelper
{
    public static void ForEach<T, U>(this IDictionary<T, U> dictionary, Action<KeyValuePair<T, U>> action)
    {
        if (dictionary == null || dictionary.Count == 0)
        {
            return;
        }

        foreach (var item in dictionary)
        {
            action(item);
        }
    }
}