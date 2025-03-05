using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public static PathGenerator Instance;

    [Header("路径生成参数")]
    public int pathCount = 5; // 生成的路径数量
    public int segmentCount = 6; // 每条路径的线段数
    public float minSegmentLength = 0.5f; // 最小线段长度
    public float maxSegmentLength = 2.0f; // 最大线段长度
    public float turnChance = 0.2f; // 转弯概率

    [Header("齿轮参数")]
    public float baseRotationSpeed = 5f; // 基础转速（度/秒）
    public float sizeSpeedRatio = 0.3f; // 尺寸转速比

    [Header("线段预制体")]
    public GameObject lineSegmentPrefab; // 线段预制体（需配置LineRenderer）

    private Vector2 origin = Vector2.zero; // 起点
    private List<List<Vector2>> paths = new List<List<Vector2>>(); // 记录所有路径
    private HashSet<Vector2> usedPositions = new HashSet<Vector2>(); // 记录已使用的位置

    private Vector2[] directions = new Vector2[]
    {
        new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1), new Vector2(-1, 1),
        new Vector2(-1, 0), new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1)
    };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GeneratePaths();
        DrawPaths();
    }

    void GeneratePaths()
    {
        for (int i = 0; i < pathCount; i++)
        {
            List<Vector2> path = new List<Vector2>();
            Vector2 currentPos = origin;
            path.Add(currentPos);
            usedPositions.Add(currentPos);

            Vector2 currentDirection = directions[Random.Range(0, directions.Length)];
            float currentLength = Random.Range(minSegmentLength, maxSegmentLength);

            for (int j = 0; j < segmentCount; j++)
            {
                Vector2 newDirection = GetNextDirection(currentDirection);
                Vector2 nextPos = currentPos + newDirection * currentLength;

                if (!usedPositions.Contains(nextPos))
                {
                    path.Add(nextPos);
                    usedPositions.Add(nextPos);
                    currentPos = nextPos;
                    currentDirection = newDirection;
                    currentLength = Random.Range(minSegmentLength, maxSegmentLength);
                }
                else
                {
                    newDirection = GetNextDirection(currentDirection, true);
                    nextPos = currentPos + newDirection * currentLength;
                    if (!usedPositions.Contains(nextPos))
                    {
                        path.Add(nextPos);
                        usedPositions.Add(nextPos);
                        currentPos = nextPos;
                        currentDirection = newDirection;
                        currentLength = Random.Range(minSegmentLength, maxSegmentLength);
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
            GameObject pathObject = new GameObject($"GearPath_{i}");
            SegmentRotation prevSegment = null;

            for (int j = 0; j < paths[i].Count - 1; j++)
            {
                // 实例化线段预制体
                GameObject segment = Instantiate(lineSegmentPrefab, pathObject.transform);
                segment.name = $"Gear_{j}";

                // 获取LineRenderer组件
                LineRenderer lr = segment.GetComponent<LineRenderer>();
                lr.SetPosition(0, paths[i][j]);
                lr.SetPosition(1, paths[i][j + 1]);

                // 添加旋转控制器
                SegmentRotation rot = segment.AddComponent<SegmentRotation>();
                rot.Initialize(lr, null);

                // 连接齿轮
                if (prevSegment != null) prevSegment.nextGear = rot;
                prevSegment = rot;
            }
        }
    }
}