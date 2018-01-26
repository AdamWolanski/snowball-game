using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {
    private List<Transform> t;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            t.Add(child);
        }
    }

}
