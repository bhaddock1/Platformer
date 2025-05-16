using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    private bool collided = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("is working");
        if (collision.gameObject.CompareTag("TurnPoint"))
        {
            collided = true;
        }
    }
    private void Move()
    {
        if (!collided)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 2f);
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * 4f);
        }
    }

}
