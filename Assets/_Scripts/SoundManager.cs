using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance;

    public static SoundManager GetInstance()
    {
        if (instance == null)
        {
            var k = GameObject.Find("ManagerObject").AddComponent<SoundManager>();
            instance = k;

        }
        return instance;
    }

    public AudioClip ShootSound;
    public AudioClip ConfirmSound;
    public AudioClip ObjectDestroyedSound;

    private AudioSource source;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            source = GetComponent<AudioSource>();
            Debug.Log("SoundManager initialized");
        }
    }

    //TODO::Add sound effects
    public void PlayShootSound()
    {
        return;
        source.PlayOneShot(ShootSound);
    }

    public void PlayConfirmSound()
    {
        return;
        source.PlayOneShot(ConfirmSound);
    }

    public void PlayObjectDestroyedSound()
    {
        return;
        source.PlayOneShot(ObjectDestroyedSound);
    }
}
