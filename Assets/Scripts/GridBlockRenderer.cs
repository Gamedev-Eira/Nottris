using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBlockRenderer : MonoBehaviour {

    //Sprite renderer because apparently I need this
    SpriteRenderer Sprite_Renderer;

    //String that lets the tile easily track and inform other classes of it's current status
    private string TileStatus = "Empty";

    //Gets all the sprites it needs
    public Sprite RedSprite;
    public Sprite OrangeSprite;
    public Sprite YellowSprite;
    public Sprite GreenSprite;
    public Sprite LblueSprite;
    public Sprite DblueSprite;
    public Sprite PurpleSprite;
    public Sprite GraySprite;

    private Vector2 BlockSize;

    void Awake() {
        Sprite_Renderer = gameObject.GetComponent<SpriteRenderer>();
        UpdateStatus("Empty");
    }

    private void RenderTile(Sprite sprite) {
        Sprite_Renderer.sprite = sprite; //renders the GameObject with the sprite passed to it.
    }//end void

    public void UpdateStatus(string NewStatus) {
        //Gets it's new status and makes TileStatus equal to it
        TileStatus = NewStatus;

        if(TileStatus == "Empty" || TileStatus == "True Empty") {
            Sprite_Renderer.enabled = false;
        } else {
            Sprite_Renderer.enabled = true;

            //then uses this switch to update it's appearence
            switch (TileStatus) {
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
                    Debug.Log("Oops!");
                    break;
            }
        }
    }

    public string ReportStatus() {  //literally just returns it's status. EZ.
        return TileStatus;
    }

    public void RenderCustomSprite(Sprite ImportedSprite) {
        Sprite_Renderer.enabled = true;
        RenderTile(ImportedSprite);
    }

}