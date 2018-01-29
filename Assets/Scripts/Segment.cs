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

    private void Start ()
    {
        //foreach child -> add to list

	    //Generate obstacles 	
	}
}
