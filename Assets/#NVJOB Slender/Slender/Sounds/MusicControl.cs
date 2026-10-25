using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
public AudioClip music; // lux_aete...
[Range(0f, 1f)] public float volume = 0.5f;
public float checkInterval = 0.2f;

private AudioSource source;
private bool pausedByOtherAudio;
private float timer;

void Start()
{
source = GetComponent<AudioSource>();
source.clip = music;
source.loop = true;
source.playOnAwake = false;
source.spatialBlend = 0f;
source.volume = volume;
source.Play();
}

void Update()
{
// Only check a few times a second to keep it cheap
timer -= Time.unscaledDeltaTime;
if (timer > 0f) return;
timer = checkInterval;

bool otherPlaying = false;
foreach (AudioSource s in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
{
if (s != source && s.isPlaying)
{
otherPlaying = true;
break;
}
}

if (otherPlaying && !pausedByOtherAudio)
{
source.Pause();
pausedByOtherAudio = true;
}
else if (!otherPlaying && pausedByOtherAudio)
{
source.UnPause();
pausedByOtherAudio = false;
}
}
}
