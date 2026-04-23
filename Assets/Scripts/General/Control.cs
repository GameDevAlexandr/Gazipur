using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Control : MonoBehaviour
{
    public static Action<Vector2> OnMouseDownInObject;
    public static Action<InteractObject> OnSelectObject;
    public static Action OnInteractObject;        // Короткое нажатие E (Tap)
    public static Action OnAlternativeInteract;   // Длинное нажатие E (Hold)
    public static Action OnOpenInventory;
    public static Action<int> OnInventorySlotSelected;

    [Header("Компоненты")]
    [SerializeField] private GameObject holdProgressBarObject;

    private PlayerInputActions inputActions;
    private HoldProgressBar holdProgressBar;
    private bool isHoldInProgress = false;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        if (holdProgressBarObject != null)
        {
            holdProgressBar = holdProgressBarObject.GetComponent<HoldProgressBar>();
            if (holdProgressBar != null)
            {
                holdProgressBar.OnHoldComplete += OnHoldComplete;
                holdProgressBar.OnHoldCancel += OnHoldCancel;
            }
            else
            {
                Debug.LogWarning("Control: На объекте HoldProgressBarObject нет компонента HoldProgressBar!");
            }

            holdProgressBarObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Control: HoldProgressBarObject не назначен в инспекторе!");
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Interact.performed += OnInteractPerformed;
        inputActions.Player.HoldInteract.started += OnHoldInteractStarted;
        inputActions.Player.HoldInteract.canceled += OnHoldInteractCanceled;
        inputActions.Player.HoldInteract.performed += OnHoldInteractPerformed;

        inputActions.Player.Inventory.performed += OnInventoryButtonPressed;
        inputActions.Player.Escape.performed += OnEscape;

        inputActions.Player.Slot1.performed += OnSlot1Performed;
        inputActions.Player.Slot2.performed += OnSlot2Performed;
        inputActions.Player.Slot3.performed += OnSlot3Performed;
        inputActions.Player.Slot4.performed += OnSlot4Performed;
        inputActions.Player.Slot5.performed += OnSlot5Performed;
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnInteractPerformed;
        inputActions.Player.HoldInteract.started -= OnHoldInteractStarted;
        inputActions.Player.HoldInteract.canceled -= OnHoldInteractCanceled;
        inputActions.Player.HoldInteract.performed -= OnHoldInteractPerformed;

        inputActions.Player.Inventory.performed -= OnInventoryButtonPressed;
        inputActions.Player.Escape.performed -= OnEscape;

        inputActions.Player.Slot1.performed -= OnSlot1Performed;
        inputActions.Player.Slot2.performed -= OnSlot2Performed;
        inputActions.Player.Slot3.performed -= OnSlot3Performed;
        inputActions.Player.Slot4.performed -= OnSlot4Performed;
        inputActions.Player.Slot5.performed -= OnSlot5Performed;

        inputActions.Disable();
    }

    private void Update()
    {
        InteractObject iObject = GetInteractObjectUnderCursor();
        OnSelectObject?.Invoke(iObject);
    }

    // Отправляет луч из камеры под курсор мыши, игнорируя UI
    private InteractObject GetInteractObjectUnderCursor()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return null;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.GetComponent<InteractObject>();
        }

        return null;
    }

    private void OnHoldInteractStarted(InputAction.CallbackContext context)
    {
        Debug.Log("Control: Начало удержания Е");
        isHoldInProgress = true;

        if (holdProgressBarObject != null)
        {
            holdProgressBarObject.SetActive(true);
            if (holdProgressBar != null)
                holdProgressBar.StartHold();
        }
    }

    // Кнопка отпущена до завершения холда
    private void OnHoldInteractCanceled(InputAction.CallbackContext context)
    {
        if (isHoldInProgress)
        {
            Debug.Log("Control: Удержание Е отменено (кнопка отпущена)");
            isHoldInProgress = false;

            if (holdProgressBar != null)
                holdProgressBar.CancelHold();

            if (holdProgressBarObject != null)
                holdProgressBarObject.SetActive(false);
        }
    }

    // Срабатывает, когда кнопка удержана достаточно долго (по настройкам Input System)
    // Само действие обрабатывается через OnHoldComplete из прогресс-бара
    private void OnHoldInteractPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Control: Удержание Е выполнено (достигнута длительность)");
    }

    // Короткое нажатие игнорируется, если игрок в процессе удержания
    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (isHoldInProgress) return;

        Debug.Log("Control: Вызвано короткое нажатие Е");
        OnInteractObject?.Invoke();
    }

    private void OnHoldComplete()
    {
        Debug.Log("Control: Вызвано длинное нажатие Е (завершено)");
        isHoldInProgress = false;

        if (holdProgressBarObject != null)
            holdProgressBarObject.SetActive(false);

        OnAlternativeInteract?.Invoke();
    }

    private void OnHoldCancel()
    {
        Debug.Log("Control: Удержание Е отменено (через прогресс-бар)");
        isHoldInProgress = false;

        if (holdProgressBarObject != null)
            holdProgressBarObject.SetActive(false);
    }

    private void OnInventoryButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Control: Вызвано открытие инвентаря");
        OnOpenInventory?.Invoke();
    }

    private void OnEscape(InputAction.CallbackContext context)
    {
        Debug.Log("Control: Нажата кнопка Escape");
    }

    private void OnSlot1Performed(InputAction.CallbackContext context)
    {
        Debug.Log("Control: Выбран слот инвентаря 1");
        OnInventorySlotSelected?.Invoke(1);
    }

    private void OnSlot2Performed(InputAction.CallbackContext context)
    {
        Debug.Log("Control: Выбран слот инвентаря 2");
        OnInventorySlotSelected?.Invoke(2);
    }

    private void OnSlot3Performed(InputAction.CallbackContext context)
    {
        Debug.Log("Control: Выбран слот инвентаря 3");
        OnInventorySlotSelected?.Invoke(3);
    }

    private void OnSlot4Performed(InputAction.CallbackContext context)
    {
        Debug.Log("Control: Выбран слот инвентаря 4");
        OnInventorySlotSelected?.Invoke(4);
    }

    private void OnSlot5Performed(InputAction.CallbackContext context)
    {
        Debug.Log("Control: Выбран слот инвентаря 5");
        OnInventorySlotSelected?.Invoke(5);
    }
}