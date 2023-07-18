using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino_T : MonoBehaviour {

    public GameObject GridTile;

    private float PositionX = 0;
    private float PositionY = 0;

    private int RotationValue = 1;

    private const int Dimensions = 3;
    private const int ShapeQuantity = 4;

    private string Colour = "Green";

    GameObject[,] Tetromino;

    bool[,,] Shape = new bool[ShapeQuantity, Dimensions, Dimensions]
    {
        {
            {false, true , false},
            {true , true , true },
            {false, false, false}
        },

        {
            {false, true , false},
            {false, true , true },
            {false, true , false}
        },

        {
            {false, false, false},
            {true , true , true },
            {false, true , false}
        },

        {
            {false, true , false},
            {true , true , false},
            {false, true , false}
        }
    };



    ///////////////////////////////////////////////////////

    void Start() {

        for (int row = 0; row < Tetromino.GetLength(0); row++) {
            for (int collum = 0; collum < Tetromino.GetLength(1); collum++) {

                Tetromino[row, collum] = Instantiate(GridTile, new Vector3(PositionX, PositionY, 0), Quaternion.identity);
                PositionX = PositionX + (Tetromino[row, collum].GetComponent<SpriteRenderer>().bounds.size.x);

            } //end for

            PositionY = PositionY + (Tetromino[row, 0].GetComponent<SpriteRenderer>().bounds.size.y);
            PositionX = PositionX - ((Tetromino[row, 0].GetComponent<SpriteRenderer>().bounds.size.x) * Tetromino.GetLength(0));

        }//end for

    }//end initalise

    ///////////////////////////////////////////////////////

    void UpdateRotation() {

        for (int row = 0; row < Dimensions; row++) {

            for (int column = 0; column < Dimensions; column++) {

                if(Shape[(RotationValue - 1), row, column] == true) {
                    Tetromino[row, column].GetComponent<GridBlockRenderer>().UpdateStatus(Colour);
                }
                else if (Shape[(RotationValue - 1), row, column] == false) {
                    Tetromino[row, column].GetComponent<GridBlockRenderer>().UpdateStatus("Empty");
                } //end if else

            }//end for
        }//end for
    }//end void

    void ResetToEmpty() {

        float temp = PositionY;

        for (int row = 0; row < Tetromino.GetLength(0); row++) {
            for (int collum = 0; collum < Tetromino.GetLength(1); collum++) {

                Tetromino[row, collum].GetComponent<GridBlockRenderer>().UpdateStatus("Empty");
                PositionX = PositionX + (Tetromino[row, collum].GetComponent<SpriteRenderer>().bounds.size.x);

            } //end for

            PositionY = PositionY + (Tetromino[row, 0].GetComponent<SpriteRenderer>().bounds.size.y);
            PositionX = PositionX - ((Tetromino[row, 0].GetComponent<SpriteRenderer>().bounds.size.x) * Tetromino.GetLength(0));

        }//end for

        PositionY = temp;

    }//end void

    int Rotator(bool clockwise, int rotate) {

        if (clockwise == true && rotate < 4) {
            rotate++;
            return rotate;
        } else if (clockwise == true && rotate >= 4) {
            return 1;
        } else if (clockwise == false && rotate > 1) {
            rotate--;
            return rotate;
        } else if (clockwise == false && rotate <= 1) {
            return 4;
        } else {
            Debug.Log("Error rotating! Clockwise is " + clockwise + " and rotate is " + rotate + ". Returning 0.");
            return 0;
        }//end if else

    }//end rotator

    void DoRotation(bool Clockwise) {
        ResetToEmpty();
        RotationValue = Rotator(Clockwise, RotationValue);
        UpdateRotation();

    }//end void

    ///////////////////////////////////////////////////////

    void Update() {

        if (Input.GetMouseButtonDown(0)) { DoRotation(false); }
        else if (Input.GetMouseButtonDown(1)) { DoRotation(true); }

    } //end update

}//end class

