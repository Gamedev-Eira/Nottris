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

    private int CurrentGhostY = 0;

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

    }//end func

    void Start() { UpdateEdgeTiles(); }

    //Private Functions

    private void ShiftLines(int StartingRow) {

        for (int row = StartingRow; row < TETRIS_BOARD.GetLength(0) - 1; row++) {
            for (int collum = 0; collum < TETRIS_BOARD.GetLength(1); collum++) {

                TETRIS_BOARD[row, collum].GetComponent<GridBlockRenderer>().UpdateStatus(TETRIS_BOARD[row+1, collum].GetComponent<GridBlockRenderer>().ReportStatus());  ;

            }//end for
        } //end for

        UpdateEdgeTiles();

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

    private int FindStartingColumn(float x) {
        for (int column = 0; column < TETRIS_BOARD.GetLength(1); column++){
            if (TETRIS_BOARD[0, column].transform.position.x == x) {
                return column;
            }//end if
        }//end for

        return 0;

    }//end func

    private int FindStartingRow(float y) {
        for (int row = 0; row < TETRIS_BOARD.GetLength(0); row++) {
            if (TETRIS_BOARD[row, 0].transform.position.y == y) {
                return row;
            }//end if
        }//end for

        return 0;

    }//end func

    private void UpdateEdgeTiles() {
        //Updates edge of playfield

        for (int collum = 1; collum < TETRIS_BOARD.GetLength(1) - 1; collum++) {
            if (TETRIS_BOARD[0, collum].GetComponent<GridBlockRenderer>().ReportStatus() == "Empty") {
                TETRIS_BOARD[0, collum].GetComponent<GridBlockRenderer>().RenderCustomSprite(Bottom);
            } if (TETRIS_BOARD[TETRIS_BOARD.GetLength(0)-1, collum].GetComponent<GridBlockRenderer>().ReportStatus() == "Empty") {
                TETRIS_BOARD[TETRIS_BOARD.GetLength(0)-1, collum].GetComponent<GridBlockRenderer>().RenderCustomSprite(Top);
            }//end if
        }//end for
        
        for (int row = 1; row < TETRIS_BOARD.GetLength(0) - 1; row++) {
            if (TETRIS_BOARD[row, 0].GetComponent<GridBlockRenderer>().ReportStatus() == "Empty") {
                TETRIS_BOARD[row, 0].GetComponent<GridBlockRenderer>().RenderCustomSprite(Left);
            } if (TETRIS_BOARD[row, (TETRIS_BOARD.GetLength(1) - 1)].GetComponent<GridBlockRenderer>().ReportStatus() == "Empty") {
                TETRIS_BOARD[row, (TETRIS_BOARD.GetLength(1) - 1)].GetComponent<GridBlockRenderer>().RenderCustomSprite(Right);
            }//end if
        } //end for

        if (TETRIS_BOARD[0, 0].GetComponent<GridBlockRenderer>().ReportStatus() == "Empty") {
            TETRIS_BOARD[0, 0].GetComponent<GridBlockRenderer>().RenderCustomSprite(BottomLeft);
        } if (TETRIS_BOARD[0, (TETRIS_BOARD.GetLength(1)-1)].GetComponent<GridBlockRenderer>().ReportStatus() == "Empty") {
            TETRIS_BOARD[0, (TETRIS_BOARD.GetLength(1)-1)].GetComponent<GridBlockRenderer>().RenderCustomSprite(BottomRight);
        } if (TETRIS_BOARD[(TETRIS_BOARD.GetLength(0)-1), 0].GetComponent<GridBlockRenderer>().ReportStatus() == "Empty") {
            TETRIS_BOARD[(TETRIS_BOARD.GetLength(0)-1), 0].GetComponent<GridBlockRenderer>().RenderCustomSprite(TopLeft);
        } if (TETRIS_BOARD[(TETRIS_BOARD.GetLength(0) - 1), TETRIS_BOARD.GetLength(1) - 1].GetComponent<GridBlockRenderer>().ReportStatus() == "Empty") {
            TETRIS_BOARD[(TETRIS_BOARD.GetLength(0) - 1), TETRIS_BOARD.GetLength(1) - 1].GetComponent<GridBlockRenderer>().RenderCustomSprite(TopRight);
        }

        
        
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

    public float FindGhostPosition(bool[,,] shape, float startX, int rotate, int FirstValidColumn, int LastValidColumn) {

        float TetWidth = TETRIS_BOARD[0, 0].GetComponent<SpriteRenderer>().bounds.size.x;

        int StartingColumn = 0;
        int temp = 0;

        StartingColumn = FindStartingColumn(startX);

        int StartingRow = 0;

        int FirstValidRow = 0;
        int EmptyRows = 0;

        int TetFits = 0;
        
        bool GhostPositionFound = false;

        bool TetrominoStartFound = false;
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

        GameObject.Find("Tetromino_Empty").GetComponent<Tetromino_Ghost>().GetStartPoint(FirstValidRow, FirstValidColumn, LastValidColumn);

        while (!GhostPositionFound) {
        
            TetFits = 0;

            for(int column = FirstValidColumn; column <= LastValidColumn; column++) {
                for (int row = FirstValidRow; row < 4; row++) {
                    
                    bool BoardTileEmpty;
                    if(StartingRow + (row - FirstValidRow) > TETRIS_BOARD.GetLength(0)-1 ) { BoardTileEmpty = true; }
                    else { BoardTileEmpty = (TETRIS_BOARD[StartingRow + (row - FirstValidRow), StartingColumn + (column - FirstValidColumn)].GetComponent<GridBlockRenderer>().ReportStatus() == "Empty"); }

                    bool GhostTileOccupied = (shape[rotate-1, row, column] == true) ;

                    if (BoardTileEmpty && GhostTileOccupied) { TetFits++; }
                }//end for
            }//end for

            if (TetFits == 4) { GhostPositionFound = true; }
            else { StartingRow++; }

            if(StartingRow == 15) { GhostPositionFound = true; }

        }//end while

        float ReturnY = TETRIS_BOARD[StartingRow, StartingColumn].transform.position[1];
        if (EmptyRows > 0) { ReturnY -= (TetWidth * EmptyRows); }

        CurrentGhostY = StartingRow;

        return ReturnY;

    }//end func

    public void PlaceToBoard(int FirstValidRow, int FirstValidColumn, bool[,,] Tet_Shape, int rotate, string Colour, float Position) {

        int StartingColumn = FindStartingColumn(Position);
        int StartingRow = CurrentGhostY;

        bool TetrominoStartFound = false;
        int EmptyColumn = 0;

        for (int row = 0; row < Tet_Shape.GetLength(0); row++) {
            for (int column = 0; column < Tet_Shape.GetLength(1); column++) {
                if (Tet_Shape[rotate-1, row, column] == false) { EmptyColumn++; }
            }//end for

            if (EmptyColumn < 4 && !TetrominoStartFound) {
                TetrominoStartFound = true;
                FirstValidRow = row;
            }//end if
        }//end for

        for (int row = FirstValidRow; row < Tet_Shape.GetLength(0); row++) {
            
            for (int column = FirstValidColumn; column < Tet_Shape.GetLength(1); column++) {

                if (Tet_Shape[rotate-1, row, column] == true) {

                    TETRIS_BOARD[StartingRow + (row - FirstValidRow), StartingColumn + (column - FirstValidColumn)].GetComponent<GridBlockRenderer>().UpdateStatus(Colour);
                }
            }//end for
        }//end for

        GameObject.Find("Tetromino_Empty").GetComponent<TetrominoConstructor>().MakeNewTet();

        CheckForLineClears();

    }//end func

    public bool CheckSidewaysTetMovement(bool[,,] shape, int rotate, float posX, float posY, bool MovingLeft) {

        int FirstValidRow = GameObject.Find("Tetromino_Empty").GetComponent<Tetromino_Ghost>().ReturnFirstRow();
        int FirstValidColumn = GameObject.Find("Tetromino_Empty").GetComponent<Tetromino_Ghost>().ReturnFirstColumn();
        int LastValidColumn = GameObject.Find("Tetromino_Empty").GetComponent<Tetromino_Ghost>().ReturnLastColumn();

        int StartingColumn = FindStartingColumn(posX);
        int StartingRow = FindStartingRow(posY);

        Debug.Log(StartingRow);

        if (MovingLeft) { StartingColumn--; }
        else if (!MovingLeft) { StartingColumn++; }

        int TetFits = 0;

        for (int row = FirstValidRow; row < shape.GetLength(0); row++ ) {
            for (int column = FirstValidColumn; column <= LastValidColumn; column++) {
                int a = StartingRow + (row - FirstValidRow);
                Debug.Log("Row: " + a);

                bool TetOccupied = (shape[rotate - 1, row, column] == true);
                bool BoardEmpty;
                if (StartingRow + (row - FirstValidRow) > TETRIS_BOARD.GetLength(0) - 1) { BoardEmpty = true; }
                else { BoardEmpty = (TETRIS_BOARD[StartingRow + (row - FirstValidRow), StartingColumn + (column - FirstValidColumn)].GetComponent<GridBlockRenderer>().ReportStatus() == "Empty"); }

                Debug.Log(TetOccupied && BoardEmpty);

                if ( TetOccupied && BoardEmpty) { TetFits++; }
            }//end for
        }//end for

        if(TetFits < 4) { return false; }
        else { return true; }

    }//end func

    public bool CheckStartPosition(bool[,,] shape, int rotate, float posX, float posY) {

        int FirstValidRow = GameObject.Find("Tetromino_Empty").GetComponent<Tetromino_Ghost>().ReturnFirstRow();
        int FirstValidColumn = GameObject.Find("Tetromino_Empty").GetComponent<Tetromino_Ghost>().ReturnFirstColumn();
        int LastValidColumn = GameObject.Find("Tetromino_Empty").GetComponent<Tetromino_Ghost>().ReturnLastColumn();

        int StartingColumn = FindStartingColumn(posX);
        int StartingRow = FindStartingRow(posY);

        int TetFits = 0;

        for (int row = FirstValidRow; row < shape.GetLength(0); row++)
        {
            for (int column = FirstValidColumn; column <= LastValidColumn; column++)
            {
                int a = StartingRow + (row - FirstValidRow);
                Debug.Log("Row: " + a);

                bool TetOccupied = (shape[rotate - 1, row, column] == true);
                bool BoardEmpty;
                if (StartingRow + (row - FirstValidRow) > TETRIS_BOARD.GetLength(0) - 1) { BoardEmpty = true; }
                else { BoardEmpty = (TETRIS_BOARD[StartingRow + (row - FirstValidRow), StartingColumn + (column - FirstValidColumn)].GetComponent<GridBlockRenderer>().ReportStatus() == "Empty"); }

                Debug.Log(TetOccupied && BoardEmpty);

                if (TetOccupied && BoardEmpty) { TetFits++; }
            }//end for
        }//end for

        if (TetFits < 4) { return false; }
        else { return true; }

    }//end func

    //Draw Background

    public int ReturnBoardHeight() {
        int Height = TETRIS_BOARD.GetLength(0);
        return Height;
    }

    public int ReturnBoardLength() {
        int Length = TETRIS_BOARD.GetLength(1);
        return Length;
    }

    public Vector3 ReturnBoardStart() {

        Vector3 Midpoint = new Vector3(0.0f, 0.0f, 0.0f);
        
        if(TETRIS_BOARD.GetLength(0) % 2 == 0 && TETRIS_BOARD.GetLength(1) % 2 == 0) {
            Midpoint = TETRIS_BOARD[ ((TETRIS_BOARD.GetLength(0)/2) -1 ) , ((TETRIS_BOARD.GetLength(1) / 2) -1 ) ].transform.position;
        }//end if

        return Midpoint;

    }//end func

    public float ReturnGridScale() {
        return (TETRIS_BOARD[0, 0].transform.localScale.x);
    }

}//end class
