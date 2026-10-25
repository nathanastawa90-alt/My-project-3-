using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SaveBox : MonoBehaviour
{
public CharacterController player; // your Capsule
public TextMeshProUGUI promptText; // the on-screen text
public float touchPadding = 0.5f;
public float savedMessageTime = 1.5f;

// static = survives the scene reload when you die
public static bool hasSave;
static Vector3 savedPosition;
static Quaternion savedRotation;

private Collider box;
private float messageTimer;

void Start()
{
box = GetComponent<Collider>();
promptText.gameObject.SetActive(false);

// After a restart, move the player to the saved spot
if (hasSave)
{
player.enabled = false;
player.transform.SetPositionAndRotation(savedPosition, savedRotation);
player.enabled = true;
}
}

void Update()
{
Bounds b = box.bounds;
b.Expand(touchPadding);
bool touching = b.Intersects(player.bounds);

// Show "Game saved!" for a moment after saving
if (messageTimer > 0f)
{
messageTimer -= Time.deltaTime;
if (messageTimer <= 0f)
promptText.gameObject.SetActive(false);
return;
}

promptText.gameObject.SetActive(touching);
if (touching)
promptText.text = "Press A to save the game";

if (touching && Keyboard.current != null && Keyboard.current.aKey.wasPressedThisFrame)
{
hasSave = true;
savedPosition = player.transform.position;
savedRotation = player.transform.rotation;

promptText.text = "Game saved!";
promptText.gameObject.SetActive(true);
messageTimer = savedMessageTime;
}
}
}

