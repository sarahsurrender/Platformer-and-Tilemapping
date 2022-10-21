using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMangerScript : MonoBehaviour
{
    public static AudioClip yaySound;
    static AudioSource audioSrc;


    // Start is called before the first frame update
    void Start()
    {
        yaySound = Resources.Load<AudioClip>("yay");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "yay":
                audioSrc.PlayOneShot(yaySound);
                break;
        }
    }
}
