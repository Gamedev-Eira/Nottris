using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tTetromino : MonoBehaviour {

    public GameObject GridTile;

    public float PositionX = 0;
    public float PositionY = 0;

    private int RotationValue = 4;

    GameObject[,] Tetromino = new GameObject[3, 3];


    void Start() {

        ResetToEmpty();
        UpdateRotation();

    }//end start

    void UpdateRotation() {
        Debug.Log(RotationValue);
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
        }
    }

    void ResetToEmpty() {
        
        for (int row = 0; row < Tetromino.GetLength(0); row++) {
            for (int collum = 0; collum < Tetromino.GetLength(1); collum++) {

                Tetromino[row, collum] = Instantiate(GridTile, new Vector3(PositionX, PositionY, 0), Quaternion.identity);
                PositionX = PositionX + (Tetromino[row, collum].GetComponent<SpriteRenderer>().bounds.size.x);

            } //end for

            PositionY = PositionY + (Tetromino[row, 0].GetComponent<SpriteRenderer>().bounds.size.y);
            PositionX = PositionX - ((Tetromino[row, 0].GetComponent<SpriteRenderer>().bounds.size.x) * Tetromino.GetLength(0));

        }//end for
    }//end void

}//end class
