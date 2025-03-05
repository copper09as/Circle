using UnityEngine;

public class GridCell
{
    public Vector2Int position;  // 网格坐标
    public MaterialElement material;  // 当前填充的材料
    public bool isPath;  // 是否是路径上的格子

    public GridCell(Vector2Int pos)
    {
        position = pos;
        material = null;
        isPath = false;
    }
}
