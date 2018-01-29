using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public Segment StartSegment;
    public Segment[] AvaliableSegments;
    private List<Segment> _segments;
    private int segs = 100;

	private void Start ()
    {
        _segments = new List<Segment>();
        _segments.Add(StartSegment);
	}
	
	void Update ()
    {
        while (segs-- > 0)
            GenerateNextSegment(_segments[_segments.Count - 1]);
	}

    private void GenerateNextSegment(Segment lastSegment)
    {
        Segment newSegment = GetRandomSegment(lastSegment);

        Vector2 pos = new Vector2(lastSegment.transform.position.x + newSegment.SegWidth*2, lastSegment.transform.position.y + (lastSegment.EndAnchorY - newSegment.StartAnchorY));
        var x = Instantiate(newSegment, pos, Quaternion.identity);
        x.transform.parent = this.transform;
        _segments.Add(x);
    }

    private Segment GetRandomSegment(Segment lastSegment)
    {
        var rnd = Mathf.Floor(Random.Range(0, 100));
        var t = 100;
        while(t --> 0)
        {
            var newSegment = AvaliableSegments[Random.Range(0, AvaliableSegments.Length)];
            var lst = lastSegment.SegmentType;
            var nwt = newSegment.SegmentType;

            if (lst == SegmentType.HILL)
            {
                if (newSegment.SegmentType == SegmentType.JUMP_END)
                    continue;
            }
            else if (lst == SegmentType.JUMP_START)
            {
                if (newSegment.SegmentType != SegmentType.JUMP_END)
                    continue;
            }
            else if (lst == SegmentType.JUMP_END)
            {
                if (newSegment.SegmentType == SegmentType.JUMP_END)
                    continue;
            }
            return newSegment;
        }
        return null;
    }
}
