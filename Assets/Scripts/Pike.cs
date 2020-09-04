using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pike : MonoBehaviour {

    [SerializeField] GameObject prefabPike;

    [SerializeField] GameObject startPike;
    [SerializeField] GameObject endPike;

    [SerializeField] float rotation = 0.0f;

    [SerializeField] int pikeCountPerUnit = 6;

	void Start ()
    {
        Vector3 direction = endPike.transform.position - startPike.transform.position;
        float lenght = direction.magnitude;
        int pikeCount = (int)(lenght * pikeCountPerUnit);

        for (int i = 0; i < pikeCount; i++)
        {
            if (i != 0)
            {
                GameObject newPike = Instantiate(prefabPike, transform);
                newPike.transform.localPosition = startPike.transform.localPosition + direction / pikeCount * i;
            }
        }

        transform.Rotate(Vector3.forward, rotation);
	}
}
