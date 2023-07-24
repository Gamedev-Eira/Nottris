using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_Tetris_Board : MonoBehaviour {

    public GameObject GridTile; //Gets a reference to GridBlockRenderer

    //2D array stores pointers to game objects
    private GameObject[,] TETRIS_BOARD = new GameObject[16, 10];

    public Sprite TopLeft;
    public Sprite TopRight;
    public Sprite BottomLeft;
    public Sprite BottomRight;
    public Sprite Bottom;
    public Sprite Left;
    public Sprite Right;
    public Sprite Top;

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

        //Updates edge of playfield
        for (int collum = 1; collum < TETRIS_BOARD.GetLength(1)-1; collum++) {

            TETRIS_BOARD[0, collum].GetComponent<GridBlockRenderer>().RenderCustomSprite(Bottom);
            TETRIS_BOARD[(TETRIS_BOARD.GetLength(0) - 1), collum].GetComponent<GridBlockRenderer>().RenderCustomSprite(Top);

        } for (int row = 1; row < TETRIS_BOARD.GetLength(0)-1; row++) {

            TETRIS_BOARD[row, 0].GetComponent<GridBlockRenderer>().RenderCustomSprite(Left);
            TETRIS_BOARD[row, (TETRIS_BOARD.GetLength(1) - 1)].GetComponent<GridBlockRenderer>().RenderCustomSprite(Right);

        } //end for

        TETRIS_BOARD[0, 0].GetComponent<GridBlockRenderer>().RenderCustomSprite(BottomLeft);
        TETRIS_BOARD[0, (TETRIS_BOARD.GetLength(1) - 1) ].GetComponent<GridBlockRenderer>().RenderCustomSprite(BottomRight);
        TETRIS_BOARD[(TETRIS_BOARD.GetLength(0) -1), 0 ].GetComponent<GridBlockRenderer>().RenderCustomSprite(TopLeft);
        TETRIS_BOARD[(TETRIS_BOARD.GetLength(0) - 1), TETRIS_BOARD.GetLength(1) - 1].GetComponent<GridBlockRenderer>().RenderCustomSprite(TopRight);

    }

    void Update() {
        TETRIS_BOARD[0, 1].GetComponent<GridBlockRenderer>().UpdateStatus("Red");
        TETRIS_BOARD[0, 3].GetComponent<GridBlockRenderer>().UpdateStatus("Red");

        TETRIS_BOARD[0, 8].GetComponent<GridBlockRenderer>().UpdateStatus("Red");
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
    }//end for

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

    public float FindGhostPosition(bool[,,] shape, float startX, int rotate, int FirstValidColumn, int LastValidColumn) {

        float TetWidth = TETRIS_BOARD[0, 0].GetComponent<SpriteRenderer>().bounds.size.x;

        int StartingColumn = 0;
        int temp = 0;

        for(int column = 0; column < TETRIS_BOARD.GetLength(1); column++) {
            if (TETRIS_BOARD[0, column].transform.position.x == startX) {
                StartingColumn = column;
            }//end if
        }//end for

        Debug.Log("Starting Column: " + StartingColumn);

        //int EndingColumn = StartingColumn + 4;
        //int CurrentColumn = StartingColumn;

        int StartingRow = 0;

        int FirstValidRow = 0;
        int EmptyRows = 0;

        int TetFits = 0;
        
        bool TetrominoStartFound;
        bool GhostPositionFound = false;

        TetrominoStartFound = false;
        for (int row = 0; row < shape.GetLength(0); row++) {

            int EmptyColumn = 0;

            for (int column = 0; column < shape.GetLength(1); column++) {
                if (shape[rotate - 1, row, column] == false) { EmptyColumn++; }
            }//end for

            if (EmptyColumn == 4 && !TetrominoStartFound) {
                EmptyRows++;
            } else if (EmptyColumn < 4 && !TetrominoStartFound) {
                TetrominoStartFound = true;
                FirstValidRow = row;
            }//end if

        }//end for

        Debug.Log("First Column: " + FirstValidColumn + " | First Row: " + FirstValidRow );

        while (!GhostPositionFound) {
        
            TetFits = 0;

            for(int column = FirstValidColumn; column <= LastValidColumn; column++) {
                for (int row = FirstValidRow; row < 4; row++) {
                    
                    bool BoardTileEmpty = (TETRIS_BOARD[StartingRow + (row - FirstValidRow), StartingColumn + (column - FirstValidColumn)].GetComponent<GridBlockRenderer>().ReportStatus() == "Empty") ;
                    bool GhostTileOccupied = (shape[rotate-1, row, column] == true) ;

                    if (BoardTileEmpty && GhostTileOccupied) { TetFits++; }
                }//end for
            }//end for

            Debug.Log("Tet Fits: " + TetFits);

            if (TetFits == 4) { GhostPositionFound = true; }
            else { StartingRow++; }

            if(StartingRow == 15) { GhostPositionFound = true; }

        }//end while

        float ReturnY = TETRIS_BOARD[StartingRow, StartingColumn].transform.position[1];
        if (EmptyRows > 0) { ReturnY -= (TetWidth * EmptyRows); }
        return ReturnY;


    }//end func
}
