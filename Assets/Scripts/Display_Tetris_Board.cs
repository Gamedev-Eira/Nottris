using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_Tetris_Board : MonoBehaviour {

    public GameObject GridTile; //Gets a reference to GridBlockRenderer

    //2D array stores pointers to game objects
    GameObject[,] TETRIS_BOARD = new GameObject[16, 10];

    //Starting position of the first tile on the board
    float y = -3.5f;
    float x = -2;

    void Awake() {

        //For loops go through board and initiate all the game object pointers. Same as Tetromino_Base
        for (int row = 0; row < TETRIS_BOARD.GetLength(0); row++) {
            for (int collum = 0; collum < TETRIS_BOARD.GetLength(1); collum++) {

                TETRIS_BOARD[row, collum] = Instantiate(GridTile, new Vector3(x, y, 0), Quaternion.identity);
                x = x + (TETRIS_BOARD[row, collum].GetComponent<SpriteRenderer>().bounds.size.x); 

            } //end for

            y = y + (TETRIS_BOARD[row, 0].GetComponent<SpriteRenderer>().bounds.size.y);
            x = x - ((TETRIS_BOARD[row, 0].GetComponent<SpriteRenderer>().bounds.size.x) * TETRIS_BOARD.GetLength(0));

        }//end for
    }
}
