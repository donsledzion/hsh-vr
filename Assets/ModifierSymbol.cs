using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierSymbol : MonoBehaviour
{
    [SerializeField] List<Renderer> renderers;

    [SerializeField] Color defaultColor, highlightedColor;

    void SetColor(Color color)
    {
        foreach (Renderer renderer in renderers)
            renderer.material.color = color;
    }

    public void Highlight()
    {
        SetColor(highlightedColor);
    }

    public void RestoreColor()
    {
        SetColor(defaultColor);
    }
}
