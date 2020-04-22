/*////
//Written by Jacob Rosengren
//Date: April 2020
////*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    #region Variables
    private LineRenderer lr;
    #endregion

    #region Functions
    private void Start () {
        lr = GetComponent<LineRenderer>();
    }
	
	private void Update () {
		lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if (hit.collider) {
                lr.SetPosition(1, hit.point);
            }
        }
        else { lr.SetPosition(1, transform.forward * 5000); }
	}
    #endregion
}
