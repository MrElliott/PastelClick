using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    private float _TargetScale;
    private float _TargetTime;
    private float _CurrentTime;

    private SpriteRenderer _sr;
    private Color _c;

    private bool _Fading = false;
    
    public void BeginFade(Color c, float targetScale, float time)
    {
        _sr = GetComponent<SpriteRenderer>();
        _c = c;
        _sr.color = _c;
        transform.localScale = new Vector3(0, 0, 0);
        _TargetScale = targetScale;
        _TargetTime = time;
        _CurrentTime = 0f;
        _Fading = true;
    }

    private void Update()
    {
        if (!_Fading)
            return;
        
        float scaleVal = Mathf.Lerp(0, _TargetScale, _CurrentTime / _TargetScale);
        transform.localScale = new Vector3(scaleVal, scaleVal, scaleVal);
        _c.a = Mathf.Lerp(0, 1,  _CurrentTime/_TargetTime);
        _sr.color = _c;
        _CurrentTime += Time.deltaTime;

        if (_CurrentTime >= _TargetTime)
        {
            Destroy(transform.gameObject);
        }
    }
}
