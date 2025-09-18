using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public static MusicManager I;

    [Header("Clips")]
    public AudioClip menuClip;
    public AudioClip level1Clip;
    public AudioClip level2Clip;

    [Header("Options")]
    [Range(0f, 1f)] public float volume = 0.8f;
    public float fadeTime = 0.75f;

    AudioSource a, b;   // pour crossfade
    Dictionary<string, AudioClip> map;

    void Awake()
    {
        if (I) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        a = gameObject.AddComponent<AudioSource>();
        b = gameObject.AddComponent<AudioSource>();
        a.loop = b.loop = true;
        a.playOnAwake = b.playOnAwake = false;
        a.volume = b.volume = 0f;

        map = new Dictionary<string, AudioClip>
        {
            {"MainMenu", menuClip},
            {"Level 1", level1Clip},
            {"Level 2", level2Clip},
        };

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene s, LoadSceneMode _)
    {
        // Choix du clip selon le nom de scène (adapte si besoin)
        if (map.TryGetValue(s.name, out var clip) && clip)
            Play(clip);
    }

    public void Play(AudioClip clip)
    {
        if (!clip) return;
        // Choisit la source inactive pour la nouvelle musique
        var from = a.isPlaying ? a : b;
        var to = a.isPlaying ? b : a;

        to.clip = clip;
        to.volume = 0f;
        to.Play();
        StopAllCoroutines();
        StartCoroutine(Crossfade(from, to, fadeTime, volume));
    }

    IEnumerator Crossfade(AudioSource from, AudioSource to, float t, float targetVol)
    {
        float el = 0f;
        float startFrom = from ? from.volume : 0f;
        while (el < t)
        {
            el += Time.unscaledDeltaTime;
            float k = Mathf.Clamp01(el / t);
            if (from && from.isPlaying) from.volume = Mathf.Lerp(startFrom, 0f, k);
            to.volume = Mathf.Lerp(0f, targetVol, k);
            yield return null;
        }
        if (from) { from.Stop(); from.volume = 0f; }
        to.volume = targetVol;
    }

    // (Optionnel) appels manuels depuis des boutons si tu veux tester
    public void PlayMenu() { if (menuClip) Play(menuClip); }
    public void PlayL1() { if (level1Clip) Play(level1Clip); }
    public void PlayL2() { if (level2Clip) Play(level2Clip); }
}
