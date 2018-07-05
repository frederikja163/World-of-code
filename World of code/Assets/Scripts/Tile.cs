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
                renderer.material.color = Color.white;
                break;
            case 2:
                renderer.material.color = Color.green;
                break;
            case 3:
                renderer.material.color = Color.blue;
                break;
        }
    }
}