﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public LayerMask layermask;
    public float viewDistance;
    public float fov;

    
    private Mesh mesh;
    private EnemyController enemyController;
    
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();


        GetComponent<MeshFilter>().mesh = mesh;
        enemyController = transform.parent.GetComponent<EnemyController>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = Vector3.zero;
        int rayCount = 30;
        float angle = 135f;
        float angleIncrease = fov / rayCount;
    
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;

            RaycastHit raycast;
            if (Physics.Raycast(transform.position, transform.TransformDirection(AngleToVector(angle)), out raycast, viewDistance, layermask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(AngleToVector(angle)) * 10, Color.yellow);
                vertex = origin + AngleToVector(angle) * raycast.distance;
                if (raycast.transform.gameObject.CompareTag("AlertObj"))
                {
                    Debug.Log("Alerted");
                    enemyController.AlertObj(raycast);

                } 
                else if (raycast.transform.gameObject.CompareTag("Player"))
                {
                    enemyController.ShootPlayer(raycast.transform);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(AngleToVector(angle)) * 10, Color.yellow);
                vertex = origin + AngleToVector(angle) * viewDistance;
            }
            vertices[vertexIndex] = vertex;
            
            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex -1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public static Vector3 AngleToVector(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), 0f,Mathf.Sin(angleRad)); 
    }


}
