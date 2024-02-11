using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelican : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnAlerted()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            anim.SetTrigger("alert");
        }
    }

    public void OnDive()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            anim.SetTrigger("dive");
        }
    }
}
