using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ColourPlatforms : MonoBehaviour
{
    //Colour transition
    //SpriteRenderer spriteRenderer;
    Tilemap tilemap;
    public Material material;
    public Color[] colors;
    private int currentColorIndex = 0;
    private int targetColorIndex = 1;
    public float targetPoint;
    public float time;

    //Collider
    public TilemapCollider2D col;

    //Bar
    public Slider bar;

    private void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        tilemap = gameObject.GetComponent<Tilemap>();
        col = gameObject.GetComponent<TilemapCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transition();
        bar.value = 1 - targetPoint;
    }

    void Transition()
    {
        if (targetPoint < 1)
        {
            col.enabled = true; //Enable hitbox
            targetPoint += Time.deltaTime / time;   //Increment targetpoint by time passed
            tilemap.color = Color.Lerp(colors[currentColorIndex], colors[targetColorIndex], targetPoint);    //Colour change
        }

        if (targetPoint >= 1)
        { 
            col.enabled = false;    //Disbale Hitbox
            targetPoint = 1;
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
