using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour
{
    public GameObject pitchController;
    public AudioSource catchPing;
    



    // Start is called before the first frame update
    void Start()
    {
        catchPing = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void OnCollisionEnter(Collision collision)
    {

        pitchController.GetComponent<PitchController>().incrementScoreVisually();
        catchPing.Play();
        Destroy(collision.gameObject);
    
    }
}
