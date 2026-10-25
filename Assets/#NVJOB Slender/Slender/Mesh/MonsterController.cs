using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterController : MonoBehaviour
{
public CharacterController player; // your Capsule
public Collider startBox; // Cube (99): monster appears, chases, sound starts
public Collider endBox; // Cube (100): monster disappears, sound stops
public AudioClip chaseSound; // the "Slender" sound
public float speed = 4f;
public float touchPadding = 0.5f;
public float catchDistance = 1.2f;
[Range(0f, 1f)] public float volume = 1f;

private Renderer[] renderers;
private AudioSource audioSource;
private bool chasing;

void Start()
{
renderers = GetComponentsInChildren<Renderer>(true);
SetVisible(false);

audioSource = gameObject.AddComponent<AudioSource>();
audioSource.clip = chaseSound;
audioSource.loop = true;
audioSource.playOnAwake = false;
audioSource.spatialBlend = 0f; // 0 = heard evenly everywhere, 1 = gets louder as the monster gets closer
audioSource.volume = volume;
}

void Update()
{
if (!chasing && Touching(startBox))
{
chasing = true;
SetVisible(true);
audioSource.Play();
}
else if (chasing && Touching(endBox))
{
chasing = false;
SetVisible(false);
audioSource.Stop();
}

if (!chasing) return;

Vector3 target = player.transform.position;
target.y = transform.position.y;
transform.LookAt(target);
transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

if (Vector3.Distance(transform.position, target) <= catchDistance)
{
SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
}

bool Touching(Collider box)
{
Bounds b = box.bounds;
b.Expand(touchPadding);
return b.Intersects(player.bounds);
}

void SetVisible(bool visible)
{
foreach (Renderer r in renderers)
r.enabled = visible;
}
}
