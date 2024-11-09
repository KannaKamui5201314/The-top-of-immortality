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
            //Debug.Log("floor");
            spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            SetColorHSV();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //优化性能
            collision.gameObject.GetComponent<EnemyController>().enabled = true;
            collision.gameObject.transform.Find("Skeletal").gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //优化性能
            collision.gameObject.GetComponent<EnemyController>().enabled = false;
            collision.gameObject.transform.Find("Skeletal").gameObject.SetActive(false);
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
