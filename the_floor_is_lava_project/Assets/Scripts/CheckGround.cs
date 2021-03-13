using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public PlayerController player;
    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Ground"))
        {
            player.SetOnGround(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Ground"))
        {
            player.SetOnGround(false);
        }
    }
}
