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

    /// <summary>
    /// Get the GameObject of this prop
    /// </summary>
    /// <returns>Gameobject of this prop</returns>
    public GameObject GetGameObject()
    {
        if (prop != null)
        {
            return prop;
        }

        CreateGameObject();

        return prop;
    }

    /// <summary>
    /// Create a GameObject if none exists so we can instantiate it
    /// </summary>
    private void CreateGameObject()
    {
        if (objPath != null)
        {
            prop = FileLoader.LoadOBJFile(Path.Combine(Application.streamingAssetsPath, objPath));
        }
        if (texturePath != null)
        {
            prop.GetComponentInChildren<Renderer>().material.mainTexture = FileLoader.LoadTexture(Path.Combine(Application.streamingAssetsPath, texturePath));
        }
        if (collision)
        {
            prop.AddComponent<Collider>();
        }
        prop.name = name;

        SetNull();
    }

    /// <summary>
    /// Set all variables to null so we can save memory
    /// </summary>
    private void SetNull()
    {
        objPath = null;
        texturePath = null;
    }
}
