using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T SelectRandomElement<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("The list is empty or null.");
        }

        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}