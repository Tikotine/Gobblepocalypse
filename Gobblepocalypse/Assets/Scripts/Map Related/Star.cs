using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StarNumber
{ 
    LEVEL1_1,
    LEVEL1_2, 
    LEVEL1_3,
    LEVEL2_1,
    LEVEL2_2,
    LEVEL2_3,
    LEVEL3_1,
    LEVEL3_2,
    LEVEL3_3
}

public class Star : MonoBehaviour
{
    [Header("References")]
    private SpriteRenderer sr;
    [SerializeField] private StarNumber starNumber;
    private StarManager sm;

    [Header("Sprites")]
    public Sprite[] starSprites;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        InitializeSprites();
        sm = GameObject.FindWithTag("StarManager").GetComponent<StarManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CollectStar();
            Destroy(gameObject);
        }
    }

    public void CollectStar()
    {
        switch (starNumber)
        {
            case StarNumber.LEVEL1_1:
                sm.starsCollected[0] = true;

                break;

            case StarNumber.LEVEL1_2:
                sm.starsCollected[1] = true;

                break;

            case StarNumber.LEVEL1_3:
                sm.starsCollected[2] = true;

                break;

            case StarNumber.LEVEL2_1:
                sm.starsCollected[3] = true;

                break;

            case StarNumber.LEVEL2_2:
                sm.starsCollected[4] = true;

                break;

            case StarNumber.LEVEL2_3:
                sm.starsCollected[5] = true;

                break;

            case StarNumber.LEVEL3_1:
                sm.starsCollected[6] = true;

                break;

            case StarNumber.LEVEL3_2:
                sm.starsCollected[7] = true;

                break;

            case StarNumber.LEVEL3_3:
                sm.starsCollected[8] = true;

                break;
        }
    }

    public void InitializeSprites()
    {
        switch (starNumber)
        { 
            case StarNumber.LEVEL1_1:
                sr.sprite = starSprites[0];

                break;

            case StarNumber.LEVEL1_2:
                sr.sprite = starSprites[1];

                break;

            case StarNumber.LEVEL1_3:
                sr.sprite = starSprites[2];

                break;

            case StarNumber.LEVEL2_1:
                sr.sprite = starSprites[3];

                break;

            case StarNumber.LEVEL2_2:
                sr.sprite = starSprites[4];

                break;

            case StarNumber.LEVEL2_3:
                sr.sprite = starSprites[5];

                break;

            case StarNumber.LEVEL3_1:
                sr.sprite = starSprites[6];

                break;

            case StarNumber.LEVEL3_2:
                sr.sprite = starSprites[7];

                break;

            case StarNumber.LEVEL3_3:
                sr.sprite = starSprites[8];

                break;
        }
    }
}
