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
            case 1: renderer.material.color = new Color(233f / 255f, 221f / 255f, 199f / 255f); break;
            case 2: renderer.material.color = new Color(196f / 255f, 212f / 255f, 170f / 255f); break;
            case 3: renderer.material.color = new Color(169f / 255f, 204f / 255f, 164f / 255f); break;
            case 4: renderer.material.color = new Color(156f / 255f, 187f / 255f, 169f / 255f); break;
            case 5: renderer.material.color = new Color(228f / 255f, 232f / 255f, 202f / 255f); break;
            case 6: renderer.material.color = new Color(196f / 255f, 212f / 255f, 170f / 255f); break;
            case 7: renderer.material.color = new Color(180f / 255f, 201f / 255f, 169f / 255f); break;
            case 8: renderer.material.color = new Color(164f / 255f, 196f / 255f, 168f / 255f); break;
            case 9: renderer.material.color = new Color(228f / 255f, 232f / 255f, 202f / 255f); break;
            case 10: renderer.material.color = new Color(196f / 255f, 204f / 255f, 187f / 255f); break;
            case 11: renderer.material.color = new Color(204f / 255f, 212f / 255f, 187f / 255f); break;
            case 12: renderer.material.color = new Color(153f / 255f, 153f / 255f, 153f / 255f); break;
            case 13: renderer.material.color = new Color(187f / 255f, 187f / 255f, 187f / 255f); break;
            case 14: renderer.material.color = new Color(221f / 255f, 221f / 255f, 187f / 255f); break;
            case 15: renderer.material.color = new Color(248f / 255f, 248f / 255f, 248f / 255f); break;
        }
        this.biome = biome;
    }
}