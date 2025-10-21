using UnityEngine;

public class Cube : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Recolor(Color newColor)
    {
        meshRenderer.material.color = newColor;
    }
}
