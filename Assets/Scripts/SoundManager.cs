using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Collections;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;

    public void SelectAudio(AudioSource audioSource, int indice, float volumen)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audios[indice], volumen);
        }
    }
    public void SelectAudio(AudioSource audioSource, AudioClip audioClip, float volumen)
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip, volumen);
        }
    }

    public IEnumerator SelectAudioWithDelay(AudioSource audioSource, int indice, float volumen, float spatialBlend, float spread, float minDist, float maxDist, float delay)
    {
        yield return PlayAudioWithDelay(audioSource, indice, volumen, spatialBlend, spread, minDist, maxDist, AudioRolloffMode.Logarithmic, delay);
    }
    
    private void SetAudioProperties(AudioSource audioSource, float spatialBlend, float spread, float minDist, float maxDist, AudioRolloffMode rolloffMode)
    {
        if (audioSource == null) return;

        audioSource.spatialBlend = spatialBlend;
        audioSource.spread = spread;
        audioSource.minDistance = minDist;
        audioSource.maxDistance = maxDist;
        audioSource.rolloffMode = rolloffMode;
    }
    
    private IEnumerator PlayAudioWithDelay(AudioSource audioSource, int indice, float volumen, float spatialBlend, float spread, float minDist, float maxDist, AudioRolloffMode rolloffMode, float delay)
    {
        while (audioSource != null)
        {
            SetAudioProperties(audioSource, spatialBlend, spread, minDist, maxDist, rolloffMode);
            audioSource.PlayOneShot(audios[indice], volumen);
            
            yield return new WaitForSeconds(audios[indice].length);
            
            yield return new WaitForSeconds(delay);
        }
    }
}