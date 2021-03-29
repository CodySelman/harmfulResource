using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PitchRange {
    public float min = 0.9f;
    public float max = 1.1f;
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
    private AudioClip musicSpeaker;
    [SerializeField]
    private AudioClip sfxAmbience;
    [SerializeField]
    private AudioClip sfxBadcard1;
    [SerializeField]
    private AudioClip sfxBadcard2;
    [SerializeField]
    private AudioClip sfxBuy;
    [SerializeField]
    private AudioClip sfxDiscard;
    [SerializeField]
    private AudioClip sfxEnter;
    [SerializeField]
    private AudioClip sfxGoodCard1;
    [SerializeField]
    private AudioClip sfxGoodCard2;
    [SerializeField]
    private AudioClip sfxLose;
    [SerializeField]
    private AudioClip sfxMouseOver;
    [SerializeField]
    private AudioClip sfxPause;
    [SerializeField]
    private AudioClip sfxPickup1;
    [SerializeField]
    private AudioClip sfxPickup2;
    [SerializeField]
    private AudioClip sfxPickup3;
    [SerializeField]
    private AudioClip sfxTVNoise;
    [SerializeField]
    private AudioClip sfxWin;

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
    }

    private float GetMusicVolume() {
        return volumeMaster * volumeMusic;
    }

    private float GetSfxVolume() {
        return volumeMaster * volumeSfx;
    }

    public void SetVolumeMusic() {
        float volume = GetMusicVolume();
        foreach (AudioSource source in musicSources) {
            source.volume = volume;
        }
    }

    public void SetVolumeSfx() {
        float volume = GetSfxVolume();
        foreach (AudioSource source in sfxSources) {
            source.volume = volume;
        }
    }

    public void SetVolumeAll() {
        SetVolumeMusic();
        SetVolumeSfx();
    }

    private float GetRandomPitch() {
        return Random.Range(sfxPitchRange.min, sfxPitchRange.max);
    }

    private void PlayWithRandomRange(AudioSource source) {
        if (source != null) {
            source.pitch = GetRandomPitch();
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
        musicSpeakerSource.volume = musicVolume;
        sfxAmbienceSource.Play();
        sfxAmbienceSource.volume = musicVolume;
    }

    public void OnPause(bool isPaused = true) {
        float volume = GetMusicVolume();

        sfxPauseSource.Play();

        // TODO coroutine to gradually do this over 0.5s rather than immediately
        if (isPaused) {
            musicFullSource.volume = volume;
            musicSpeakerSource.volume = 0;
            sfxAmbienceSource.volume = 0;
        } else {
            musicFullSource.volume = 0;
            musicSpeakerSource.volume = volume;
            sfxAmbienceSource.volume = volume;
        }
    }

    public void OnPlayCard(bool isGood = true) {
        bool playSound1 = Random.Range(0, 1f) > 0.5f;
        
        if (isGood) {
            if (playSound1) {
                PlayWithRandomRange(sfxGoodCard1Source);
            } else {
                PlayWithRandomRange(sfxGoodCard2Source);
            }
        } else {
            if (playSound1) {
                PlayWithRandomRange(sfxBadcard1Source);
            } else {
                PlayWithRandomRange(sfxBadcard2Source);
            }
        }
    }

    public void OnClickButton() {
        PlayWithRandomRange(sfxEnterSource);
    }

    public void OnBuyCard() {
        PlayWithRandomRange(sfxBuySource);
    }

    public void OnDiscard() {
        PlayWithRandomRange(sfxDiscardSource);
    }

    public void OnWinGame() {
        sfxWinSource.Play();
    }

    public void OnLoseGame() {
        sfxLoseSource.Play();
    }

    public void OnCardHover() {
        PlayWithRandomRange(sfxMouseOverSource);
    }

    // public void OnGainCard() {
    //     int ran = Random.Range(0, 3);
    //     switch(ran) {
    //         case 0:
    //             PlayWithRandomRange(sfxPickup1Source);
    //             break;
    //         case 1:
    //             PlayWithRandomRange(sfxPickup2Source);
    //             break;
    //         case 2:
    //             PlayWithRandomRange(sfxPickup3Source);
    //             break;
    //         default:
    //             PlayWithRandomRange(sfxPickup1Source);
    //             break;
    //     }
    // }
}
