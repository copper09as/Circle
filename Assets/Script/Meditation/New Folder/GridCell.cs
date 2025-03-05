using UnityEngine;

public class GridCell
{
    public Vector2Int position;  // ��������
    public MaterialElement material;  // ��ǰ���Ĳ���
    public bool isPath;  // �Ƿ���·���ϵĸ���

    public GridCell(Vector2Int pos)
    {
        position = pos;
        material = null;
        isPath = false;
    }
}
