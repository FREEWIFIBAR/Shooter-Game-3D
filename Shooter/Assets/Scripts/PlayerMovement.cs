using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private Rigidbody _rb;
    private Animator _animator;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float angle = rotationSpeed * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X");
        _rb.AddRelativeTorque(0, mouseX * angle, 0);

        if (Input.GetKey(KeyCode.Mouse0) && Time.timeScale > 0)
        {
            _animator.SetFloat("Velocity", 0);
            return;
        }

        float space = speed * Time.deltaTime;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(horizontal, 0, vertical);
        _rb.AddRelativeForce(dir.normalized * space);

        _animator.SetFloat("Velocity", _rb.linearVelocity.magnitude);
    }
}