using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostControl : MonoBehaviour
{
    public bool boosted;

    void onTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("boosting");
            boosted = true;
        }
    }
}
