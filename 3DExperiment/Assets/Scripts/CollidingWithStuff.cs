using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingWithStuff : MonoBehaviour
{ 
    [SerializeField] private int playerHealth = 3;
    [SerializeField] private Transform ghost; 
    public AudioSource DamageSound;
    public AudioSource HealUpSound;
    public AudioSource LockedSound;
    public AudioSource UnlockableSound;
    public AudioSource UnlockedSound;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Hazard" && playerHealth>0)
        {
            DamageSound.Play();
            playerHealth -= 1;
            Debug.Log("Ouch! I recieved damage, better find another path..         Player Health: "+playerHealth);
        }
        
        if(hit.gameObject.tag == "Locked")
        {
            LockedSound.Play();
            Debug.Log("Can't pass through this ghost, gotta look for another path");
        }
        if(hit.gameObject.tag == "Unlockable")
        {
            if(GameObject.Find("Player").GetComponent<CollectCoins>().money<13)
            {
                UnlockableSound.Play();
                Debug.Log("I need to collect all 13 coins to unlock this path..");
            }

            if(GameObject.Find("Player").GetComponent<CollectCoins>().money==13)   
            {
                UnlockedSound.Play();   
                ghost.Translate(0,4*Time.deltaTime,0);
            } 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Heal") && playerHealth<3)
        {
            playerHealth += 1;
            HealUpSound.Play();  
            Destroy(other.gameObject);
            Debug.Log("Nice! I got healed up..         Player Health: "+playerHealth);
        }
        if(other.gameObject.CompareTag("Heal") && playerHealth>=3)
            Debug.Log("Maximum health");
    }
    
    //private void OnCollisionStay(Collision other)
    //{
        //float shrinkRate=0.1f *Time.deltaTime;
        //if(other.gameObject.tag != "Ground")
        //{
            //transform.localScale -= new Vector3(shrinkRate, shrinkRate, shrinkRate);
        //}
    //}
}
