using UnityEngine;

public class Cube : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Recolor(Color newColor)
    {
        meshRenderer.material.color = newColor;
    }
}
