using DG.Tweening;
using UnityEngine;
using System.Collections;

public class AudioManager : OSK.SingletonMono<AudioManager>
{
    [Header("Sfx")]
    public AudioClip[] soundSFX;

    [Header("Music")]
    [Space]
    public AudioClip[] themeMusics;

    [Space]
    public AudioSource musicSource;
    public AudioSource soundSource;
    
    private int playing;
    private bool canPlay = true;

    public void PlayMusic(string name, float volume = 1, bool isloop = true)
    {
        AudioClip s = System.Array.Find(themeMusics, sound => sound.name == name);
        if (s != null)
        {
            musicSource.clip = s;
            musicSource.loop = isloop;
            musicSource.Play();
            musicSource.volume = volume;
        }
    }

    public void PlayOneShot(string name, float volume = 1, float delayPlay = 0, float pitch = 1)
    {
        if (playing > 5 && canPlay) return;

        StartCoroutine(PlayClipByName(name, volume, delayPlay, pitch));
        canPlay = false;
    }
    private IEnumerator PlayClipByName(string name, float volume = 1, float delayPlay = 0, float pitch = 1)
    {
        AudioClip s = System.Array.Find(soundSFX, sound => sound.name == name);
        yield return new WaitForSeconds(delayPlay);

        if (s != null)
        {
            playing++;
            canPlay = true;
            soundSource.clip = s;
            soundSource.pitch = pitch;
            soundSource.PlayOneShot(s, volume);
            yield return new WaitForSeconds(Random.Range(0, 0.2f));
            playing--;
        }
        yield return new WaitForSeconds(Random.Range(0, 0.2f));
    }

    public void SetVolumeMusic(float volum, float timeFade, float delaySet = 0)
    {
        DOVirtual.Float(musicSource.volume, volum, timeFade,
         v => musicSource.volume = v).SetEase(Ease.Linear).SetDelay(delaySet);
    }

    public void PlayOneShotByClip(AudioClip clipSFX, float volume = 1, float _delayPlay = 0, float pitch = 1)
    {
        if (playing > 10 && canPlay) return;

        StartCoroutine(PlayClipByClip(clipSFX, volume, _delayPlay, pitch));
        canPlay = false;
    }

    private IEnumerator PlayClipByClip(AudioClip clipSFX, float _volume, float _delayPlay = 0, float pitch = 1)
    {
        yield return new WaitForSeconds(Random.Range(0, 0.15f));

        if (clipSFX != null)
        {
            playing++;
            canPlay = true;
            soundSource.clip = clipSFX;
            soundSource.pitch = pitch;
            var vl = _volume - (0.1f * playing);
            soundSource.PlayOneShot(clipSFX, vl > 0 ? vl : 0.5f);
            yield return new WaitForSeconds(Random.Range(0, 0.2f));
            playing--;
        }
        yield return new WaitForSeconds(Random.Range(0, 0.2f));
    }

    public void Stop()
    {
        musicSource.Stop();
        soundSource.Stop();
    }

    public void Pause()
    {
        musicSource.Pause();
        soundSource.Pause();
    }

    public void Resume()
    {
        musicSource.Play();
        soundSource.Play();
    }
}
