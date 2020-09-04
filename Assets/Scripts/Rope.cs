using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

    LineRenderer lineRenderer;
    List<GameObject> listLink = new List<GameObject>();

    [SerializeField] GameObject anchor;
    LineRenderer anchorRenderer;
    GameObject candy;

    [SerializeField] int linkPerUnit = 5;
    [SerializeField] bool useRadius = false;
    [SerializeField] float radius = 1.0f;
    [SerializeField] float precision = 0.5f;

    bool isCreate = false;
    bool isDestroy = false;
    int linkCount = 0;

    void CreateRope()
    {
        if (!isCreate)
        {
            //calculate distance and link count
            Vector3 direction = candy.transform.position - anchor.transform.position;
            float lenght = direction.magnitude + precision;
            linkCount = (int)(lenght * linkPerUnit);

            //instantiate and connect link
            GameObject lastLink = null;
            for (int i = 0; i < linkCount; i++)
            {
                listLink.Add(new GameObject());
                listLink[i].name = "Link(" + i + ")";
                listLink[i].tag = "Link";
                listLink[i].layer = LayerMask.NameToLayer("Rope");
                listLink[i].transform.parent = transform;
                
                Rigidbody2D rigidbody = listLink[i].AddComponent<Rigidbody2D>();
                rigidbody.mass = 1.5f;
                rigidbody.drag = 0.05f;
                rigidbody.angularDrag = 0.25f;

                float size = lenght / linkCount;

                BoxCollider2D collider = listLink[i].AddComponent<BoxCollider2D>();
                collider.size = new Vector2(0.075f, size);
                collider.isTrigger = false;

                HingeJoint2D hinge = listLink[i].AddComponent<HingeJoint2D>();
                hinge.autoConfigureConnectedAnchor = false;
                hinge.anchor = new Vector2(0.0f, -size / 2);

                if (lastLink == null)
                {
                    hinge.connectedBody = anchor.GetComponent<Rigidbody2D>();
                    hinge.connectedAnchor = Vector2.zero;
                }
                else
                {
                    hinge.connectedBody = lastLink.GetComponent<Rigidbody2D>();
                    hinge.connectedAnchor = new Vector2(0.0f, size / 2);
                }

                lastLink = listLink[i];
            }

            HingeJoint2D candyHinge = candy.AddComponent<HingeJoint2D>();
            candyHinge.autoConfigureConnectedAnchor = false;
            candyHinge.anchor = Vector2.zero;
            candyHinge.connectedBody = lastLink.GetComponent<Rigidbody2D>();
            candyHinge.connectedAnchor = new Vector2(0.0f, lenght / linkCount / 2);

            isCreate = true;
        }
    }
    public void DestroyLink()
    {
        lineRenderer.enabled = false;
        int size = listLink.Count;
        for (int i = 0; i < size; i++)
        {
            Destroy(listLink[0]);
            listLink.RemoveAt(0);
        }

        isDestroy = true;
    }
    void UpdateRenderer()
    {
        if (!isDestroy)
        {
            Vector3[] position = new Vector3[listLink.Count];
            for (int i = 0; i < listLink.Count; i++)
            {
                position[i] = listLink[i].transform.position;
            }

            lineRenderer.positionCount = listLink.Count;
            lineRenderer.SetPositions(position);
        }
    }

    void Awake()
    {
        anchorRenderer = anchor.GetComponent<LineRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        candy = GameObject.FindGameObjectWithTag("Candy");

        if (!useRadius)
        {
            CreateRope();
        }
    }

    void Update()
    {
        if (useRadius && !isCreate)
        {
            if (Vector3.Distance(anchor.transform.position, candy.transform.position) < radius)
            {
                SoundManager.instance.PlaySound(SoundType.CandyLink);
                CreateRope();
            }

            Vector3[] tab = CreateCircle(anchor.transform.position, radius, 30);
            anchorRenderer.positionCount = tab.Length;
            anchorRenderer.SetPositions(tab);
        }

        UpdateRenderer();
    }

    private void OnDrawGizmos()
    {
        if (useRadius)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(anchor.transform.position, radius);
        }
    }

    public static Vector3[] CreateCircle(Vector3 center, float radius, int nbPoints)
    {
        Vector3[] result = new Vector3[nbPoints];
        Vector3 direction = Vector3.right * radius;

        float angle = 360.0f / nbPoints;

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = new Vector2();
            result[i].x = center.x + direction.x * Mathf.Cos(angle * i * Mathf.PI / 180.0f) - direction.y * Mathf.Sin(angle * i * Mathf.PI / 180.0f);
            result[i].y = center.y + direction.x * Mathf.Sin(angle * i * Mathf.PI / 180.0f) - direction.y * Mathf.Cos(angle * i * Mathf.PI / 180.0f);
            result[i].z = 0.0f;
        }

        return result;
    }
}
