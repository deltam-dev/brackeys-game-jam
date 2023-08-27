using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSkinController : MonoBehaviour
{

    public bool isShiny;
    private int skinNr;
    public Skins[] skins;
    SpriteRenderer spriteRenderer;
    public int spriteNr;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isShiny) skinNr = 0;
        else skinNr = skins.Length - 1;
    }

    void LateUpdate()
    {
        SkinChoice();
    }

    void SkinChoice()
    {
        if (spriteRenderer.sprite.name.Contains(gameObject.name))
        {
            string spriteName = spriteRenderer.sprite.name;
            spriteName = spriteName.Replace(gameObject.name+"_", "");
            spriteNr = int.Parse(spriteName);

            spriteRenderer.sprite = skins[skinNr].sprites[spriteNr];
        }
    }


}

[System.Serializable]
public struct Skins
{
    public Sprite[] sprites;
}
