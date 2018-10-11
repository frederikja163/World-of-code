using UnityEngine;

public class Tile : MonoBehaviour
{
    private float flora;
    private Biome biome = null;
    [SerializeField]
    private new Renderer renderer;

    public void SetBiome(Biome biome, float spawnRate)
    {
        this.biome = biome;

        if (biome == null)
        {
            return;
        }

        float rate = 0;
        for (int i = 0; i < biome.tiles.Length; i++)
        {
            rate += biome.tiles[i].spawnRate;
            if (spawnRate <= rate)
            {
                renderer.material.mainTexture = biome.tiles[i].texture;
                return;
            }
        }
    }

    public void SetFlora(float flora)
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        this.flora = flora;
        if (biome == null)
        {
            return;
        }
        if (biome.flora == null)
        {
            return;
        }

        float f = 0;
        for (int i = 0; i < biome.flora.Length; i++)
        {
            f += biome.flora[i].spawnRate;
            if (f >= flora)
            {
                GameObject go = AssetManager<PropDefinition>.FindObj(x => x.name == biome.flora[i].prop).GetGameObject();
                SetProp(go);
                return;
            }
            f += biome.flora[i].spawnRate;
        }
    }

    public void SetProp(GameObject go)
    {
        go = Instantiate(go, transform.position, Quaternion.identity);
        go.transform.SetParent(transform);

        Collider col = go.GetComponent<Collider>();
        if (col != null)
        {
            col.position = Map.WorldToMapPosition(transform.position);
        }
    }
}