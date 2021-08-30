using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowThePath : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public Transform[] path_Points;
    [HideInInspector] public float speed_Enemy;
    [HideInInspector] public bool is_return;

    [HideInInspector] public Vector3[] _new_Position;
    private int cur_Pos;

    private void Start()
    {
        _new_Position = NewPositionByPath(path_Points);

        transform.position = _new_Position[0];
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _new_Position[cur_Pos], speed_Enemy * Time.deltaTime);
        if (Vector3.Distance(transform.position, _new_Position[cur_Pos]) < 0.2f)
        {
            cur_Pos ++;
            if(cur_Pos >= _new_Position.Length)
            {
                if(is_return)
                {
                    cur_Pos = 0;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    Vector3[] NewPositionByPath(Transform[] pathPos)
    {
        Vector3[] pathPositions = new Vector3[pathPos.Length];
        for(int i = 0; i < path_Points.Length; i++)
        {
            pathPositions[i] = pathPos[i].position;
        }
        pathPositions = Smoothing(pathPositions);
        pathPositions = Smoothing(pathPositions);
        pathPositions = Smoothing(pathPositions);
        return pathPositions;
    }
    Vector3[] Smoothing(Vector3[] path_Positions)
    {
        Vector3[] new_Path_Positions = new Vector3[(path_Positions.Length - 2) * 2 + 2];
        new_Path_Positions[0] = path_Positions[0];
        new_Path_Positions[new_Path_Positions.Length - 1] = path_Positions[path_Positions.Length - 1];

        int j = 1;
        for (int i = 0; i < path_Positions.Length - 2; i++)
        {
            new_Path_Positions[j] = path_Positions[i] + (path_Positions[i + 1] - path_Positions[i]) * 0.75f;
            new_Path_Positions[j + 1] = path_Positions[i + 1] + (path_Positions[i + 2] - path_Positions[i + 1]) * 0.25f;
            j += 2;
        }
        return new_Path_Positions;
    }
}
