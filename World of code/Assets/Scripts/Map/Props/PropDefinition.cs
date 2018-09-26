using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Definition of a single prop
/// </summary>
[Serializable]
public class PropDefinition
{
    /// <summary>
    /// name of prop, set to zero once gameObject has been created to save memory
    /// </summary>
    public string name = null;
    /// <summary>
    /// path of prop obj, set to zero once gameObject has been created to save memory
    /// </summary>
    public string objPath = null;
    /// <summary>
    /// path of prop texture, set to zero once gameObject has been created to save memory
    /// </summary>
    public string texturePath = null;
    /// <summary>
    /// wether or not this prop has a collider
    /// </summary>
    public bool collision;

    /// <summary>
    /// gameObject for instantiating
    /// </summary>
    private GameObject prop;

    /// <summary>
    /// Basic constructor for prop definitions
    /// </summary>
    /// <param name="name">Name of the prop</param>
    /// <param name="objPath">Path </param>
    /// <param name="texturePath"></param>
    /// <param name="collision"></param>
    public PropDefinition(string name, string objPath, string texturePath, bool collision)
    {
        this.name = name;
        this.objPath = objPath;
        this.texturePath = texturePath;
        this.collision = collision;
    }

    public GameObject GetGameObject()
    {
        if (prop != null)
        {
            return prop;
        }

        prop = FileLoader.LoadOBJFile(Path.Combine(Application.streamingAssetsPath, objPath));
        prop.GetComponentInChildren<Renderer>().material.mainTexture = FileLoader.LoadTexture(Path.Combine(Application.streamingAssetsPath, texturePath));
        if (collision)
        {
            prop.AddComponent<Collider>().position = new Vector2Int(0, 5);
        }
        prop.name = name;

        return prop;
    }
}
