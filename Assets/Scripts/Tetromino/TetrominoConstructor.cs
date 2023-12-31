using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoConstructor : MonoBehaviour {

    private const int Shape_Size = 4;   //Shape size stores the size of the tetromino grid
    private const int ShapeQuantity = 4;    //Shape quantity stores how many rotations a tetromino can do

    private char[] Shapes = new char[7] { 'T', 'I', 'O', 'Z', 'S', 'J', 'L' };
    private int CurrentShapeAccess = -1;

    //////////////////////////////////////////////////////////////////////

    //These arrays are 3D arrays that contain the 4 rotations of each tetromino.
    //They are passed to Tetromino_Base to allow it to handle it's own drawing. 
    //The 3D arrays contain 4 4x4 grids

    private bool[,,] I_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
    {
        {
            {false, false, false, false},
            {true , true , true , true },
            {false, false, false, false},
            {false, false, false, false}
        },

        {
            {false, false, true , false},
            {false, false, true , false},
            {false, false, true , false},
            {false, false, true , false}
        },

        {
            {false, false, false, false},
            {false, false, false, false},
            {true , true , true , true },
            {false, false, false, false}
        },

        {
            {false, true , false, false},
            {false, true , false, false},
            {false, true , false, false},
            {false, true , false, false}
        }
    };

    private bool[,,] L_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
    {
        {
            {true , false, false, false},
            {true , true , true , false},
            {false, false, false, false},
            {false, false, false, false}
        },

        {
            {false, true , true , false},
            {false, true , false, false},
            {false, true , false, false},
            {false, false, false, false}
        },

        {
            {false, false, false, false},
            {true , true , true , false},
            {false, false, true , false},
            {false, false, false, false}
        },

        {
            {false, true , false, false},
            {false, true , false, false},
            {true , true , false, false},
            {false, false, false, false}
        }
    };

    private bool[,,] J_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
    {
        {
            {false, false, true , false},
            {true , true , true , false},
            {false, false, false, false},
            {false, false, false, false}
        },

        {
            {false, true , false, false},
            {false, true , false, false},
            {false, true , true , false},
            {false, false, false, false}
        },

        {
            {false, false, false, false},
            {true , true , true , false},
            {true , false, false, false},
            {false, false, false, false}
        },

        {
            {true , true , false, false},
            {false, true , false, false},
            {false, true , false, false},
            {false, false, false, false}
        }
    };

    private bool[,,] O_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
    {
        {
            {true , true , false, false},
            {true , true , false, false},
            {false, false, false, false},
            {false, false, false, false}
        },

        {
            {true , true , false, false},
            {true , true , false, false},
            {false, false, false, false},
            {false, false, false, false}
        },

        {
            {true , true , false, false},
            {true , true , false, false},
            {false, false, false, false},
            {false, false, false, false}
        },

        {
            {true , true , false, false},
            {true , true , false, false},
            {false, false, false, false},
            {false, false, false, false}
        }
    };

    private bool[,,] S_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
    {
        {
            {false, true , true , false},
            {true , true , false, false},
            {false, false, false, false},
            {false, false, false, false}
        },

        {
            {false, true , false, false},
            {false, true , true , false},
            {false, false, true , false},
            {false, false, false, false}
        },

        {
            {false, false, false, false},
            {false, true , true , false},
            {true , true , false, false},
            {false, false, false, false}
        },

        {
            {true , false, false, false},
            {true , true , false, false},
            {false, true , false, false},
            {false, false, false, false}
        }
    };

    private bool[,,] T_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
    {
        {
            {false, true , false, false},
            {true , true , true , false},
            {false, false, false, false},
            {false, false, false, false}
        },

        {
            {false, true , false, false},
            {false, true , true , false},
            {false, true , false, false},
            {false, false, false, false}
        },

        {
            {false, false, false, false},
            {true , true , true , false},
            {false, true , false, false},
            {false, false, false, false}
        },

        {
            {false, true , false, false},
            {true , true , false, false},
            {false, true , false, false},
            {false, false, false, false}
        }
    };

    private bool[,,] Z_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
    {
        {
            {true , true , false, false},
            {false, true , true , false},
            {false, false, false, false},
            {false, false, false, false}
        },

        {
            {false, false, true, false},
            {false, true , true, false},
            {false, true, false, false},
            {false, false, false, false}
        },

        {
            {false, false, false, false},
            {true , true , false, false},
            {false, true , true , false},
            {false, false, false, false}
        },

        {
            {false, true , false, false},
            {true , true , false, false},
            {true , false, false, false},
            {false, false, false, false}
        }
    };

    //////////////////////////////////////////////////////////////////////

    //MakeNewTet passes along the colour and 3D shape array to Tetromino_Base, according to which shape currently needs to be drawn

    void Awake() {
        TetRandomiser();
    }

    public void MakeNewTet() {

        string TheColour;
        bool[,,] TheShape;

        if (CurrentShapeAccess < 6) { CurrentShapeAccess++; }
        else if (CurrentShapeAccess == 6) {
            TetRandomiser();
            CurrentShapeAccess = 0;
        }

        switch (Shapes[CurrentShapeAccess]) {
            case 'I':
                TheColour = "Yellow";
                TheShape = I_Shape;
                break;
            case 'L':
                TheColour = "Light Blue";
                TheShape = L_Shape;
                break;
            case 'J':
                TheColour = "Dark Blue";
                TheShape = J_Shape;
                break;
            case 'O':
                TheColour = "Purple";
                TheShape = O_Shape;
                break;
            case 'S':
                TheColour = "Orange";
                TheShape = S_Shape;
                break;
            case 'T':
                TheColour = "Green";
                TheShape = T_Shape;
                break;
            case 'Z':
                TheColour = "Red";
                TheShape = Z_Shape;
                break;
            default:
                TheColour = "Green";
                TheShape = T_Shape;
                break;
        }

        GetComponent<Tetromino_Base>().init(TheColour, TheShape);

    }

    private void TetRandomiser() {

        for(int i = 0; i < 7; i++) {

            int RandomNum1 = 0;
            int RandomNum2 = 0;

            while (RandomNum1 == RandomNum2) { RandomNum1 = Random.Range(0, 6); RandomNum2 = Random.Range(0, 6); }

            char temp = Shapes[RandomNum1];
            Shapes[RandomNum1] = Shapes[RandomNum2];
            Shapes[RandomNum2] = temp;

        }//end for

    }//end func

    //Calls MakeNewTet on starting (for the time being)
    void Start() {
        MakeNewTet();
    }
}