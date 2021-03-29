using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PitchRange {
    public float min = 0.96f;
    public float max = 1.04f;
}

public class AudioManager : MonoBehaviour
{
    // singleton instance
    public static AudioManager instance = null;

    public float volumeMaster = 1;
    public float volumeMusic = 1;
    public float volumeSfx = 1;

    public PitchRange sfxPitchRange;

    [SerializeField]
    private AudioClip musicFull;
    [SerializeField]
    private float musicFullVolume = 1f;
    [SerializeField]
    private AudioClip musicSpeaker;
    [SerializeField]
    private float musicSpeakerVolume = 1f;
    [SerializeField]
    private AudioClip sfxAmbience;
    [SerializeField]
    private float sfxAmbienceVolume = 1f;
    [SerializeField]
    private AudioClip sfxBadcard1;
    [SerializeField]
    private float sfxBadcard1Volume = 1f;
    [SerializeField]
    private AudioClip sfxBadcard2;
    [SerializeField]
    private float sfxBadcard2Volume = 1f;
    [SerializeField]
    private AudioClip sfxBuy;
    [SerializeField]
    private float sfxBuyVolume = 1f;
    [SerializeField]
    private AudioClip sfxDiscard;
    [SerializeField]
    private float sfxDiscardVolume = 1f;
    [SerializeField]
    private AudioClip sfxEnter;
    [SerializeField]
    private float sfxEnterVolume = 1f;
    [SerializeField]
    private AudioClip sfxGoodCard1;
    [SerializeField]
    private float sfxGoodCard1Volume = 1f;
    [SerializeField]
    private AudioClip sfxGoodCard2;
    [SerializeField]
    private float sfxGoodCard2Volume = 1f;
    [SerializeField]
    private AudioClip sfxLose;
    [SerializeField]
    private float sfxLoseVolume = 1f;
    [SerializeField]
    private AudioClip sfxMouseOver;
    [SerializeField]
    private float sfxMouseOverVolume = 1f;
    [SerializeField]
    private AudioClip sfxPause;
    [SerializeField]
    private float sfxPauseVolume = 1f;
    [SerializeField]
    private AudioClip sfxPickup1;
    [SerializeField]
    private float sfxPickup1Volume = 1f;
    [SerializeField]
    private AudioClip sfxPickup2;
    [SerializeField]
    private float sfxPickup2Volume = 1f;
    [SerializeField]
    private AudioClip sfxPickup3;
    [SerializeField]
    private float sfxPickup3Volume = 1f;
    [SerializeField]
    private AudioClip sfxTVNoise;
    [SerializeField]
    private float sfxTVNoiseVolume = 1f;
    [SerializeField]
    private AudioClip sfxWin;
    [SerializeField]
    private float sfxWinVolume = 1f;

    [SerializeField]
    private AudioSource musicFullSource;
    [SerializeField]
    private AudioSource musicSpeakerSource;
    [SerializeField]
    private AudioSource sfxAmbienceSource;
    private AudioSource sfxBadcard1Source;
    private AudioSource sfxBadcard2Source;
    private AudioSource sfxBuySource;
    private AudioSource sfxDiscardSource;
    private AudioSource sfxEnterSource;
    private AudioSource sfxGoodCard1Source;
    private AudioSource sfxGoodCard2Source;
    private AudioSource sfxLoseSource;
    private AudioSource sfxMouseOverSource;
    private AudioSource sfxPauseSource;
    private AudioSource sfxPickup1Source;
    private AudioSource sfxPickup2Source;
    private AudioSource sfxPickup3Source;
    private AudioSource sfxTVNoiseSource;
    private AudioSource sfxWinSource;

    private List<AudioSource> musicSources;
    private List<AudioSource> sfxSources;

    void Awake() {
        // singleton setup
        if (instance == null) {
            Object.DontDestroyOnLoad(this.gameObject);
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        // musicFullSource = gameObject.AddComponent<AudioSource>();
        // musicFullSource.clip = musicFull;
        // musicSpeakerSource = gameObject.AddComponent<AudioSource>();
        // musicSpeakerSource.clip = musicSpeaker;
        // sfxAmbienceSource = gameObject.AddComponent<AudioSource>();
        // sfxAmbienceSource.clip = sfxAmbience;
        sfxBadcard1Source = gameObject.AddComponent<AudioSource>();
        sfxBadcard1Source.clip = sfxBadcard1;
        sfxBadcard2Source = gameObject.AddComponent<AudioSource>();
        sfxBadcard2Source.clip = sfxBadcard2;
        sfxBuySource = gameObject.AddComponent<AudioSource>();
        sfxBuySource.clip = sfxBuy;
        sfxDiscardSource = gameObject.AddComponent<AudioSource>();
        sfxDiscardSource.clip = sfxDiscard;
        sfxEnterSource = gameObject.AddComponent<AudioSource>();
        sfxEnterSource.clip = sfxEnter;
        sfxGoodCard1Source = gameObject.AddComponent<AudioSource>();
        sfxGoodCard1Source.clip = sfxGoodCard1;
        sfxGoodCard2Source = gameObject.AddComponent<AudioSource>();
        sfxGoodCard2Source.clip = sfxGoodCard2;
        sfxLoseSource = gameObject.AddComponent<AudioSource>();
        sfxLoseSource.clip = sfxLose;
        sfxMouseOverSource = gameObject.AddComponent<AudioSource>();
        sfxMouseOverSource.clip = sfxMouseOver;
        sfxPauseSource = gameObject.AddComponent<AudioSource>();
        sfxPauseSource.clip = sfxPause;
        sfxPickup1Source = gameObject.AddComponent<AudioSource>();
        sfxPickup1Source.clip = sfxPickup1;
        sfxPickup2Source = gameObject.AddComponent<AudioSource>();
        sfxPickup2Source.clip = sfxPickup2;
        sfxPickup3Source = gameObject.AddComponent<AudioSource>();
        sfxPickup3Source.clip = sfxPickup3;
        sfxTVNoiseSource = gameObject.AddComponent<AudioSource>();
        sfxTVNoiseSource.clip = sfxTVNoise;
        sfxWinSource = gameObject.AddComponent<AudioSource>();
        sfxWinSource.clip = sfxWin;

        musicSources = new List<AudioSource>() {
            musicFullSource,
            musicSpeakerSource,
            sfxAmbienceSource,
        };
        sfxSources = new List<AudioSource>() {
            sfxBadcard1Source,
            sfxBadcard2Source,
            sfxBuySource,
            sfxDiscardSource,
            sfxEnterSource,
            sfxGoodCard1Source,
            sfxGoodCard2Source,
            sfxLoseSource,
            sfxMouseOverSource,
            sfxPauseSource,
            sfxPickup1Source,
            sfxPickup2Source,
            sfxPickup3Source,
            sfxTVNoiseSource,
            sfxWinSource,
        };

        SetVolumeMusic();
    }

    // public void SetMusicFullVolume(float volume) {
    //     musicFullVolume = volume;
    // }
    // public void SetMusicSpeakerVolume(float volume) {
    //     musicSpeakerVolume = volume;
    // }
    // public void SetSfxAmbienceVolume(float volume) {
    //     sfxAmbienceVolume = volume;
    // }
    // public void SetSfxBadcard1Volume(float volume) {
    //     sfxBadcard1Volume = volume;
    // }
    // public void SetSfxBadcard2Volume(float volume) {
    //     sfxBadcard2Volume = volume;
    // }
    // public void SetSfxBuyVolume(float volume) {
    //     sfxBuyVolume = volume;
    // }
    // public void SetSfxDiscardVolume(float volume) {
    //     sfxDiscardVolume = volume;
    // }
    // public void SetSfxEnterVolume(float volume) {
    //     sfxEnterVolume = volume;
    // }
    // public void SetSfxGoodCard1Volume(float volume) {
    //     sfxGoodCard1Volume = volume;
    // }
    // public void SetSfxGoodCard2Volume(float volume) {
    //     sfxGoodCard2Volume = volume;
    // }
    // public void SetSfxLoseVolume(float volume) {
    //     sfxLoseVolume = volume;
    // }
    // public void SetSfxMouseOverVolume(float volume) {
    //     sfxMouseOverVolume = volume;
    // }
    // public void SetSfxPauseVolume(float volume) {
    //     sfxPauseVolume = volume;
    // }
    // public void SetSfxPickup1Volume(float volume) {
    //     sfxPickup1Volume = volume;
    // }
    // public void SetSfxPickup2Volume(float volume) {
    //     sfxPickup2Volume = volume;
    // }
    // public void SetSfxPickup3Volume(float volume) {
    //     sfxPickup3Volume = volume;
    // }
    // public void SetSfxTVNoiseVolume(float volume) {
    //     sfxTVNoiseVolume = volume;
    // }
    // public void SetSfxWinVolume(float volume) {
    //     sfxWinVolume = volume;
    // }

    private float GetMusicVolume() {
        return volumeMaster * volumeMusic;
    }

    private float GetSfxVolume() {
        return volumeMaster * volumeSfx;
    }

    public void SetVolumeMusic() {
        float volume = GetMusicVolume();
        
        if (GameController.instance.isPaused) {
            musicFullSource.volume = volume * musicFullVolume;
            musicSpeakerSource.volume = 0;
            sfxAmbienceSource.volume = 0;
        } else {
            musicFullSource.volume = 0;
            musicSpeakerSource.volume = volume * musicSpeakerVolume;
            sfxAmbienceSource.volume = volume * sfxAmbienceVolume;
        }
    }

    // public void SetVolumeSfx() {
    //     float volume = GetSfxVolume();
    //     foreach (AudioSource source in sfxSources) {
    //         source.volume = volume;
    //     }
    // }

    public void SetVolumeAll() {
        SetVolumeMusic();
        // SetVolumeSfx();
    }

    public void OnMasterVolumeChange(float value) {
        volumeMaster = value;
        SetVolumeAll();
    }

    public void OnMusicVolumeChange(float value) {
        volumeMusic = value;
        SetVolumeMusic();
    }

    public void OnSfxVolumeChange(float value) {
        volumeSfx = value;
        // SetVolumeSfx();
    }

    private float GetRandomPitch() {
        return Random.Range(sfxPitchRange.min, sfxPitchRange.max);
    }

    private void PlayWithRandomRange(AudioSource source, float volume) {
        if (source != null) {
            source.pitch = GetRandomPitch();
            source.volume = volume;
            source.Play();
        }
    }

    void PlayWithRandomRange() {
        Debug.Log("PlayWithRandomRange called with no arg");
    }

    public void StartBgMusic() {
        float musicVolume = GetMusicVolume();
        musicFullSource.Play();
        musicFullSource.volume = 0;
        musicSpeakerSource.Play();
        musicSpeakerSource.volume = musicVolume * musicSpeakerVolume;
        sfxAmbienceSource.Play();
        sfxAmbienceSource.volume = musicVolume * sfxAmbienceVolume;
    }

    public void OnPause(bool isPaused = true) {
        float volume = GetMusicVolume();

        // TODO coroutine to gradually do this over 0.5s rather than immediately
        if (isPaused) {
            sfxPauseSource.Play();
            musicFullSource.volume = volume * musicFullVolume;
            musicSpeakerSource.volume = 0;
            sfxAmbienceSource.volume = 0;
        } else {
            musicFullSource.volume = 0;
            musicSpeakerSource.volume = volume * musicSpeakerVolume;
            sfxAmbienceSource.volume = volume * sfxAmbienceVolume;
        }
    }

    public void OnPlayCard(bool isGood = true) {
        bool playSound1 = Random.Range(0, 1f) > 0.5f;
        
        if (isGood) {
            if (playSound1) {
                PlayWithRandomRange(sfxGoodCard1Source, sfxGoodCard1Volume);
            } else {
                PlayWithRandomRange(sfxGoodCard2Source, sfxGoodCard2Volume);
            }
        } else {
            if (playSound1) {
                PlayWithRandomRange(sfxBadcard1Source, sfxBadcard1Volume);
            } else {
                PlayWithRandomRange(sfxBadcard2Source, sfxBadcard2Volume);
            }
        }
    }

    public void OnClickButton() {
        PlayWithRandomRange(sfxEnterSource, sfxEnterVolume);
    }

    public void OnBuyCard() {
        PlayWithRandomRange(sfxBuySource, sfxBuyVolume);
    }

    public void OnDiscard() {
        PlayWithRandomRange(sfxDiscardSource, sfxDiscardVolume);
    }

    public void OnWinGame() {
        sfxWinSource.volume = sfxWinVolume;
        sfxWinSource.Play();
    }

    public void OnLoseGame() {
        sfxLoseSource.volume = sfxLoseVolume;
        sfxLoseSource.Play();
    }

    public void OnCardHover() {
        PlayWithRandomRange(sfxMouseOverSource, sfxMouseOverVolume);
    }

    // public void OnGainCard() {
    //     int ran = Random.Range(0, 3);
    //     switch(ran) {
    //         case 0:
    //             PlayWithRandomRange(sfxPickup1Source, sfxPickup1Volume);
    //             break;
    //         case 1:
    //             PlayWithRandomRange(sfxPickup2Source, sfxPickup2Volume);
    //             break;
    //         case 2:
    //             PlayWithRandomRange(sfxPickup3Source, sfxPickup3Volume);
    //             break;
    //         default:
    //             PlayWithRandomRange(sfxPickup1Source, sfxPickup1Volume);
    //             break;
    //     }
    // }
}
