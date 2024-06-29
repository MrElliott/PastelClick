using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float pointerDownTimer;

    public float requireHoldTime;

    public UnityEvent onLongClick;

    [SerializeField] private Image fillImage;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        Debug.Log("OnPointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
        Debug.Log("OnPointerUp");
    }

    private void Update()
    {
        if (!pointerDown) return;
        
        pointerDownTimer += Time.deltaTime;
        if (pointerDownTimer > requireHoldTime)
        {
            if (onLongClick != null)
                onLongClick.Invoke();

            Reset();
        }
        SetFill();
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0f;
        SetFill();
    }

    private void SetFill()
    {
        float fillAmount = pointerDownTimer / requireHoldTime;
        fillImage.fillAmount = fillAmount;
    }
}
