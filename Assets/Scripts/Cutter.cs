using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour {

    Vector3 previousCutterPosition = Vector3.zero;
    Vector3 currentCutterPosition = Vector3.zero;
	
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            previousCutterPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetButton("Fire1"))
        {
            currentCutterPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float distance = (currentCutterPosition - previousCutterPosition).magnitude;
            if (distance >= 0.1f)
            {
                RaycastHit2D hit = Physics2D.Raycast(previousCutterPosition, currentCutterPosition - previousCutterPosition, distance);
                Debug.DrawRay(previousCutterPosition, currentCutterPosition - previousCutterPosition, Color.white, 1);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "Link")
                    {
                        SoundManager.instance.PlaySound(SoundType.RopeBleak2);
                        hit.collider.gameObject.transform.parent.GetComponent<Rope>().DestroyLink();
                    }
                }
            }

            previousCutterPosition = currentCutterPosition;
        }
	}
}
