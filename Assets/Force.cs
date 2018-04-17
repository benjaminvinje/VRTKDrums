using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour {

    public float force;

    void Start()
    {
       
    }

    void FixedUpdate()
    {
       
    }

    void OnCollisionEnter(Collision target)
    {
               
            // If the object we hit is the enemy
            if (target.gameObject.tag.Equals("drum") == true)
            {
                // Calculate Angle Between the collision point and the player
                Vector3 dir = target.contacts[0].point - transform.position;
                // We then get the opposite (-Vector3) and normalize it
                dir = -dir.normalized;
                // And finally we add force in the direction of dir and multiply it by force. 
                // This will push back the player
                GetComponent<Rigidbody>().AddForce(dir * force);
            }
            }
}
