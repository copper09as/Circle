using UnityEngine;

// 方向枚举（16个方向）
public enum Direction
{
    N, NE, E, SE, S, SW, W, NW,
    NNE, ENE, ESE, SSE, SSW, WSW, WNW, NNW
}

// 材料（元素）类，每个材料代表一个方向
[System.Serializable]
public class MaterialElement
{
    public string name;
    public Direction direction;
    public Sprite sprite;  // UI显示
}
