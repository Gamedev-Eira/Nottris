using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoConstructor : MonoBehaviour {

    private const int Shape_Size = 4;
    private const int ShapeQuantity = 4;

    private char CurrentShape = 'I' ;

    //////////////////////////////////////////////////////////////////////

    bool[,,] I_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
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

    bool[,,] L_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
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

    bool[,,] J_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
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

    bool[,,] O_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
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

    bool[,,] S_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
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

    bool[,,] T_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
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

    bool[,,] Z_Shape = new bool[ShapeQuantity, Shape_Size, Shape_Size]
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

    void MakeNewTet() {
        switch (CurrentShape) {
            case 'I':
                GetComponent<Tetromino_Base>().init("Yellow", I_Shape);
                break;
            case 'L':
                GetComponent<Tetromino_Base>().init("Light Blue", L_Shape);
                break;
            case 'J':
                GetComponent<Tetromino_Base>().init("Dark Blue", J_Shape);
                break;
            case 'O':
                GetComponent<Tetromino_Base>().init("Purple", O_Shape);
                break;
            case 'S':
                GetComponent<Tetromino_Base>().init("Orange", S_Shape);
                break;
            case 'T':
                GetComponent<Tetromino_Base>().init("Green", T_Shape);
                break;
            case 'Z':
                GetComponent<Tetromino_Base>().init("Red", Z_Shape);
                break;
        }

    }

    void Start() {
        MakeNewTet();
    }

    void Update() {
        
    }
}