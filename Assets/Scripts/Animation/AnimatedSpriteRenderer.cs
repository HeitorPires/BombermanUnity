using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    public float AnimationTime = .25f;
    public Sprite IdleSprite;
    public SpriteRendererType SpriteRendererType;
    public bool Idle;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private bool _isLoop = true;
    private int _index = 0;

    private void Awake()
    {
        OnValidate();
    }

    private void Start()
    {
        Init();
        InvokeRepeating(nameof(NextFrame), AnimationTime, AnimationTime);
    }


    private void OnValidate()
    {
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        _spriteRenderer.enabled = false;
    }

    void Init()
    {
        AnimatedSpriteRendererManager.Instance.AssociateRenderes(SpriteRendererType, this);
    }

    private void NextFrame()
    {
        _index++;
        if (_isLoop && _index >= _sprites.Count)
            _index = 0;

        if (Idle)
            _spriteRenderer.sprite = IdleSprite;
        else if(_index <  _sprites.Count)
            _spriteRenderer.sprite = _sprites[_index];
    }

}
