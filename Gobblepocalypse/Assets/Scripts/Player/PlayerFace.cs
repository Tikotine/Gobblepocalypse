using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//jme
public class PlayerFace : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] faces; //0 slp, 1 shoot, 2 atk, 3 charging?, 4 default

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void changeFace(int a)
    {
        sr.sprite = faces[a];
    }
}
