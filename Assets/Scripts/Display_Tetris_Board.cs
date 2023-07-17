using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_Tetris_Board : MonoBehaviour {

    public GameObject GridTile;

    GameObject[,] TETRIS_BOARD = new GameObject[16, 10];

    string ColourblindMode = "Regular";


    void Start() {

        float y = -3.5f;

        for (int row = 0; row < TETRIS_BOARD.GetLength(0); row++) {

            float x = -2;

            for (int collum = 0; collum < TETRIS_BOARD.GetLength(1); collum++) {

                TETRIS_BOARD[row, collum] = Instantiate(GridTile, new Vector3(x, y, 0), Quaternion.identity);
                x = x + (TETRIS_BOARD[row, collum].GetComponent<SpriteRenderer>().bounds.size.x); 

            } //end for

            y = y + (TETRIS_BOARD[row, 0].GetComponent<SpriteRenderer>().bounds.size.y);

        }//end for
        

    }

    void Update() {
        
    }

    void UpdateBoard() {

    }

    void RedrawBoard() {

    }

}
