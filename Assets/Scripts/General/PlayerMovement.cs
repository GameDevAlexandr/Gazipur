using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _gravity = 20f;
    [SerializeField] float _jumpSpeed = 8f;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            moveDirection = new Vector3(horizontal, 0, vertical);
            moveDirection = transform.TransformDirection(moveDirection) * _speed;

            if (Input.GetButton("Jump"))
                moveDirection.y = _jumpSpeed;
        }

        moveDirection.y -= _gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
