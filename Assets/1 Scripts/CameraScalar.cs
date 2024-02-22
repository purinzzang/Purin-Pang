using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScalar : MonoBehaviour
{
    Board board;
    public GameObject background;
    public float cameraOffset;
    public float aspectRatio = 0.625f;
    public float padding;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        if (board != null)
        {
            RepositionCamera(board.width - 1, board.height - 1);
        }
    }

    void RepositionCamera(float x, float y)
    {
        Vector3 tempPos = new Vector3(x/2, y/2, cameraOffset);
        transform.position = tempPos;
        if(board.width >= board.height)
        {
            Camera.main.orthographicSize = ((board.width / 2 + padding) / aspectRatio) + 2; 
        }
        else
        {
            Camera.main.orthographicSize = (board.height / 2 + padding) + 2; 
        }
    }
}
