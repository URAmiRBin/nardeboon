using System;
using UnityEngine;


public class AudioManager : MonoBehaviour {
    [Range(0.0f, 1.0f)] [SerializeField] float MasterSound;
    [SerializeField] MusicLibrary Music;
    [SerializeField] SFXLibrary SFX;
    
    private AudioSource musicPlayer;
    private AudioSource sfxPlayer;
    private bool _sound;

    public bool SoundStatus {
        get => _sound;
        private set {
            _sound = value;
            PlayerPrefs.SetInt(PlayerPrefKeys.SOUND, _sound ? 1 : 0);
            musicPlayer.mute = _sound;
        }
    }

    public void Initialize() {
        musicPlayer = gameObject.AddComponent<AudioSource>();
        musicPlayer.spatialBlend = 0;
        musicPlayer.loop = true;
        musicPlayer.volume = 1;

        sfxPlayer = gameObject.AddComponent<AudioSource>();

        sfxPlayer.spatialBlend = 0;
        sfxPlayer.loop = false;
        sfxPlayer.playOnAwake = false;
        sfxPlayer.volume = 1;

        NardeboonEvents.UIEvents.onSoundSetEvent += SetSoundStatus;
    }

    void OnDisable() {
        NardeboonEvents.UIEvents.onSoundSetEvent += SetSoundStatus;
    }

    public void PlayMusic(SoundClass clip) {
        musicPlayer.clip = clip.Clip;
        musicPlayer.mute = !_sound;
        musicPlayer.volume = clip.Volume * MasterSound;
        musicPlayer.pitch = clip.Pitch;

        musicPlayer.loop = clip.Loop;

        musicPlayer.Play();
    }

    public void PlaySFX(SoundClass clip, float pitch = 1) {
        sfxPlayer.clip = clip.Clip;
        sfxPlayer.mute = !_sound;
        sfxPlayer.volume = clip.Volume * MasterSound;
        sfxPlayer.pitch = pitch;
        sfxPlayer.panStereo = clip.SoundChanel;

        sfxPlayer.PlayOneShot(clip.Clip, _sound ? 1 : 0);
    }

    public void PlayMusic(AudioClip clip, bool loop = false) {
        musicPlayer.clip = clip;
        musicPlayer.mute = !_sound;
        musicPlayer.volume = MasterSound;
        musicPlayer.loop = loop;
        musicPlayer.Play();
    }

    public void PlaySFX(AudioClip clip, float pitch = 1) {
        sfxPlayer.clip = clip;
        sfxPlayer.mute = !_sound;
        sfxPlayer.volume = MasterSound;
        sfxPlayer.pitch = pitch;
        sfxPlayer.PlayOneShot(clip, _sound ? 1 : 0);
    }

    public void SetSoundStatus(bool state) => SoundStatus = state;
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
public class SFXLibrary {
    public SoundClass gameover;
    public SoundClass clickUI;
    public SoundClass openUI;
    public SoundClass win;
    public SoundClass coinDling;
}

[Serializable]
public class MusicLibrary {
    public SoundClass[] menu;
}