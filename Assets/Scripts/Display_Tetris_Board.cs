using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_Tetris_Board : MonoBehaviour {

    public GameObject GridTile;

    GameObject[,] TETRIS_BOARD = new GameObject[16, 10];

    float y = -3.5f;
    float x = -2;

    void Start() {

        float y = -3.5f;

        for (int row = 0; row < TETRIS_BOARD.GetLength(0); row++) {

            float x = -2;

            for (int collum = 0; collum < TETRIS_BOARD.GetLength(1); collum++) {

                TETRIS_BOARD[row, collum] = Instantiate(GridTile, new Vector3(x, y, 0), Quaternion.identity);
                x = x + (TETRIS_BOARD[row, collum].GetComponent<SpriteRenderer>().bounds.size.x); 

            } //end for

            y = y + (TETRIS_BOARD[row, 0].GetComponent<SpriteRenderer>().bounds.size.y);
            x = x - ((TETRIS_BOARD[row, 0].GetComponent<SpriteRenderer>().bounds.size.x) * TETRIS_BOARD.GetLength(0));

        }//end for
        

    }

    void Update() {
        TempUpdater();
    } //end update

    void TempUpdater() {

        TETRIS_BOARD[0, 0].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
        TETRIS_BOARD[1, 0].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
        TETRIS_BOARD[2, 0].GetComponent<GridBlockRenderer>().UpdateStatus("Green");
        TETRIS_BOARD[1, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Green");

        TETRIS_BOARD[0, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Red");
        TETRIS_BOARD[0, 2].GetComponent<GridBlockRenderer>().UpdateStatus("Red");
        TETRIS_BOARD[0, 3].GetComponent<GridBlockRenderer>().UpdateStatus("Red");
        TETRIS_BOARD[1, 2].GetComponent<GridBlockRenderer>().UpdateStatus("Red");

        TETRIS_BOARD[2, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Orange");
        TETRIS_BOARD[2, 2].GetComponent<GridBlockRenderer>().UpdateStatus("Orange");
        TETRIS_BOARD[2, 3].GetComponent<GridBlockRenderer>().UpdateStatus("Orange");
        TETRIS_BOARD[1, 3].GetComponent<GridBlockRenderer>().UpdateStatus("Orange");

    }

}
