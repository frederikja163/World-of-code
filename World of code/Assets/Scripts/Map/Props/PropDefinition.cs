using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StreamingAssets;
using System.IO;

public class PropDefinition
{
    public string name = null;
    public string objPath = null;
    public string texturePath = null;
    public bool collision;

    private GameObject prop;

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

        prop = OBJLoader.LoadOBJFile(Path.Combine(Application.streamingAssetsPath, objPath));
        prop.GetComponentInChildren<Renderer>().material.mainTexture = TextureLoader.LoadTexture(Path.Combine(Application.streamingAssetsPath, texturePath));
        prop.transform.localScale = Vector3.one * 0.1f;
        if (collision)
        {
            prop.AddComponent<Collider>().position = new Vector2Int(0, 5);
        }
        prop.name = name;

        return prop;
    }
}
