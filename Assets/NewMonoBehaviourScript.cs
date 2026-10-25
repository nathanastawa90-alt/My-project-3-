using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
public float speed = 6f;
public float jumpHeight = 1.5f;
public float gravity = -20f;
public float mouseSensitivity = 0.1f;

private CharacterController controller;
private Transform cam;
private Vector3 velocity;
private float pitch;

void Start()
{
controller = GetComponent<CharacterController>();
cam = Camera.main.transform;
Cursor.lockState = CursorLockMode.Locked;
Cursor.visible = false;
}

void Update()
{
Keyboard kb = Keyboard.current;
Mouse mouse = Mouse.current;
if (kb == null || mouse == null) return;

// Mouse look: turn the body left/right, tilt the camera up/down
Vector2 look = mouse.delta.ReadValue() * mouseSensitivity;
transform.Rotate(0f, look.x, 0f);
pitch = Mathf.Clamp(pitch - look.y, -85f, 85f);
cam.localRotation = Quaternion.Euler(pitch, 0f, 0f);

// Movement relative to where the player is facing
float x = 0f, z = 0f;
if (kb.dKey.isPressed) x += 1f;
if (kb.aKey.isPressed) x -= 1f;
if (kb.wKey.isPressed) z += 1f;
if (kb.sKey.isPressed) z -= 1f;
Vector3 move = (transform.right * x + transform.forward * z).normalized;

// Gravity and jump
if (controller.isGrounded && velocity.y < 0f)
velocity.y = -2f;

if (kb.spaceKey.wasPressedThisFrame && controller.isGrounded)
velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

velocity.y += gravity * Time.deltaTime;

controller.Move((move * speed + velocity) * Time.deltaTime);

// Press Esc to get your mouse back
if (kb.escapeKey.wasPressedThisFrame)
{
Cursor.lockState = CursorLockMode.None;
Cursor.visible = true;
}
}
}
