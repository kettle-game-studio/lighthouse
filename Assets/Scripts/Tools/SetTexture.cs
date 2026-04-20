using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SetTexture : MonoBehaviour
{
    public Texture texture;
    Renderer targetRenderer;
    Texture lastTexture = null;

    void Start()
    {
        targetRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (texture != lastTexture)
        {
            targetRenderer.material.mainTexture = texture;
            lastTexture = texture;
        }
    }
}
