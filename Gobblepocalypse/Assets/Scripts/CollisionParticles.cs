using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollisionParticles : MonoBehaviour
{
    private ParticleSystem ps;
    private GameObject player;
    private PlayerScript playerScript;
    private Rigidbody2D playerRb;

    public int particleCount;
    public int multiplier;

    private Color particleColour;

    // Start is called before the first frame update
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    public void PlayParticles(Collision2D c)
    {
        SetParticleColour(c);   //Sets particle colour based on thing collided with
        CalculateParticles();   //Calculate the particles
        ps.Play();  //Play the particles on collision
        Invoke("SelfDestruct", 3);  //destroy after 3 seconds
    }

    public void CalculateParticles()
    {
        ParticleSystem.EmissionModule em = ps.emission;     //references the particle system's emission module

        //int mouseX = (int)System.Math.Abs(playerScript.mouseDistance.x * playerScript.shootPower);      //make the value positive and cast as int
        //int mouseY = (int)System.Math.Abs(playerScript.mouseDistance.y * playerScript.shootPower);
        //particleCount = (mouseX + mouseY) / divisor;    //Calculate a number of particles based off the values given

        int mouseX = (int)System.Math.Abs(playerRb.velocity.x);      //make the value positive and cast as int
        int mouseY = (int)System.Math.Abs(playerRb.velocity.y);
        particleCount = (mouseX + mouseY) / 2 * multiplier;    //Calculate a number of particles based off the values given

        em.burstCount = particleCount;      //Set the particle burst amount
    }

    public void SetParticleColour(Collision2D c)
    {

        if (c.gameObject.GetComponent<Tilemap>() != null)
        {
            particleColour = c.gameObject.GetComponent<Tilemap>().color;    //Detect the colour of the surface collided with
        }

        ParticleSystem.MainModule mm = ps.main;     //Get the main module of the particle system
        mm.startColor = particleColour;       //Assign its colour to the surface colour
    }

    public void SelfDestruct()
    { 
        Destroy(gameObject);
    }
}
