using UnityEngine;
using System.Collections;

public class MiniMapResize : MonoBehaviour {

    Resolution resolution;

	void Start () {
        resolution = Screen.currentResolution;
        print(resolution.width + ", "+ resolution.height);
	}
	
	void Update () {
	}
}
