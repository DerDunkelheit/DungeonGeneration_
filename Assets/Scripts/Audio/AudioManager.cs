using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource[] levelMusics = null;
    [SerializeField] AudioSource[] SFX = null;

    AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void playMainMusic(int index)
    {
        levelMusics[index].loop = true;
        levelMusics[index].Play();
    }

    public void PlaySfx(int numberOfSfx)
    {
        SFX[numberOfSfx].Stop();
        SFX[numberOfSfx].Play();
    }



}
