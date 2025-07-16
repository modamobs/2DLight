using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightCollader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Constructor logic if needed
    SpriteRenderer spriteRenderer;
    public Light2D light2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Light"))
        {

            light2D.shadowsEnabled = true; // Disable shadows when player exits the collider
            spriteRenderer.enabled = false; // Disable the sprite renderer of the light

        }
        

    }
    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Light"))
        {
            spriteRenderer.enabled = true; // Disable the sprite renderer of the light
            light2D.shadowsEnabled = true; // Disable shadows when player exits the collider
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            if (light2D != null)
            {
                light2D.shadowsEnabled = false;
                Debug.Log("Light2D shadowsEnabled set to false");
            }
            else
            {
                Debug.LogWarning("light2D is not assigned!");
            }
        }
    }
}
