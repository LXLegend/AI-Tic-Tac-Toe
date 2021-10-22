using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceAnimation : MonoBehaviour
{
    public Vector3 initialPos;

    public Vector3 finalPosOffset = Vector3.up * 0.125f;

    private Vector3 finalPos;

    private float currentTime = 0f;

    public float animTime = .8f;

    public float delay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;

        finalPos = initialPos + finalPosOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < animTime + delay && delay < currentTime)
        {
            transform.position = initialPos + Mathf.Sin((currentTime / (animTime + delay)) * Mathf.PI / 2) * finalPosOffset;

            currentTime += Time.deltaTime;
        }
        else if (currentTime < animTime + delay)
        {
            currentTime += Time.deltaTime;
        }
    }
}
