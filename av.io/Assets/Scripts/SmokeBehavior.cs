using System.Collections;
using UnityEngine;

public class SmokeBehavior : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f;

    private SpriteRenderer spriteRenderer;

    private Vector3 initialScale;
    private Color initialColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        initialScale = transform.localScale;
        initialColor = spriteRenderer.color;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f; 
        while(timer < lifetime)
        {
            timer += Time.deltaTime;
            float progress = timer / lifetime;

            //transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, progress);

            Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
            spriteRenderer.color = Color.Lerp(initialColor, targetColor, progress);

            yield return null;
        }

        Destroy(gameObject, lifetime);
    }
}
