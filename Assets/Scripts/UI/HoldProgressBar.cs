using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoldProgressBar : MonoBehaviour
{
    private const float HOLD_DURATION = 1f;           // Длительность удержания (секунд)
    private const float START_ALPHA = 0f;             // Начальная прозрачность (0-255)
    private const float END_ALPHA = 80f;              // Конечная прозрачность (0-255)

    private Image progressImage;
    private Coroutine holdCoroutine;
    private Color originalColor;

    private bool isHolding = false;
    private float currentHoldTime = 0f;

    public System.Action OnHoldComplete;
    public System.Action OnHoldCancel;

    private void Awake()
    {
        progressImage = GetComponent<Image>();
        if (progressImage == null)
        {
            Debug.LogError("HoldProgressBar: Нет компонента Image на объекте " + gameObject.name);
            return;
        }

        originalColor = progressImage.color;
    }

    // EaseInQuart: t^4 - очень медленное начало с резким ускорением к концу
    private float EaseInQuad(float t)
    {
        return t * t;
    }

    public void StartHold()
    {
        if (progressImage == null || isHolding) return;

        Debug.Log("HoldProgressBar: Начало удержания");
        isHolding = true;
        currentHoldTime = 0f;

        Color startColor = originalColor;
        startColor.a = START_ALPHA / 255f;
        progressImage.color = startColor;
        progressImage.fillAmount = 0f;

        if (holdCoroutine != null)
            StopCoroutine(holdCoroutine);
        holdCoroutine = StartCoroutine(HoldProcess());
    }

    public void CancelHold()
    {
        if (!isHolding) return;

        Debug.Log("HoldProgressBar: Удержание отменено");
        isHolding = false;

        if (holdCoroutine != null)
            StopCoroutine(holdCoroutine);

        progressImage.fillAmount = 0f;
        OnHoldCancel?.Invoke();
    }

    private IEnumerator HoldProcess()
    {
        while (currentHoldTime < HOLD_DURATION)
        {
            currentHoldTime += Time.deltaTime;

            float progress = currentHoldTime / HOLD_DURATION;

            // Заполнение круга (линейное)
            progressImage.fillAmount = progress;

            // Прозрачность меняется от START_ALPHA до END_ALPHA на протяжении всего цикла с замедлением в начале
            float easedProgress = EaseInQuad(progress);
            float alpha255 = Mathf.Lerp(START_ALPHA, END_ALPHA, easedProgress);

            Color newColor = originalColor;
            newColor.a = alpha255 / 255f;
            progressImage.color = newColor;

            yield return null;
        }

        CompleteHold();
    }

    private void CompleteHold()
    {
        if (!isHolding) return;

        Debug.Log("HoldProgressBar: Удержание завершено");
        isHolding = false;
        progressImage.fillAmount = 0f;
        OnHoldComplete?.Invoke();
    }

    private void OnDestroy()
    {
        OnHoldComplete = null;
        OnHoldCancel = null;
    }
}