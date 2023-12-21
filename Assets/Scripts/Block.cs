using System;
using UnityEngine;
using TMPro;
public class Block : MonoBehaviour
{
    private int _hitsRemaining = 1;
    private SpriteRenderer _spriteRenderer;
    private TextMeshPro _textMeshPro;
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _textMeshPro = GetComponentInChildren<TextMeshPro>();
        UpdateVisualState();
    }
    void UpdateVisualState()
    {
        _textMeshPro.SetText(_hitsRemaining.ToString());
        _spriteRenderer.color = Color.Lerp(Color.white,Color.red, _hitsRemaining / 10f);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        _hitsRemaining--;
        if (_hitsRemaining > 0)
        {
            UpdateVisualState();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    internal void SetHits(int hits)
    {
        _hitsRemaining = hits;
        UpdateVisualState();
    }
}
