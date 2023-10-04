using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    [SerializeField] public Slider volumeSlider;
    private float musicVolume = 0;
    private AudioSource audioSource;
    [SerializeField] public GameObject objectMusic;
    // Start is called before the first frame update
    void Start()
    {
        objectMusic = GameObject.FindWithTag("GameMusic");
        audioSource = objectMusic.GetComponent<AudioSource>();
        musicVolume = PlayerPrefs.GetFloat("volume");
        audioSource.volume = musicVolume;
        volumeSlider.value = musicVolume;

    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = musicVolume;
        PlayerPrefs.SetFloat("volume", musicVolume);
    }
    public void VolumeUpdater(float volume)
    {
        musicVolume = volume;
    }
    public void MusicReset()
    {
        PlayerPrefs.DeleteKey("volume");
        audioSource.volume = 1;
        volumeSlider.value = 1;
    }
}
