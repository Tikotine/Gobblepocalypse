using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPlatforms : MonoBehaviour
{
    //Colour transition
    SpriteRenderer spriteRenderer;
    public Material material;
    public Color[] colors;
    private int currentColorIndex = 0;
    private int targetColorIndex = 1;
    public float targetPoint;
    public float time;

    //Collider
    public BoxCollider2D col;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Transition();
    }

    void Transition()
    {
        if (targetPoint < 1)
        {
            col.enabled = true; //Enable hitbox
            targetPoint += Time.deltaTime / time;   //Increment targetpoint by time passed
            spriteRenderer.color = Color.Lerp(colors[currentColorIndex], colors[targetColorIndex], targetPoint);    //Colour change
        }

        if (targetPoint >= 1)
        { 
            col.enabled = false;    //Disbale Hitbox
        }

        /*if (targetPoint >= 1f)
        {
            targetPoint = 0f;
            currentColorIndex = targetColorIndex;
            targetColorIndex++;

            if (targetColorIndex == colors.Length)
            {
                targetColorIndex = 0;
            }
        }*/

    }
}
