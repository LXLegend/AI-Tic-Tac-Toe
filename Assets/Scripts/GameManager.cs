using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Slot prefab for generating grid
    public GameObject SlotPrefab;
    public GameObject XPrefab;
    public GameObject OPrefab;

    public Vector2 gridSize = new Vector2(3, 3);

    public float gridSpacing = 1f;

    // 0 represents empty space, 1 represents X's, 2 represents O's
    int[,] grid;

    // 1 represents Player One's turn, 2 represents Player Two's turn, 0 represents game end (Yes, x could have used an enum, but that requires an additional line of code for the same thing)
    int gameState = 1;

    // if there are two players disable the AI
    public bool TwoPlayers = false;

    // does the AI make the first move?
    public bool AIFirst = false;

    // Start is called before the first frame update
    void Start()
    {
        InitializeGrid();
        PositionCamera();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case 1:
                GetPlayerInput();
                break;
            case 2:
                AIMove();
                break;
            default:
                break;
        }
        GetPlayerInput();
    }

    // x invite you to guess what it does. it places the grid prefabs based on the grid size and spacing.
    public void InitializeGrid()
    {
        grid = new int[(int)gridSize.x, (int)gridSize.y];

        if (SlotPrefab)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    Instantiate(SlotPrefab, new Vector3(x * gridSpacing, 0, y * gridSpacing), Quaternion.identity);
                }
            }
        }
    }

    // positions the camera automatically based on the grid size and grid spacing.
    public void PositionCamera()
    {
        Camera camRef = Camera.main;
        camRef.transform.position = new Vector3((gridSize.x * gridSpacing / 2) - (gridSpacing / 2), 10, (gridSize.y * gridSpacing / 2) - (gridSpacing / 2));
        camRef.orthographicSize = Mathf.Max((gridSize.y * gridSpacing + 1) / 2, (gridSize.x * gridSpacing + 1) / 2);
    }

    public void UpdateBoardState()
    {

    }

    public void AIMove()
    {
        int[] move = MiniMax.FindBestMove(grid, 2);
        // rip error checking- dw about it
        if ((move[0] != -1 && move[1] != -1))
        {
            ref int gridVal = ref grid[move[0], move[1]];
            Vector3 slotPos = new Vector3(move[0], 0, move[1]);
            if (gridVal == 0)
            {
                gridVal = gameState;
                Instantiate(OPrefab, slotPos, Quaternion.identity);
                gameState = (gameState % 2) + 1;
            }
        }
    }

    public void GetPlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(mouseRay, out hitInfo, Camera.main.transform.position.y))
            {
                Vector3 slotPos = hitInfo.transform.position;
                print(slotPos);
                ref int gridVal = ref grid[(int)slotPos.x, (int)slotPos.z];
                if (gridVal == 0)
                {
                    gridVal = gameState;
                    Instantiate(XPrefab, slotPos, Quaternion.identity);
                    //int[] move = MiniMax.FindBestMove(grid, 2);
                    //print(move[0]);
                    //print(move[1]);
                    gameState = (gameState % 2) + 1;
                }
            }
            
        }

    }
}
