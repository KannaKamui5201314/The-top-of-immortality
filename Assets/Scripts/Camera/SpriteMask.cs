using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMask : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            Debug.Log("floor");
            spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            SetColorHSV();
        }
    }

    void SetColorHSV()
    {
        Color currentColor = spriteRenderer.color;
        float h, s, v;
        Color.RGBToHSV(currentColor, out h, out s, out v);
        h = (30f * Global.Boundary) % 360f / 360f;
        Color newColor = Color.HSVToRGB(h, s, v);
        spriteRenderer.color = newColor;
    }
}
