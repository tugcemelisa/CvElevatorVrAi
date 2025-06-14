using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnTouch : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerHand")) 
        {
            audioSource.Play();
        }
    }
}
