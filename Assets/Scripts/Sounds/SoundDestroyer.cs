using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDestroyer : MonoBehaviour
{
    private AudioSource _audioSourece;
    private float _clipLength;
    public float timeToPlay;

    private void Awake()
    {
        _audioSourece = GetComponent<AudioSource>();
    }

    IEnumerator Start()
    {
        _clipLength = _audioSourece.loop ? timeToPlay : _audioSourece.clip.length;
        yield return new WaitForSeconds(_clipLength);
        Destroy(gameObject);
    }
}
