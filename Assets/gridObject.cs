using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    // information about the type of object stored in this grid is held here.
    //probably will not use

    // location of the grid space in the array

    int[] locationInArray = new int[2];

    //state the grid space is in (empty, X, O)

    int state = 0;

    public GridObject(int[] location)
    {
        locationInArray = location;
    }

    public void updateState(int newState)
    {
        state = newState;
    }
}
