using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsDead { get; private set; } = false;
    public SOStats SOStats;
    public string explosionLayer = "Explosion";
    public string PlayerKey {  get; private set; }

    [Header("Inputs")]
    public KeyCode KeyCodeUp = KeyCode.W;
    public KeyCode KeyCodeDown = KeyCode.S;
    public KeyCode KeyCodeLeft = KeyCode.A;
    public KeyCode KeyCodeRight = KeyCode.D;
     
    [Header("SpriteAnimation")]
    [SerializeField] private List<AnimatedSpriteRenderer> _animatedSpriteRenderers;
    private AnimatedSpriteRenderer _currentSpriteRenderer;

    [Header("Movement")]
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection = Vector2.down; //start sprite

    private void Awake()
    {
        OnValidate();
        SOStats.ResetSO();
        PlayerKey = Guid.NewGuid().ToString();
    }

    private void Start()
    {
        _currentSpriteRenderer = _animatedSpriteRenderers.Find(i => i.PlayerSpriteType == SpritePlayerType.DOWN);
    }

    private void OnValidate()
    {
        if (_rigidbody2D == null) _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
    }

    private void FixedUpdate()
    {
        if (!IsDead)
        {
            Vector2 position = _rigidbody2D.position;
            Vector2 translation = SOStats.Speed * Time.fixedDeltaTime * _moveDirection;
            _rigidbody2D.MovePosition(position + translation);
        }
    }

    private void GatherInput()
    {

        if (!IsDead)
        {
            if (Input.GetKey(KeyCodeUp))
            {
                SetDirection(Vector2.up);
                HandleAnimation(SpritePlayerType.UP);
            }
            else if (Input.GetKey(KeyCodeDown))
            {
                SetDirection(Vector2.down);
                HandleAnimation(SpritePlayerType.DOWN);
            }
            else if (Input.GetKey(KeyCodeLeft))
            {
                SetDirection(Vector2.left);
                HandleAnimation(SpritePlayerType.LEFT);
            }
            else if (Input.GetKey(KeyCodeRight))
            {
                SetDirection(Vector2.right);
                HandleAnimation(SpritePlayerType.RIGHT);
            }
            else
            {
                _moveDirection = Vector2.zero;
                HandleAnimation(SpritePlayerType.IDLE);
            }
        }
    }

    private void SetDirection(Vector2 newDirection)
    {
        _moveDirection = newDirection;
    }

    private void HandleAnimation(SpritePlayerType type)
    {

        foreach (AnimatedSpriteRenderer sr in _animatedSpriteRenderers)
        {
            var a = AnimatedSpriteRendererManager.Instance.GetSpriteRendererByType(PlayerKey, sr.PlayerSpriteType);
            if (a == null) return;
            if (a.Type == type)
            {
                a.AnimatedSpriteRenderer.enabled = true;
                _currentSpriteRenderer = a.AnimatedSpriteRenderer;
            }
            else
                a.AnimatedSpriteRenderer.enabled = false;
        }

        _currentSpriteRenderer.Idle = type == SpritePlayerType.IDLE;
        if (_currentSpriteRenderer.Idle)
            _currentSpriteRenderer.enabled = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(explosionLayer))
            KillPlayer();
    }

    private void KillPlayer()
    {
        IsDead = true;
        GetComponent<BombController>().enabled = false;
        HandleAnimation(SpritePlayerType.DEAD);
        GameManager.Instance.CheckWinState();
    }

}