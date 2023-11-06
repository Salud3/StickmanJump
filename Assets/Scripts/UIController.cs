using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxslider;

    private void Awake()
    {
        _sfxslider.value = .5f;
        _musicSlider.value = .5f;
        _musicSlider = GameObject.Find("/SceneLoadManager/Canvas/MenuOptions/OptionsMenu/VolumenMusica").GetComponent<Slider>();
        _sfxslider = GameObject.Find("/SceneLoadManager/Canvas/MenuOptions/OptionsMenu/VolumenGeneral").GetComponent<Slider>();


    }
    private void Start()
    {
        _sfxslider.value = AudioManager.Instance.SavedSFXVolume;
        _musicSlider.value = AudioManager.Instance.SavedMusicVolume;
        gameObject.SetActive(false);

    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxslider.value);
    }
}
