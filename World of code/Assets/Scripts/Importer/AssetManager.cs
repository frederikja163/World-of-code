using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages assets for type t
/// </summary>
/// <typeparam name="t">Type of asset to mannage</typeparam>
public static class AssetManager<t>
{
    /// <summary>
    /// List of objects, so we can find them later on
    /// </summary>
    private static List<t> objs = new List<t>();

    /// <summary>
    /// Adds objects to the list of managed objects
    /// </summary>
    /// <param name="objs">Objects to add to the list</param>
    public static void AddObjs(params t[] objs)
    {
        AssetManager<t>.objs.AddRange(objs);
    }

    /// <summary>
    /// Find objects based on given predicate
    /// </summary>
    /// <param name="predicate">Predicate used for searching for objects</param>
    /// <returns>Object found using the predicate</returns>
    public static t FindObj(Predicate<t> predicate)
    {
        return objs.Find(predicate);
    }
	
}
