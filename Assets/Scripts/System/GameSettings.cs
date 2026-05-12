using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Slider _musicVoloumeSlider;
    [SerializeField] private Slider _soundVoloumeSlider;
    [SerializeField] private Slider _mouseSensSlider;
    [SerializeField] private Toggle _muteToggle;

    [Inject] Sounds _sounds;
    public void Start()
    {
        _musicVoloumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
        _soundVoloumeSlider.onValueChanged.AddListener(ChangeSoundVolume);
        _mouseSensSlider.onValueChanged.AddListener(ChangeSensitivity);
        _muteToggle.onValueChanged.AddListener(Mute);
    }
    private void ChangeMusicVolume(float value)
    {
        _sounds.SetMusicVolume(value);
    } 
    private void ChangeSoundVolume(float value)
    {
        _sounds.SetSoundsVolume(value);
    }
    private void Mute(bool isMute)
    {
        _sounds.Mute(isMute);
    }
    private void ChangeSensitivity(float value)
    {

    }
}
