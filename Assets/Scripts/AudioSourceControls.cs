using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceControls : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField, Range(0f, 1f)] private float starTime = 0;
    [SerializeField, Range(0f, 1f)] private float debugCurrentSeekTime;

    private void Start()
    {
        audioSource.time = starTime * audioSource.clip.length;
    }

    private void OnValidate()
    {
        audioSource.time = debugCurrentSeekTime * audioSource.clip.length;
    }

    private void Update()
    {
        debugCurrentSeekTime = audioSource.time / audioSource.clip.length;
    }
}
