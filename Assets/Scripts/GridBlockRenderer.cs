using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBlockRenderer : MonoBehaviour {

    SpriteRenderer Sprite_Renderer;

    public Sprite MySprite;

    private Vector2 BlockSize;

    // Start is called before the first frame update
    void Start() {
        Sprite_Renderer = gameObject.GetComponent<SpriteRenderer>();
        Sprite_Renderer.sprite = MySprite;

        BlockSize = Sprite_Renderer.size;
        Debug.Log(Sprite_Renderer.size);
    }

    public float GetSize() {

        return BlockSize[0];
    }

}
