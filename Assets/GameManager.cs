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

    // Start is called before the first frame update
    void Start()
    {
        InitializeGrid();
        PositionCamera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeGrid()
    {
        grid = new int[(int)gridSize.x, (int)gridSize.y];

        if (SlotPrefab)
        {
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    Instantiate(SlotPrefab, new Vector3(i * gridSpacing, 0, j * gridSpacing), Quaternion.identity);
                }
            }
        }
    }

    public void PositionCamera()
    {
        Camera camRef = Camera.main;
        camRef.transform.position = new Vector3((gridSize.x * gridSpacing / 2) - (gridSpacing / 2), 10, (gridSize.y * gridSpacing / 2) - (gridSpacing / 2));
        camRef.orthographicSize = Mathf.Max((gridSize.y * gridSpacing + 1) / 2, (gridSize.x * gridSpacing + 1) / 2);
    }

    public void UpdateBoardState()
    {

    }
}
