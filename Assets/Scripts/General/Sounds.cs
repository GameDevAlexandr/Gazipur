
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using Zenject;
using static EnumData;

public class Sounds : MonoBehaviour
{
    public AudioSource PlayerSource => _playerSource;
    [SerializeField] private AudioMixerGroup mixer;

    [SerializeField] private AudioSource _playerSource;    
    [SerializeField] private PlayerSoundData[] _playerSounds;
    [SerializeField] private AudioSource _uiSource;
    [SerializeField] private UISoundData[] _uiSound;
    [field:SerializeField] public AudioSource[] Background { get; private set; }

    private AudioSource _curBackground;
    [Inject]
    private void Init()
    {
        DontDestroyOnLoad(gameObject);        
    }

    [System.Serializable]
    public struct PlayerSoundData
    {
       public PlayerSound sound;
       public AudioClip clip;
    } 
    [System.Serializable]
    public struct UISoundData
    {
       public UISound sound;
       public AudioClip clip;
    }
    
    private void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float soundsVolume = PlayerPrefs.GetFloat("SoundsVolume", 0.75f);
        bool musicMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        bool soundsMuted = PlayerPrefs.GetInt("SoundsMuted", 0) == 1;

        SetMusicVolume(musicMuted ? 0f : musicVolume);
        SetSoundsVolume(soundsMuted ? 0f : soundsVolume);

        foreach (var bg  in Background)
        {
            bg.Stop();
        }

        Background[0].Play();
    }

    public void RandomPitch(AudioSource pitchedAudio, float spread)
    {
        float pitch = Random.Range(-spread, spread);
        pitchedAudio.pitch = 1 + pitch;
        if (!pitchedAudio.isPlaying)
        {
            pitchedAudio.Play();
        }
        else if(pitchedAudio.time>0.1f)
        {
            pitchedAudio.Play();
        }
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log($"SetMusicVolume called with volume: {volume}");
        mixer.audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetSoundsVolume(float volume)
    {
        Debug.Log($"SetSoundsVolume called with volume: {volume}");
        mixer.audioMixer.SetFloat("Sound", Mathf.Log10(volume) * 20);
    }

    public void Mute(bool mute)
    {
        if (mute)
        {
            mixer.audioMixer.SetFloat("MasterVolume", -80);
        }
        else
        {
            mixer.audioMixer.SetFloat("MasterVolume", 0);
        }
    }

    public void ButtonClick(int typeNumber)
    {
        switch(typeNumber)
        {
            case 0: UIPlay(UISound.buttonClick);
               break;
        }
    }

    public void ChangeBackground(AudioSource source)
    {
        if (!_curBackground)
        {
            _curBackground = source;
            source.Play();
            return;
        }
        if (_curBackground == source) return;
        FadeSound(source);
    }

    public void OverlapBackground(AudioSource source)
    {
        float tr = _curBackground.time;
        _curBackground.Stop();
        _curBackground = source;
        _curBackground.time = tr;
        _curBackground.Play();
    }

    private void FadeSound(AudioSource source)
    {
        source.volume = 0;
        source.Play();
        source.DOFade(1, 3);        
        _curBackground.DOFade(0, 3).OnComplete(() =>
        {
            _curBackground.Stop();
            _curBackground = source;
        });        
    }
    public void PlayerPlay(PlayerSound sound, bool isLoop)
    {
        var clip = System.Array.Find(_playerSounds, s => s.sound == sound).clip;
        _playerSource.clip = clip;
        _playerSource.loop = isLoop;
        _playerSource.Play();
    }
    public void PlayerStop() => _playerSource.Stop();
    public void UIPlay(UISound sound)
    {
        var clip = System.Array.Find(_uiSound, s => s.sound == sound).clip;
        _uiSource.clip = clip;
        _uiSource.Play();
    }
}