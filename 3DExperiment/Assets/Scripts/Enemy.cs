using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioSource EatGhostSound;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Enemy")
        {
            EatGhostSound.Play();
            Destroy(hit.gameObject);
            Debug.Log("Enemy Defeated!");
        }
    }
}