using UnityEngine;

public class Tile : MonoBehaviour
{
    private Biome biome = null;
    [SerializeField]
    private new Renderer renderer;

    public void SetBiome(Biome biome)
    {
        this.biome = biome;

        if (biome == null)
        {
            return;
        }

        renderer.material.color = biome.color;
    }
}