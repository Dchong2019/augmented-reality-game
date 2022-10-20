using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class Baseball : MonoBehaviour
{
    public AudioSource src;
    private Rigidbody rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        // get launch direction based off new ball location
        GetLaunchDirection();
        if (GameInfo.speed == "fast")
        {
            rb.AddForce((rb.rotation * Vector3.forward) * Random.Range(0.5f, 0.8f), ForceMode.Impulse);
        } else
        {
            rb.AddForce((rb.rotation * Vector3.forward) * Random.Range(0.3f, 0.5f), ForceMode.Impulse);
        }

        src.Play();
    }

    private void OnDestroy()
    {

    }

    private void GetLaunchDirection()
    {
        // get side of body throw is on, encoded as left = -1 right = 1
        int side = 0;
        if((GameInfo.leftSide && GameInfo.rightSide) || (!GameInfo.leftSide && !GameInfo.rightSide)) {
            while (side == 0)
            {
                side = Random.Range(-1, 2);
            }
        }
        else if (GameInfo.leftSide) {
            side = -1;
        }
        else if (GameInfo.rightSide)
        {
            side = 1;
        }

        // 90 degree angle throws
        if (GameInfo.gameType == "90")
        {
            rb.rotation *= Quaternion.Euler(0, -2 * side, 0); // use side int to mathematically change to correct sides
        }

        // 45 degree angle throws 
        else if (GameInfo.gameType == "45")
        {
            rb.rotation *= Quaternion.Euler(1, -2 * side, 0);

        }

        // 45 degree angle throws 
        else if (GameInfo.gameType == "30")
        {
            rb.rotation *= Quaternion.Euler(2, -2 * side, 0);

        }

        // random mode
        else
        {
            if (side == -1)
            {
                rb.rotation *= Quaternion.Euler(Random.Range(-2, 3), Random.Range(0, 3), 0);

            }
            else if (side == 1)
            {
                rb.rotation *= Quaternion.Euler(Random.Range(-2, 3), Random.Range(-2, 0), 0);

            }

        }

      
        
    }
}