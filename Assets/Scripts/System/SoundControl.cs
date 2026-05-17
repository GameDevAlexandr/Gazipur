using UnityEngine;
using UnityEngine.Audio;

public class SoundControl : MonoBehaviour
{
     [ SerializeField] private AudioMixerGroup mixer;
    public void Start()
    {
        //mixer = Resources.Load<AudioMixerGroup>("AudioMixer");
    }
    public void ChangeMusicVolume(float value)
    {
        Debug.Log("ChangeMusicValue " + value);
        mixer.audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }
    public void ChangeSoundVolume(float value)
    {
        mixer.audioMixer.SetFloat("SoundVolume", Mathf.Log10(value) * 20);
    }
    public void Mute(bool isMute)
    {
        if (isMute)
        {
            mixer.audioMixer.SetFloat("MasterVolume", -80);
        }
        else
        {
            mixer.audioMixer.SetFloat("MasterVolume", 0);
        }
    }
}
