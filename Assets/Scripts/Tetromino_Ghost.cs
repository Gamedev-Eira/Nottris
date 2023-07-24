using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino_Ghost : MonoBehaviour {

    public GameObject GridTile;     //Gets a reference to the GameObject found in GridBlockRenderer

    private float PositionX = 0;    //The X and Y positions ther GridTiles are spawned from 
    private float PositionY = 0;

    private const int Dimensions = 4;   //The dimensions of the Tetromino grid
    private const int ShapeQuantity = 4;//The amoung of rotations a Tetromino can do

    private string Colour = "Empty";    //Colour tracks the colour of the tetromino currently being rendered

    private GameObject[,] Ghost = new GameObject[4, 4]; //Array of GameObject (GridTile) pointers

    private bool[,] Tet_Shape;     //A @D array (4x4) that stores the shape of the current tetromino

    private float[] Boundaries;     //stores the left, right, and bottom boundaries of the playfield (in that order)

    public Sprite RedGhost;
    public Sprite OrangeGhost;
    public Sprite YellowGhost;
    public Sprite GreenGhost;
    public Sprite LblueGhost;
    public Sprite DblueGhost;
    public Sprite PurpleGhost;

    ///////////////////////////////////////////////////////

    void Awake() {  //DO NOT MOVE THIS FROM AWAKE. EVERYTHING WILL BREAK

        //For loop goes through 2D array and Instansiates a GridTile in each entry in the array

        Vector3 Position = GameObject.Find("Tetris_Board_Empty").GetComponent<Display_Tetris_Board>().ReturnStartingPosition();
        PositionX = Position[0];
        PositionY = Position[1];

        for (int row = 0; row < Dimensions; row++) {
            for (int collum = 0; collum < Dimensions; collum++) {

                Ghost[row, collum] = Instantiate(GridTile, new Vector3(PositionX, PositionY, 0), Quaternion.identity);
                //Ghost[row, collum].GetComponent<GridBlockRenderer>().UpdateStatus("Empty");

                //Gets the size of the created GridTile and increments PositionX by it's value
                //This places the next GridTile next to the previous one
                PositionX = PositionX + (Ghost[row, collum].GetComponent<SpriteRenderer>().bounds.size.x);

            } //end for

            //Same deal for Y as with X
            PositionY = PositionY + (Ghost[0, 0].GetComponent<SpriteRenderer>().bounds.size.y);

            //X is now reset to it's previous starting value
            PositionX = PositionX - ((Ghost[0, 0].GetComponent<SpriteRenderer>().bounds.size.x) * Ghost.GetLength(0));

        }//end for

        //InvokeRepeating("UpdatePositionSoftdrop", 0.0f, 1.0f);

    }//end Awake

    public void RenderGhost(bool[,,] shape, string colour, Vector3 position, int rotate) {

        Sprite sprite;

        switch(colour) {
            case "Red":
                sprite = RedGhost;
                break;
            case "Orange":
                sprite = OrangeGhost;
                break;
            case "Yellow":
                sprite = YellowGhost;
                break;
            case "Green":
                sprite = GreenGhost;
                break;
            case "Light Blue":
                sprite = LblueGhost;
                break;
            case "Dark Blue":
                sprite = DblueGhost;
                break;
            case "Purple":
                sprite = PurpleGhost;
                break;
            default:
                sprite = RedGhost;
                break;
        }

        for (int row = 0; row < Ghost.GetLength(0); row++) {
            for (int collum = 0; collum < Ghost.GetLength(1); collum++) {

                if (shape[rotate, row, collum] == true) {
                    Ghost[row, collum].GetComponent<GridBlockRenderer>().RenderCustomSprite(sprite);
                } else {
                    Ghost[row, collum].GetComponent<GridBlockRenderer>().UpdateStatus("Empty");
                }

                Ghost[row, collum].transform.position = position;
                position[0] += Ghost[0, 0].GetComponent<SpriteRenderer>().bounds.size.x;
            } //end for

            position[1] += Ghost[0, 0].GetComponent<SpriteRenderer>().bounds.size.y;
            position[0] -= Ghost[0, 0].GetComponent<SpriteRenderer>().bounds.size.x * Ghost.GetLength(0);

        }//end for

        

    }//end void

    }//end class