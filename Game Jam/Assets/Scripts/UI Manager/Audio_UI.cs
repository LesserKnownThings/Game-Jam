using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_UI : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playClip()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
