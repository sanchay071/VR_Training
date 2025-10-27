using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayerController : MonoBehaviour
{
    [Header("Music Settings")]
    public AudioClip[] songs;          // List of music tracks
    public AudioSource musicSource;    // Music AudioSource

    [Header("Button Click Sound")]
    public AudioClip clickSound;       // Sound to play on button press
    public AudioSource clickSource;    // Separate AudioSource for click

    [Header("Trigger Settings")]
    public Collider triggerCollider;   // Assign your BoxCollider or trigger

    private int currentSong = 0;

    void Start()
    {
        // If AudioSource not assigned, get it from this GameObject
        if (musicSource == null)
            musicSource = GetComponent<AudioSource>();

        if (songs.Length > 0)
        {
            musicSource.clip = songs[currentSong];
            musicSource.Play(); // play music on start
        }

        // Ensure clickSource exists
        if (clickSource == null)
        {
            GameObject clickObj = new GameObject("ClickSource");
            clickObj.transform.parent = transform;
            clickSource = clickObj.AddComponent<AudioSource>();
        }
    }

    // Detect player/controller entering the collider
    private void OnTriggerEnter(Collider other)
    {
        // You can filter by tag if you want only the controller to trigger
        if (other.CompareTag("PlayerHand"))
        {
            ToggleMusic();
        }
    }

    // Toggle play/pause for music
    private void ToggleMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Pause();
        else
            musicSource.Play();

        // Play button click sound
        if (clickSound != null)
            clickSource.PlayOneShot(clickSound);
    }

    // Optional: next track
    public void NextSong()
    {
        if (songs.Length == 0) return;
        currentSong = (currentSong + 1) % songs.Length;
        musicSource.clip = songs[currentSong];
        musicSource.Play();
    }
}
