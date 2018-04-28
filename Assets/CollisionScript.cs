using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CollisionScript : MonoBehaviour {
    public AudioSource collisionSound;
       
	// Use this for initialization
	void Start () {
        collisionSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("drumstick") == true)
        {
            collisionSound.Play();
                                    
        }

    }

    private void OnCollisionExit(Collision target)
    {
        
    }
}
