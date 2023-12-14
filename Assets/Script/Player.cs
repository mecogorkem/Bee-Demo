using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int flower => _flower;
    public int reachedLevel => _reachedLevel;
    public bool sound => _sound;
    
    private int _flower;
    private int _reachedLevel;
    private bool _sound;
    public List<SkinType> availableSet;
    public SkinType activeSkin;

    public int MaxLevel;

    public Player()
    {
        _flower = 0;
        _reachedLevel = 1;
        _sound = true;
        MaxLevel = 10;
        activeSkin = SkinType.Yellow;
        availableSet = new List<SkinType>
        {
            SkinType.Yellow,
            SkinType.Red
        };
    }

    public void AddFlower(int extra)
    {
        _flower += extra;
    }
    public void UseFlower(int price)
    {
        _flower -= price;
    }

    public void LevelUp(int level)
    {
        _reachedLevel = (level > reachedLevel) ? level : reachedLevel;
    }

    public void ChangeSoundStatues()
    {
        _sound = !sound;
    }

 

}
