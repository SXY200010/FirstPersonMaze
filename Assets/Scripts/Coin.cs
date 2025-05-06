using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Coin : MonoBehaviour
{
    [Tooltip("Sound to play when collected")]
    public AudioClip collectSound;

    private AudioSource audioSource;
    private bool collected = false;

    void Awake()
    {
        // make sure collider is trigger
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;

        // add AudioSource for playing collectSound
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = collectSound;
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        // only react to player
        if (other.CompareTag("Player"))
        {
            collected = true;

            // increment score
            ScoreManager.AddScore(1);

            // play sound
            audioSource.Play();

            // hide visuals
            foreach (var rend in GetComponentsInChildren<Renderer>())
                rend.enabled = false;

            // disable collider so it won't trigger again
            GetComponent<Collider>().enabled = false;

            // destroy after sound finishes
            Destroy(gameObject, collectSound.length);
        }
    }
}
