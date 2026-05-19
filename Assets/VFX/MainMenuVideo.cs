using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MainMenuVideo : MonoBehaviour
{
    [SerializeField] private RawImage videoRawImage;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject[] uiElementsToShowAfterVideo; // Сюда перетащите элементы меню, которые должны появиться ПОСЛЕ видео

    void Start()
    {
        // Скрываем все элементы UI меню в начале
        foreach (var uiElement in uiElementsToShowAfterVideo)
        {
            if (uiElement != null)
                uiElement.SetActive(false);
        }

        // Настраиваем видеоплеер
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Скрываем видео
        videoRawImage.gameObject.SetActive(false);

        // Показываем элементы главного меню
        foreach (var uiElement in uiElementsToShowAfterVideo)
        {
            if (uiElement != null)
                uiElement.SetActive(true);
        }

        // Отписываемся от события (хороший тон)
        videoPlayer.loopPointReached -= OnVideoFinished;
    }
}