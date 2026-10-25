using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class EndBlock : MonoBehaviour
{
public CharacterController player; // your Capsule
public TextMeshProUGUI endText; // the on-screen text
public float touchPadding = 0.5f;

private Collider box;
private bool escaped;

void Start()
{
box = GetComponent<Collider>();
endText.text = "Yay you escaped, press B to do it all again";
endText.gameObject.SetActive(false);
}

void Update()
{
if (!escaped)
{
Bounds b = box.bounds;
b.Expand(touchPadding);

if (b.Intersects(player.bounds))
{
escaped = true;
endText.gameObject.SetActive(true);
Time.timeScale = 0f; // freeze the game
AudioListener.pause = true; // pause the monster sound
}
return;
}

if (Keyboard.current != null && Keyboard.current.bKey.wasPressedThisFrame)
{
SaveBox.hasSave = false; // forget the checkpoint
Time.timeScale = 1f;
AudioListener.pause = false;
SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
}
}
