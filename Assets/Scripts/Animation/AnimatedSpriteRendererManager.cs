using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class AnimatedSpriteRendererManager : Singleton<AnimatedSpriteRendererManager>
{

    [SerializeField] private List<ItemSpriteSetup> _itemsRenderersList;
    private Dictionary<string, List<PlayerSpriteSetup>> _playerRenderersDict;

    protected override void Awake()
    {
        base.Awake();
        _playerRenderersDict = new Dictionary<string, List<PlayerSpriteSetup>>();
    }

    public void AssociateRenderes(SpriteItemsType type, AnimatedSpriteRenderer renderer)
    {
        _itemsRenderersList.ForEach(i => { if (i.Type == type) i.AnimatedSpriteRenderer = renderer; });
    }

    public void AssociateRenderes(SpritePlayerType type, AnimatedSpriteRenderer renderer, string name)
    {
        PlayerSpriteSetup pss = new()
        {
            Type = type,
            AnimatedSpriteRenderer = renderer
        };

        if (_playerRenderersDict.ContainsKey(name))
            _playerRenderersDict[name].Add(pss);
        else
            _playerRenderersDict.Add(name, new List<PlayerSpriteSetup>() { pss });


    }

    public ItemSpriteSetup GetSpriteRendererByType(SpriteItemsType type) => _itemsRenderersList.Find(i => i.Type == type);

    public PlayerSpriteSetup GetSpriteRendererByType(string name, SpritePlayerType type)
    {
        if (_playerRenderersDict.ContainsKey(name))
        {
            foreach (PlayerSpriteSetup pss in _playerRenderersDict[name])
            {
                if (pss.Type == type) return pss;
            }
            Debug.Log(type + " Sem retorno");
        }
        return null;
    }


}


public enum SpritePlayerType
{
    NONE,
    IDLE,
    UP,
    DOWN,
    LEFT,
    RIGHT,
    DEAD
}

public enum SpriteItemsType
{
    NONE,
    BOMB,
    START_EXPLIOSION,
    MIDDLE_EXPLIOSION,
    END_EXPLIOSION,
    BRICK_DESTROYED,
}

[System.Serializable]
public class PlayerSpriteSetup
{
    public SpritePlayerType Type;
    public AnimatedSpriteRenderer AnimatedSpriteRenderer;
}

[System.Serializable]
public class ItemSpriteSetup
{
    public SpriteItemsType Type;
    public AnimatedSpriteRenderer AnimatedSpriteRenderer;
}

