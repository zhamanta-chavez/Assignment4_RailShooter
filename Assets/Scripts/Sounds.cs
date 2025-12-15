using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip[] audioClips;

    public void ShootSound()
    {
        audio.PlayOneShot(audioClips[0]);
    }
}

