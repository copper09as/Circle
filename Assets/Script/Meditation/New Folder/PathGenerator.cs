using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public int pathCount = 5; // ���ɵ�·������
    public int segmentCount = 6; // ÿ��·���ж��ٸ��㣨�߶��� + 1��
    public float segmentLength = 1.0f; // ÿ���߶εĳ���
    public float turnChance = 0.2f; // �趨ת��ĸ��ʣ�0~1��
    public float rotationSpeed = 1.0f; // ��ת�ٶȣ�����ÿ���߶���ת�Ŀ���

    private Vector2 origin = Vector2.zero; // ���
    private List<List<Vector2>> paths = new List<List<Vector2>>(); // ��¼����·��
    private HashSet<Vector2> usedPositions = new HashSet<Vector2>(); // ��¼�Ѿ�ʹ�õ�λ�ã���ֹ����

    private Vector2[] directions = new Vector2[]
    {
        new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1), new Vector2(-1, 1),
        new Vector2(-1, 0), new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1)
    };

    void Start()
    {
        GeneratePaths();
        DrawPaths(); // ����·�����������
    }

    void GeneratePaths()
    {
        for (int i = 0; i < pathCount; i++)
        {
            List<Vector2> path = new List<Vector2>();
            Vector2 currentPos = origin;
            path.Add(currentPos);
            usedPositions.Add(currentPos);

            Vector2 currentDirection = directions[Random.Range(0, directions.Length)]; // �����ʼ����

            for (int j = 0; j < segmentCount; j++)
            {
                Vector2 newDirection = GetNextDirection(currentDirection);
                Vector2 nextPos = currentPos + newDirection * segmentLength;

                if (!usedPositions.Contains(nextPos))
                {
                    path.Add(nextPos);
                    usedPositions.Add(nextPos);
                    currentPos = nextPos;
                    currentDirection = newDirection;
                }
                else
                {
                    // �������������·������������
                    newDirection = GetNextDirection(currentDirection, true);
                    nextPos = currentPos + newDirection * segmentLength;
                    if (!usedPositions.Contains(nextPos))
                    {
                        path.Add(nextPos);
                        usedPositions.Add(nextPos);
                        currentPos = nextPos;
                        currentDirection = newDirection;
                    }
                }
            }

            paths.Add(path);
        }
    }

    Vector2 GetNextDirection(Vector2 lastDirection, bool forceTurn = false)
    {
        List<Vector2> validDirections = new List<Vector2>();

        foreach (Vector2 dir in directions)
        {
            float angle = Vector2.Angle(lastDirection, dir);
            if (forceTurn || angle < 45f || (Random.value > turnChance && angle < 90f))
            {
                validDirections.Add(dir);
            }
        }

        return validDirections[Random.Range(0, validDirections.Count)];
    }

    void DrawPaths()
    {
        for (int i = 0; i < paths.Count; i++)
        {
            // ����һ���µ� GameObject ���ڱ�ʾ����·��
            GameObject pathObject = new GameObject("Path_" + i);

            // ����ÿһ��·���еĵ㣬��ÿ�������ڵĵ�����һ��������LineRenderer
            for (int j = 0; j < paths[i].Count - 1; j++)
            {
                GameObject segmentObject = new GameObject("Segment_" + j);
                segmentObject.transform.SetParent(pathObject.transform);

                LineRenderer lineRenderer = segmentObject.AddComponent<LineRenderer>();

                lineRenderer.positionCount = 2;
                lineRenderer.startWidth = 0.1f;
                lineRenderer.endWidth = 0.1f;
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;

                lineRenderer.SetPosition(0, new Vector3(paths[i][j].x, paths[i][j].y, 0f));
                lineRenderer.SetPosition(1, new Vector3(paths[i][j + 1].x, paths[i][j + 1].y, 0f));

                // ����Ҷ�任����ת
                segmentObject.AddComponent<SegmentRotation>().Initialize(lineRenderer, rotationSpeed);
            }
        }
    }
}