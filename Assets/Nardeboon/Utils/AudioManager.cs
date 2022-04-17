using System;
using UnityEngine;


public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    public static Action<bool> OnSoundSetEvent;
    public static Action OnSoundChangeEvent;

    private AudioSource musicPlayer;
    private AudioSource sfxPlayer;

    private bool _isSoundOn;

    public const string MUSICKEY = "SFX";

    private void Awake() {
        if (instance == null) {
            instance = this;
        }

        musicPlayer = new GameObject("Music Player").AddComponent<AudioSource>();
        musicPlayer.transform.SetParent(transform);
        musicPlayer.spatialBlend = 0;
        musicPlayer.loop = true;
        musicPlayer.volume = 1;

        sfxPlayer = new GameObject("SFX Player").AddComponent<AudioSource>();
        sfxPlayer.transform.SetParent(transform);
        sfxPlayer.spatialBlend = 0;
        sfxPlayer.loop = false;
        sfxPlayer.playOnAwake = false;
        sfxPlayer.volume = 1;
    }

    public void PlayMusic(SoundClass clip) {
        musicPlayer.clip = clip.Clip;
        musicPlayer.mute = !_isSoundOn;
        musicPlayer.volume = clip.Volume * MasterSound;
        musicPlayer.pitch = clip.Pitch;

        musicPlayer.loop = clip.Loop;

        musicPlayer.Play();
    }

    public void PlaySFX(SoundClass clip, float pitch = 1)
    {
        sfxPlayer.clip = clip.Clip;
        sfxPlayer.mute = !_isSoundOn;
        sfxPlayer.volume = clip.Volume * MasterSound;
        sfxPlayer.pitch = pitch;
        sfxPlayer.panStereo = clip.SoundChanel;

        sfxPlayer.PlayOneShot(clip.Clip, _isSoundOn ? 1 : 0);
    }

    public void ChangeState(bool state) {
        _isSoundOn = state;
        musicPlayer.mute = _isSoundOn;
    }

    [Range(0.0f, 1.0f)] public float MasterSound;
    public MusicClass Music;

    public SFXClass SFX;
}


[Serializable]
public class SoundClass {
    public AudioClip Clip;

    [Range(0f, 2f)]
    public float Volume = 1f;

    [Range(0.5f, 1.5f)]
    public float Pitch = 1f;

    [Range(-1, 1)]
    public float SoundChanel = 0;

    public bool Loop;
}

[Serializable]
public class SFXClass {
    public SoundClass[] shoot;
    public SoundClass explosion;
    public SoundClass gameover;
    public SoundClass clickUI;
    public SoundClass openUI;
    public SoundClass starPop;
    public SoundClass win;
    public SoundClass warning;
    public SoundClass giantFull;
    public SoundClass coinDling;
}

[Serializable]
public class MusicClass {
    public SoundClass[] menu;
}