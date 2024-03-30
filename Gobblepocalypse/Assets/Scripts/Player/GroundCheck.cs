using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerScript player;

    private void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }

    private void FixedUpdate()
    {
        transform.position = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.pf.changeFace(4);
    }
}
