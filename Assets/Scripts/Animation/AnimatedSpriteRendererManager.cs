using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class AnimatedSpriteRendererManager : Singleton<AnimatedSpriteRendererManager>
{

    [SerializeField] private List<SpriteRendererSetup> _renderers;

    public void AssociateRenderes(SpriteRendererType type, AnimatedSpriteRenderer renderer)
    {
        _renderers.ForEach(i => { if(i.Type == type) i.AnimatedSpriteRenderer = renderer; });
    }


    public SpriteRendererSetup GetSpriteRendererByType(SpriteRendererType type) => _renderers.Find(i => i.Type == type);


}

public enum SpriteRendererType
{
    IDLE,
    UP,
    DOWN,
    LEFT,
    RIGHT,
    BOMB,
    START_EXPLIOSION,
    MIDDLE_EXPLIOSION,
    END_EXPLIOSION,
    BRICK_DESTROYED
}

[System.Serializable]
public class SpriteRendererSetup
{
    public SpriteRendererType Type;
    public AnimatedSpriteRenderer AnimatedSpriteRenderer;
}
