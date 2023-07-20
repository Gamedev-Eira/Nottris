using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_Tetris_Board : MonoBehaviour {

    public GameObject GridTile; //Gets a reference to GridBlockRenderer

    //2D array stores pointers to game objects
    private GameObject[,] TETRIS_BOARD = new GameObject[16, 10];

    //Starting position of the first tile on the board
    private float y = -3.5f;
    private float x = -2;

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

    void Update() {

    }

    //Private Functions

    private void ShiftLines(int StartingRow) {

        for (int row = StartingRow; row < TETRIS_BOARD.GetLength(0) - 1; row++) {
            for (int collum = 0; collum < TETRIS_BOARD.GetLength(1); collum++) {

                TETRIS_BOARD[row, collum].GetComponent<GridBlockRenderer>().UpdateStatus(TETRIS_BOARD[row+1, collum].GetComponent<GridBlockRenderer>().ReportStatus());  ;

            }//end for
        } //end for
    }//end ShiftLines

    private void CheckForLineClears() {

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

    //Public Functions

    public Vector3 ReturnStartingPosition() {
        return ( TETRIS_BOARD[TETRIS_BOARD.GetLength(0)-3 , 4].transform.position);
    }

    public float[] ReturnEdges() {

        float LeftBorder = TETRIS_BOARD[0, 0].transform.position[0];
        float RightBorder = TETRIS_BOARD[0, TETRIS_BOARD.GetLength(1) - 1].transform.position[0];
        float BottomBorder = TETRIS_BOARD[0, 0].transform.position[1];

        float[] Borders = new float[3] { LeftBorder, RightBorder, BottomBorder };

        return (Borders);
    }
}
