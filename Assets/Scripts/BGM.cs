using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private AudioSource m_AudioSource;

    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip loop;

    private bool CR_running = false;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (m_AudioSource.isPlaying && m_AudioSource.clip != loop && !CR_running) StartCoroutine(PlayBGM());
        else if (!m_AudioSource.isPlaying) m_AudioSource.clip = intro;
    }

    private IEnumerator PlayBGM()
    {
        CR_running = true;

        m_AudioSource.loop = false;
        print("Playing intro...");
        yield return new WaitForSeconds(intro.length);
        print("Playing loop.");
        m_AudioSource.Stop();
        m_AudioSource.clip = loop;
        m_AudioSource.loop = true;
        m_AudioSource.Play();

        CR_running = false;
    }
}
