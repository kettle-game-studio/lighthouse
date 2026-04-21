using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip[] musics;
    public AudioSource source;
    private int idx = 0;


    void Update()
    {
        if (!source.isPlaying)
        {
            source.clip = musics[idx];
            idx = (idx + 1) % musics.Length;
            source.Play();
        }
    }
}
