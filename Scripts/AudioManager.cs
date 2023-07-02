using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    AudioSource saunaAudio;
    [SerializeField] GameObject sauna;

    [SerializeField] AudioClip fireSE;
    [SerializeField] AudioClip attackSE;
    [SerializeField] AudioClip damageSE;
    [SerializeField] AudioClip workSE;
    [SerializeField] AudioClip wordsSE;
    [SerializeField] AudioClip chestOpenSE;
    [SerializeField] AudioClip chestCloseSE;
    [SerializeField] AudioClip weaponSE;
    [SerializeField] AudioClip startSE;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
        saunaAudio = sauna.GetComponent<AudioSource>();
    }

    public void FireSE()
    {
        saunaAudio.PlayOneShot(fireSE);
    }
    public void StopFireSE()
    {
        saunaAudio.Stop();
    }

    public void AttackSE()
    {
        audioSource.PlayOneShot(attackSE);
    }
    public void DamageSE()
    {
        audioSource.PlayOneShot(damageSE);
    }
    public void WorkSE()
    {
        audioSource.PlayOneShot(workSE);
    }
    public void WordsSE()
    {
        audioSource.PlayOneShot(wordsSE);
    }
    public void ChestOpenSE()
    {
        audioSource.PlayOneShot(chestOpenSE);
    }
    public void ChestCloseSE()
    {
        audioSource.PlayOneShot(chestCloseSE);
    }
    public void WeaponSE()
    {
        audioSource.PlayOneShot(weaponSE);
    }
    public void StartSE()
    {
        audioSource.PlayOneShot(startSE);
    }
}