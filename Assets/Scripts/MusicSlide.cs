using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSlide : MonoBehaviour
{
    private AudioSource SlideSound;

    // Start is called before the first frame update
    void Start()
    {
        SlideSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SlideSound.Play();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            SlideSound.Stop();
        }
    }
}
