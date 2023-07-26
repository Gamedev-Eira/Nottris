using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino_Base : MonoBehaviour {

    public GameObject GridTile;     //Gets a reference to the GameObject found in GridBlockRenderer

    private float PositionX = 0;    //The X and Y positions ther GridTiles are spawned from 
    private float PositionY = 0;

    private int RotationValue = 1;  //The current rotation of the Tetromino

    private const int Dimensions = 4;   //The dimensions of the Tetromino grid
    private const int ShapeQuantity = 4;//The amoung of rotations a Tetromino can do

    string Colour = "Empty";    //Colour tracks the colour of the tetromino currently being rendered

    private GameObject[,] Tetromino = new GameObject[4, 4]; //Array of GameObject (GridTile) pointers

    private bool[,,] Tet_Shape;     //A 3D array (4x4) that stores the potential shapes of the current tetromino

    private float[] Boundaries;     //stores the left, right, and bottom boundaries of the playfield (in that order)

    private bool TetUpdate = true;
    private bool PlaceNextCall = false;

    ///////////////////////////////////////////////////////

    void Awake() {  //DO NOT MOVE THIS FROM AWAKE. EVERYTHING WILL BREAK

        //For loop goes through 2D array and Instansiates a GridTile in each entry in the array

        Vector3 Position = GameObject.Find("Tetris_Board_Empty").GetComponent<Display_Tetris_Board>().ReturnStartingPosition();
        PositionX = Position[0];
        PositionY = Position[1];

        Boundaries = GameObject.Find("Tetris_Board_Empty").GetComponent<Display_Tetris_Board>().ReturnEdges();

        for (int row = 0; row < Dimensions; row++) {
            for (int collum = 0; collum < Dimensions; collum++) {

                Tetromino[row, collum] = Instantiate(GridTile, new Vector3(PositionX, PositionY, 0), Quaternion.identity);

                //Gets the size of the created GridTile and increments PositionX by it's value
                //This places the next GridTile next to the previous one
                PositionX = PositionX + (Tetromino[row, collum].GetComponent<SpriteRenderer>().bounds.size.x);

            } //end for

            //Same deal for Y as with X
            PositionY = PositionY + (Tetromino[0, 0].GetComponent<SpriteRenderer>().bounds.size.y);

            //X is now reset to it's previous starting value
            PositionX = PositionX - ((Tetromino[0, 0].GetComponent<SpriteRenderer>().bounds.size.x) * Tetromino.GetLength(0));

        }//end for

        InvokeRepeating("UpdatePositionSoftdrop", 0.0f, 1.0f);

    }//end Awake

    ///////////////////////////////////////////////////////

    public void init(string colour, bool[,,] shape) {
        //init is called by TetrominoConstructor and passes along a colour and 3D array
        //The tetromino then makes it's values the ones it recieved
        Colour = colour;
        Tet_Shape = shape;

        MoveToTop();
        UpdateRotation();

        TetUpdate = true;

    }//end init

    ///////////////////////////////////////////////////////
    // Visual Updates

    private void UpdateRotation() {

        //for loops go through Tetromino and compare it to the 2D array of bools
        //if the corrosponding bool is true, it makes that tile the aproproate colour it recieved from init
        //if it's false, it's set to empty

        for (int row = 0; row < Dimensions; row++) {
            for (int column = 0; column < Dimensions; column++) {
                if(Tet_Shape[(RotationValue - 1), column, row] == true) {
                    Tetromino[column, row].GetComponent<GridBlockRenderer>().UpdateStatus(Colour);
                } else if (Tet_Shape[(RotationValue - 1), column, row] == false) {
                    Tetromino[column, row].GetComponent<GridBlockRenderer>().UpdateStatus("Empty");
                } //end if else

            }//end for
        }//end for

    }//end void

    private void ResetToEmpty() {

        float temp = PositionY;

        //for loop sets all the GridTiles in Tetromino to empty

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

    private void MoveToTop() {

        Vector3 Position = GameObject.Find("Tetris_Board_Empty").GetComponent<Display_Tetris_Board>().ReturnStartingPosition();
        PositionX = Position[0];
        PositionY = Position[1];

        for (int row = 0; row < Dimensions; row++) {
            for (int collum = 0; collum < Dimensions; collum++) {

                Tetromino[row, collum].transform.position = new Vector3(PositionX, PositionY, 0.0f);

                //Gets the size of the created GridTile and increments PositionX by it's value
                //This places the next GridTile next to the previous one
                PositionX = PositionX + (Tetromino[row, collum].GetComponent<SpriteRenderer>().bounds.size.x);

            } //end for

            //Same deal for Y as with X
            PositionY = PositionY + (Tetromino[0, 0].GetComponent<SpriteRenderer>().bounds.size.y);

            //X is now reset to it's previous starting value
            PositionX = PositionX - ((Tetromino[0, 0].GetComponent<SpriteRenderer>().bounds.size.x) * Tetromino.GetLength(0));

        }//end for

    }//end func

    ///////////////////////////////////////////////////////
    //Rotation

    private int Rotator(bool clockwise, int rotate) {

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

    private void DoRotation(bool Clockwise) {

        //Do rotation updates RotationValue then calls UpdateRotation
        RotationValue = Rotator(Clockwise, RotationValue);
        UpdateRotation();
        ResolvePosition();

    }//end void

    ///////////////////////////////////////////////////////
    //Movement

    private bool CheckSidewaysMovement(bool MovingLeft) {

        int OnScreen = 0;

        for (int row = 0; row < Tetromino.GetLength(0); row++) {
            for (int collum = 0; collum < Tetromino.GetLength(1); collum++) {

                if(Tet_Shape[RotationValue-1, row, collum]) {

                    if (MovingLeft && Tetromino[row, collum].transform.position[0] > Boundaries[0]) { OnScreen++; }
                    else if (!MovingLeft && Tetromino[row, collum].transform.position[0] < Boundaries[1]) { OnScreen++; }

                }//end if

            } //end for

        }//end for

        if(OnScreen < 4) { return false; }

        int StartColumn = GetComponent<Tetromino_Ghost>().ReturnFirstColumn();

        return GameObject.Find("Tetris_Board_Empty").GetComponent<Display_Tetris_Board>().CheckSidewaysTetMovement(Tet_Shape, RotationValue, Tetromino[0, StartColumn].transform.position.x, Tetromino[0, 0].transform.position.y, MovingLeft);

    }//end bool

    private void UpdatePositionSideways(bool MovingLeft) {

        float NewPosition = Tetromino[0, 0].GetComponent<SpriteRenderer>().bounds.size.x;

        for (int row = 0; row < Dimensions; row++) {
            for (int column = 0; column < Dimensions; column++) {

                if (MovingLeft) { Tetromino[row, column].transform.position -= new Vector3(NewPosition, 0, 0); }
                else if (!MovingLeft) { Tetromino[row, column].transform.position += new Vector3(NewPosition, 0, 0); }

            }//end for
        }//end for

    }

    private void ResolvePosition() {

        for (int row = 0; row < Tetromino.GetLength(0); row++) {
            for (int collum = 0; collum < Tetromino.GetLength(1); collum++) {

                if (Tet_Shape[RotationValue - 1, row, collum]) {

                    while (Tetromino[row, collum].transform.position[0] < Boundaries[0]) {
                        UpdatePositionSideways(false);
                    }//end while

                    while (Tetromino[row, collum].transform.position[0] > Boundaries[1]) {
                        UpdatePositionSideways(true);
                    }//end while

                }//end if

            } //end for
        }//end for

    }//end func

    private void UpdatePositionSoftdrop() {

        if (Tetromino[0, 0].transform.position.y > GetComponent<Tetromino_Ghost>().GetPosition().y) {

            float NewPosition = Tetromino[0, 0].GetComponent<SpriteRenderer>().bounds.size.y;

            for (int row = 0; row < Dimensions; row++) {
                for (int column = 0; column < Dimensions; column++) {

                    Tetromino[row, column].transform.position -= new Vector3(0, NewPosition, 0);

                }//end for
            }//end for

        }//end if

        else if (Tetromino[0, 0].transform.position == GetComponent<Tetromino_Ghost>().GetPosition()) { CheckBottom(); }

    }//end func

    ///////////////////////////////////////////////////////
    //Ect

    void CheckBottom() {

        if(PlaceNextCall == false) { PlaceNextCall = true; }
        else if(PlaceNextCall == true) {
            PlaceNextCall = false;
            GetComponent<Tetromino_Ghost>().InitiatePlacement(Tet_Shape, RotationValue, Colour);
        }//end if else

    }//end void

    ///////////////////////////////////////////////////////
    //Main game loop

    void Update() {

        //If checks for a left or right mouse click and rotates accordingly
        if (Input.GetMouseButtonDown(0)) { DoRotation(true); TetUpdate = true; }
        else if (Input.GetMouseButtonDown(1)) { DoRotation(false); TetUpdate = true; }

        else if (Input.GetKeyDown("a") && CheckSidewaysMovement(true)) { UpdatePositionSideways(true); TetUpdate = true; }
        else if (Input.GetKeyDown("d") && CheckSidewaysMovement(false)) { UpdatePositionSideways(false); TetUpdate = true; }

        else if (Input.GetKeyDown("w")) { GetComponent<Tetromino_Ghost>().InitiatePlacement(Tet_Shape, RotationValue, Colour); }
        else if (Input.GetKeyDown("s")) { UpdatePositionSoftdrop(); }

        if (TetUpdate) {

            PlaceNextCall = false;

            int FirstColumn = 0;
            int LastColumn = 0;
            int check;

            bool FirstColumnFound = false;
            for(int column = 0; column < Tetromino.GetLength(1); column++ ) {
                check = 0;
                for (int row = 0; row < Tetromino.GetLength(0); row++) {
                    if (Tet_Shape[RotationValue - 1, row, column] == false) { check++; }
                }//end for

                if (check >= 4 && !FirstColumnFound) { FirstColumn++; }
                else if (check < 4) {
                    LastColumn = column;
                    if (FirstColumn >= 0) { FirstColumnFound = true; }
                }//end if
            }//end for

            float GhostY = GameObject.Find("Tetris_Board_Empty").GetComponent<Display_Tetris_Board>().FindGhostPosition(Tet_Shape, (Tetromino[0, FirstColumn].transform.position.x), RotationValue, FirstColumn, LastColumn);
            Vector3 GhostPosition = new Vector3(Tetromino[0,0].transform.position.x, GhostY, 0.0f);
            
            GetComponent<Tetromino_Ghost>().RenderGhost(Tet_Shape, Colour, GhostPosition, (RotationValue-1) );
            TetUpdate = false;
        }

    } //end update

}//end class