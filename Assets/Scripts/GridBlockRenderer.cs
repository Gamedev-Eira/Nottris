using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBlockRenderer : MonoBehaviour {

    SpriteRenderer Sprite_Renderer;

    private Sprite MySprite;
    private string TileStatus = "Empty";

    public Sprite RedSprite;
    public Sprite OrangeSprite;
    public Sprite YellowSprite;
    public Sprite GreenSprite;
    public Sprite LblueSprite;
    public Sprite DblueSprite;
    public Sprite PurpleSprite;
    public Sprite GraySprite;
    public Sprite EmptySprite;

    private Vector2 BlockSize;

    // Start is called before the first frame update
    void Start() {
        Sprite_Renderer = gameObject.GetComponent<SpriteRenderer>();
        RenderTile(EmptySprite);
    }

    void Update() {
        switch(TileStatus) {
            case "Empty":
                RenderTile(EmptySprite);
                break;
            case "Red":
                RenderTile(RedSprite);
                break;
            case "Orange":
                RenderTile(OrangeSprite);
                break;
            case "Yellow":
                RenderTile(YellowSprite);
                break;
            case "Green":
                RenderTile(GreenSprite);
                break;
            case "Light Blue":
                RenderTile(LblueSprite);
                break;
            case "Dark Blue":
                RenderTile(DblueSprite);
                break;
            case "Purple":
                RenderTile(PurpleSprite);
                break;
            case "Gray":
                RenderTile(GraySprite);
                break;
            default:
                RenderTile(EmptySprite);
                break;
        }
    }

    private void RenderTile(Sprite sprite) {
        Sprite_Renderer.sprite = sprite;
    }

    public void UpdateStatus(string NewStatus) {
        TileStatus = NewStatus;
    }

}
