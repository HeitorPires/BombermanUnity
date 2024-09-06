using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Audio/Sound Manager", fileName ="Sound Manager")]
public class SoundManagerSO : ScriptableObject
{
    private static SoundManagerSO instance;
    public static SoundManagerSO Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Resources.Load<SoundManagerSO>("Sound Manager");
            }
            return instance;
        }
    }

    public AudioSource SoundObject;

    public static void PlaySoundFXClip(AudioClip clip, Vector3 soundPos, float volume, bool loop = false)
    {
        AudioSource a = Instantiate(Instance.SoundObject, soundPos, Quaternion.identity);
        a.loop = loop;

        a.clip = clip;
        a.volume = volume;
        a.Play();
    }
}
