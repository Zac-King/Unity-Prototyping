﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : Observer, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public AudioClip a_hovered;
    public AudioClip a_clicked;
    private AudioSource a_source;
    public bool sound;

    public void Awake()
    {
        a_source = GetComponent<AudioSource>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayAudio(a_hovered);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayAudio(a_clicked);
        
        Debug.Log("button click");
    }

    private void PlayAudio(AudioClip clip)
    {
        if (sound)
        {
            if (a_source == null)
            {
                Debug.Log("no audio attached to " + name);
                return;
            }
            a_source.PlayOneShot(clip);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Animator>().SetTrigger("Normal");
    }
}