//Tetromino is a base class for all the other tetromino types to draw from.
//It has a Rotate method that can be used by all other tetromino pieces
public class Tetrimino {

    //Rotate is a method used to rotate a tetromino. It uses an int (referred to as rotate) to keep track of a value...
    //...between 1 and 4, to reflect the 4 possible orientations of a tetromino.
    //It also recieves a boolean called RotationClockwise. If RotationClockwise is true, it rotates clockwise (via...
    //... incrementing by one), and if false it rotates anti-clockwise (via deincrementing by 1).
    //It then checks if the value has exceeded 4 or become less than 1, and instead causes an over/under-flow if need-be.

    //Rotate is protected so only child classes can access this function
    protected int Rotate(int rotate, bool RotationClockwise) {

        if (RotationClockwise == true) {
            rotate++;
            if (rotate >= 5) { rotate = 1; }

        } else if (RotationClockwise == false) {
            rotate++;
            if (rotate <= 0) { rotate = 4; }
        } //end if-else

        return rotate; //after the if-else, the new value of rotate is returned to the sub-class
    } //end Rotate

    protected int[] GivePosition (int x, int y) {
        int[] position = new int[2] { x, y };
        return (position);
    } //end GivePosition

} //end Class

/////////////////////////////////////////////////////////////////////////////

//tTet is one of the sub-classes of Tetromino, and represents the T piece
public class tTet : Tetrimino {

    //Variables

    //Used by the AI to quickly tell at a glance what tetromino piece is being represented by class data
    private string TetrominoType = "T";

    //Keepts track of the rotation of the tetromino
    private int RotationValue = 1;

    int PositionX = 0;
    int PositionY = 0;

    //The 4 orientations of the tetromino. Represented by 4x4 boolean arrays.
    //true means that tile is occupied by the tetromino, false means that it is not.

    private bool[,] TetrominoShape1 = new bool[4, 4]
    {
        {false , true  , false , false},
        {true  , true  , true  , false},
        {false , true  , false , false},
        {false , false , false , false},
    };

    private bool[,] TetrominoShape2 = new bool[4, 4]
    {
        {false , true  , false , false},
        {false , true  , true  , false},
        {false , true  , false , false},
        {false , false , false , false},
    };

    private bool[,] TetrominoShape3 = new bool[4, 4]
    {
        {false , false , false , false},
        {true  , true  , true  , false},
        {false , true  , false , false},
        {false , false , false , false},
    };

    private bool[,] TetrominoShape4 = new bool[4, 4]
    {
        {false , true  , false , false},
        {true  , true  , false , false},
        {false , true  , false , false},
        {false , false , false , false},
    };

    //ReturnTetType simply displays the value of TetrominoType by returning it
    string ReturnTetType() { return (TetrominoType); }

    //ReturnTetShape returns the apropriate 4x4 grid for the tetromino, based on it's current rotation
    //It uses a switch-case with each 4 possible rotation positions as a case, and returns the apropriate TetrominioShape
    bool[,] ReturnTetShape() {
        switch (RotationValue)
        {
            default:
                return (TetrominoShape1);
                break;

            case 1:
                return (TetrominoShape1);
                break;

            case 2:
                return (TetrominoShape2);
                break;

            case 3:
                return (TetrominoShape3);
                break;

            case 4:
                return (TetrominoShape4);
                break;
        }
    }

    bool[,] RotateTet(bool RotateClockwise) {
        RotationValue = Rotate(RotationValue, RotateClockwise);
        return (ReturnTetShape());
    }

    int[] ReturnPosition() {
        return (GivePosition(PositionX, PositionY));
    } //end ReturnPosition

} //end Class