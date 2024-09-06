using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SOStats SOStats;

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
        _currentSpriteRenderer = _animatedSpriteRenderers.Find(i => i.SpriteRendererType == SpriteRendererType.DOWN);
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
        Vector2 position = _rigidbody2D.position;
        Vector2 translation = SOStats.Speed * Time.fixedDeltaTime * _moveDirection;
        _rigidbody2D.MovePosition(position + translation);
    }

    private void GatherInput()
    {
        if (Input.GetKey(KeyCodeUp))
        {
            SetDirection(Vector2.up);
            HandleAnimation(SpriteRendererType.UP);
        }
        else if (Input.GetKey(KeyCodeDown))
        {
            SetDirection(Vector2.down);
            HandleAnimation(SpriteRendererType.DOWN);
        }
        else if (Input.GetKey(KeyCodeLeft))
        {
            SetDirection(Vector2.left);
            HandleAnimation(SpriteRendererType.LEFT);
        }
        else if (Input.GetKey(KeyCodeRight))
        {
            SetDirection(Vector2.right);
            HandleAnimation(SpriteRendererType.RIGHT);
        }
        else
        {
            _moveDirection = Vector2.zero;
            HandleAnimation(SpriteRendererType.IDLE);
        }
    }

    private void SetDirection(Vector2 newDirection)
    {
        _moveDirection = newDirection;
    }

    private void HandleAnimation(SpriteRendererType type)
    {

        foreach (AnimatedSpriteRenderer sr in _animatedSpriteRenderers)
        {
            var a = AnimatedSpriteRendererManager.Instance.GetSpriteRendererByType(sr.SpriteRendererType);
            if (a.Type == type)
            {
                a.AnimatedSpriteRenderer.enabled = true;
                _currentSpriteRenderer = a.AnimatedSpriteRenderer;
            }
            else
                a.AnimatedSpriteRenderer.enabled = false;
        }

        _currentSpriteRenderer.Idle = type == SpriteRendererType.IDLE;
        if (_currentSpriteRenderer.Idle)
            _currentSpriteRenderer.enabled = true;

    }
}


public struct Stats
{

}