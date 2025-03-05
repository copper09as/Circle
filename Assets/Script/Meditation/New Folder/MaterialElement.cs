using UnityEngine;

// ����ö�٣�16������
public enum Direction
{
    N, NE, E, SE, S, SW, W, NW,
    NNE, ENE, ESE, SSE, SSW, WSW, WNW, NNW
}

// ���ϣ�Ԫ�أ��࣬ÿ�����ϴ���һ������
[System.Serializable]
public class MaterialElement
{
    public string name;
    public Direction direction;
    public Sprite sprite;  // UI��ʾ
}
