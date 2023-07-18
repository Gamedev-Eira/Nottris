using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tTetromino : MonoBehaviour
{

    public GameObject GridTile;

    private float PositionX = 0;
    private float PositionY = 0;

    private int RotationValue = 1;

    GameObject[,] Tetromino = new GameObject[3, 3];


    void Start() {

        //float temp = PositionY;

        for (int row = 0; row < Tetromino.GetLength(0); row++) {
            for (int collum = 0; collum < Tetromino.GetLength(1); collum++) {

                Tetromino[row, collum] = Instantiate(GridTile, new Vector3(PositionX, PositionY, 0), Quaternion.identity);
                PositionX = PositionX + (Tetromino[row, collum].GetComponent<SpriteRenderer>().bounds.size.x);

            } //end for

            PositionY = PositionY + (Tetromino[row, 0].GetComponent<SpriteRenderer>().bounds.size.y);
            PositionX = PositionX - ((Tetromino[row, 0].GetComponent<SpriteRenderer>().bounds.size.x) * Tetromino.GetLength(0));

        }//end for

        //PositionY = temp;

        UpdateRotation();

    }//end start

    void Update()
    {

        if (Input.GetMouseButtonDown(0)) { DoRotation(false); }
        else if (Input.GetMouseButtonDown(1)) { DoRotation(true); }

    } //end update

    void UpdateRotation() {
        switch (RotationValue) {
            case 1:
                Tetromino[1, 0].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                Tetromino[1, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                Tetromino[1, 2].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                Tetromino[2, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                break;
            case 2:
                Tetromino[0, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                Tetromino[1, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                Tetromino[2, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                Tetromino[1, 2].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                break;
            case 3:
                Tetromino[1, 0].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                Tetromino[1, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                Tetromino[1, 2].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                Tetromino[0, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                break;
            case 4:
                Tetromino[0, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                Tetromino[1, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                Tetromino[2, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                Tetromino[1, 0].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
                break;
        }//end switch
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

}//end class
