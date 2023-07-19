using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino_Base : MonoBehaviour {

    public GameObject GridTile;

    private float PositionX = 0;
    private float PositionY = 0;

    private int RotationValue = 4;

    private const int Dimensions = 4;
    private const int ShapeQuantity = 4;

    private string Colour = "Empty";

    private GameObject[,] Tetromino = new GameObject[4, 4];

    bool[,,] Tet_Shape;

    ///////////////////////////////////////////////////////

    void Awake() {
        for (int row = 0; row < Dimensions; row++) {
            for (int collum = 0; collum < Dimensions; collum++) {

                Tetromino[row, collum] = Instantiate(GridTile, new Vector3(PositionX, PositionY, 0), Quaternion.identity);
                PositionX = PositionX + (Tetromino[row, collum].GetComponent<SpriteRenderer>().bounds.size.x);

            } //end for

            PositionY = PositionY + (Tetromino[0, 0].GetComponent<SpriteRenderer>().bounds.size.y);
            PositionX = PositionX - ((Tetromino[0, 0].GetComponent<SpriteRenderer>().bounds.size.x) * Tetromino.GetLength(0));

        }//end for

    }//end start

    ///////////////////////////////////////////////////////

    public void init(string colour, bool[,,] shape) {
        Colour = colour;
        Tet_Shape = shape;

    }//end init

    void UpdateRotation() {

        for (int row = 0; row < Dimensions; row++) {

            for (int column = 0; column < Dimensions; column++) {
                if(Tet_Shape[(RotationValue - 1), column, row] == true) {
                    Tetromino[column, row].GetComponent<GridBlockRenderer>().UpdateStatus(Colour);
                }
                else if (Tet_Shape[(RotationValue - 1), column, row] == false) {
                    Tetromino[column, row].GetComponent<GridBlockRenderer>().UpdateStatus("Empty");
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

        RotationValue = Rotator(Clockwise, RotationValue);
        UpdateRotation();

    }//end void

    ///////////////////////////////////////////////////////

    void Update() {
        Debug.Log(RotationValue - 1);
        UpdateRotation();

        if (Input.GetMouseButtonDown(0)) { DoRotation(true); }
        else if (Input.GetMouseButtonDown(1)) { DoRotation(false); }
        

    } //end update

}//end class

