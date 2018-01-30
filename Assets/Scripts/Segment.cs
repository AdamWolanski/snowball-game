using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SegmentType
{
    HILL,
    JUMP_START,
    JUMP_END
}

public class Segment : MonoBehaviour {

    public float StartAnchorY;
    public float EndAnchorY;
    public float SegWidth = 9.6f;
    public SegmentType SegmentType;
    public float obstacleChance;

    public Object[] obstacles;
    private List<Transform> anchors = new List<Transform>();

    private void Start ()
    {
        obstacles = Resources.LoadAll("Obstacles", typeof(GameObject));

        foreach(Transform child in transform)
        {
            anchors.Add(child);
        }

        GenerateObstacles();
	}

    private void GenerateObstacles()
    {
        for (int i = 0; i < anchors.Count; ++i)
        {
            var rnd = Random.Range(0, 100);
            if (rnd > obstacleChance)
            {
                GameObject x = Instantiate(obstacles[Random.Range(0, obstacles.Length)], anchors[i].position, anchors[i].rotation) as GameObject;
                x.transform.localPosition -= x.gameObject.transform.GetChild(0).localPosition;
                x.transform.parent = transform;
            }
        }
    }
}
