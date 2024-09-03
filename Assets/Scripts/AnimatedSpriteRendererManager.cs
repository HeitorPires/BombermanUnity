using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class AnimatedSpriteRendererManager : Singleton<AnimatedSpriteRendererManager>
{

    [SerializeField] private List<SpriteRendererSetup> _renderers;

    public SpriteRendererSetup GetSpriteRendererByType(SpriteRendererType type) => _renderers.Find(i => i.Type == type);


}

public enum SpriteRendererType
{
    IDLE,
    UP,
    DOWN,
    LEFT,
    RIGHT,
    BOMB
}

[System.Serializable]
public class SpriteRendererSetup
{
    public SpriteRendererType Type;
    public AnimatedSpriteRenderer AnimatedSpriteRenderer;
}
