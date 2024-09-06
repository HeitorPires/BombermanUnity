using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    public float AnimationTime = .25f;
    public Sprite IdleSprite;
    public SpriteItemsType ItemSpriteType;
    public SpritePlayerType PlayerSpriteType;
    public bool Idle;
    public bool isPlayer;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private bool _isLoop = true;
    private int _index = 0;

    private void Awake()
    {
        OnValidate();
        Invoke(nameof(Init), .2f);
    }

    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), AnimationTime, AnimationTime);
    }


    private void OnValidate()
    {
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();

        var p = GetComponentInParent<Player>();
        if(p != null)
            isPlayer = true;
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
        if(isPlayer)
        {
            string name = GetComponentInParent<Player>().PlayerKey;
            AnimatedSpriteRendererManager.Instance.AssociateRenderes(PlayerSpriteType, this, name);
        }
        else
            AnimatedSpriteRendererManager.Instance.AssociateRenderes(ItemSpriteType, this);
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
