using UnityEngine;

public class Tile : MonoBehaviour
{
    private int biome = -1;
    [SerializeField]
    private new Renderer renderer;

    public void SetBiome(int biome)
    {
        switch (biome)
        {
            case 1:
                renderer.material.color = new Color(0f, 0.5f, 0f);
                break;
            case 2:
                renderer.material.color = new Color(0.1f, 0.5f, 0.1f);
                break;
            case 3:
            default:
                renderer.material.color = new Color(0.2f, 0.5f, 0.2f);
                break;
        }
        this.biome = biome;
    }
}