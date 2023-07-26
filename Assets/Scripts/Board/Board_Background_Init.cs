using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Background_Init : MonoBehaviour {

    public GameObject BackgroundTile;
    private GameObject Background;

    
    void Awake() {

        float CurrentScale = GameObject.Find("Tetris_Board_Empty").GetComponent<Display_Tetris_Board>().ReturnGridScale();

        Background = Instantiate(BackgroundTile, new Vector3(CurrentScale, CurrentScale, -1.0f), Quaternion.identity);

        float Length = ( (GameObject.Find("Tetris_Board_Empty").GetComponent<Display_Tetris_Board>().ReturnBoardLength()-1) * CurrentScale );
        float Height = ( (GameObject.Find("Tetris_Board_Empty").GetComponent<Display_Tetris_Board>().ReturnBoardHeight()-2) * CurrentScale );
        Height -= 0.1f;

        Vector3 NewScale = new Vector3(Length - CurrentScale, Height - CurrentScale, -1.0f);
        Background.transform.localScale = NewScale;

        Vector3 NewPosition = GameObject.Find("Tetris_Board_Empty").GetComponent<Display_Tetris_Board>().ReturnBoardStart();
        NewPosition += new Vector3(CurrentScale/2, CurrentScale/2, 0.0f);

        Background.transform.position = NewPosition;

    }
    
}
