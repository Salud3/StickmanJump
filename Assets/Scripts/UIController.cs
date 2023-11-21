using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxslider;
    public Animator FondoNegro;

    private void Awake()
    {
        FondoNegro = GameObject.Find("/Canvas/FondoNegro").GetComponent<Animator>();
        _musicSlider = GameObject.Find("/Canvas/MenuOptions/MusicLevel").GetComponent<Slider>();
        _sfxslider = GameObject.Find("/Canvas/MenuOptions/SFXLevel").GetComponent<Slider>();
        _sfxslider.value = .5f;
        _musicSlider.value = .5f;
        

    }
    private void Start()
    {
        
        GameObject a = _musicSlider.gameObject.transform.parent.gameObject;
        a.SetActive(false);

    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Reanude()
    {
        Time.timeScale = 1.0f;
    }
    public void Play()
    {
        FondoNegro.SetTrigger("Fadein");
        GameManager.Instance.Loadscene();
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
