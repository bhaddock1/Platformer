using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
   private void OnCollisionEnter(Collision collision)
   {
	    if (collision.gameObject.CompareTag("Platform") && !collidedPlatforms.Contains(collision.collider))
        {
            collidedPlatforms.Add(collision.collider); // Mark the platform as collided

            Scorekeeper.Instance.AddToScore();
        }
   }
	
}
