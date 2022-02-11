using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip CoinSoundEffect;

    public void playCoinSoundEffect()
    {
        AudioSource.PlayClipAtPoint(CoinSoundEffect, Camera.main.transform.position);
    }
}
