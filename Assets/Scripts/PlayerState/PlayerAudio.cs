using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource doubleJumpSound;
    [SerializeField] private AudioSource footstepsSound;
    [SerializeField] private AudioClip[] footstepsClips = new AudioClip[10];
    public void PlayJumpSound()
    {
        jumpSound.Play();
    }

    public void PlayDoubleJumpSound()
    {
        doubleJumpSound.Play();
    }

    public void PlayFootsteps()
    {
        if (!footstepsSound.isPlaying) {
            footstepsSound.clip = footstepsClips[Random.Range(0, footstepsClips.Length)];
            footstepsSound.Play();
        }
    }

    public void StopPlayingFootsteps() { footstepsSound.Stop(); }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
