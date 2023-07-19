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

    void Update()
    {
        TETRIS_BOARD[0, 0].GetComponent<GridBlockRenderer>().UpdateStatus("Gray");
        TETRIS_BOARD[0, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Gray");

        for(int x = 0; x < TETRIS_BOARD.GetLength(1); x++) {
            TETRIS_BOARD[1, x].GetComponent<GridBlockRenderer>().UpdateStatus("Gray");
            TETRIS_BOARD[2, x].GetComponent<GridBlockRenderer>().UpdateStatus("Gray");
        }

        TETRIS_BOARD[3, 8].GetComponent<GridBlockRenderer>().UpdateStatus("Gray");

        CheckForLineClears();
    }

    void ShiftLines(int StartingRow) {

        for (int row = StartingRow; row < TETRIS_BOARD.GetLength(0) - 1; row++) {
            for (int collum = 0; collum < TETRIS_BOARD.GetLength(1); collum++) {

                TETRIS_BOARD[row, collum].GetComponent<GridBlockRenderer>().UpdateStatus(TETRIS_BOARD[row+1, collum].GetComponent<GridBlockRenderer>().ReportStatus());  ;

            }//end for
        } //end for
    }//end ShiftLines

    void CheckForLineClears() {

        for (int row = 0; row < TETRIS_BOARD.GetLength(0); row++) {

            int OccupiedTiles = 0;

            for (int collum = 0; collum < TETRIS_BOARD.GetLength(1); collum++) {

                if (TETRIS_BOARD[row, collum].GetComponent<GridBlockRenderer>().ReportStatus() != "Empty") {
                    OccupiedTiles++;
                }//end if

            } //end for

            if (OccupiedTiles == TETRIS_BOARD.GetLength(1)) {
                ShiftLines(row);
                row--;
            }
        }//end for
    }

    public Vector3 ReturnStartingPosition() {
        return ( TETRIS_BOARD[TETRIS_BOARD.GetLength(0)-2 , 4].transform.position );
    }
}
