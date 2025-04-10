using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public SpriteDefinition NullDefinition = new SpriteDefinition(); 

    public List<SpriteDefinition> Sprites = new List<SpriteDefinition>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSprites()
    {
        var sprites = Resources.LoadAll<Sprite>("Unicode");
        var text = Resources.Load<TextAsset>("Unicode/ImageAssignment");
        var lines = text.text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        foreach (var line in lines)
        {
            var split = line.Split(':');

            if (split.Length < 2 || split[0][0] == '-')
                continue;

            var sprite = sprites.FirstOrDefault(x => x.name == split[1]);
            if (sprite == null)
                continue;

            var newSprite = Sprite.Create(sprite.texture, sprite.rect, sprite.pivot); 
            newSprite.name = split[0];

            var spriteDef = new SpriteDefinition();
            spriteDef.Name = split[0]; 
            spriteDef.Sprite = sprite;

            if (split.Length >= 3)
            {
                spriteDef.Color = GetColor(split[2]);
            }
            else
            {
                spriteDef.Color = Color.white;
            }

            if (newSprite.name == "null")
                NullDefinition = spriteDef;

            Sprites.Add(spriteDef);
        }


    }

    public Color GetColor(string name)
    {
        switch (name)
        {
            case "magenta":
                return Color.magenta;

            case "green":
                return Color.green;
            case "grey":
            case "gray":
                return Color.gray;
            case "blue":
                return Color.blue;
            case "yellow":
                return Color.yellow;
            case "red":
                return Color.red;
            case "cyan":
                return Color.cyan;

            case "white":
            default:
                return Color.white;
        }
    }

    public SpriteDefinition GetSprite(string name)
    {
        var retval = Sprites.FirstOrDefault(x => x.Name == name);

        if (retval == null)
            return NullDefinition;
        return retval;

    }
}
