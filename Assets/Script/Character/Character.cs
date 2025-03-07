
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Image healthBar;
    public int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if(value < 0)
            {
                health = 0;
            }
            else
            {
                health = value;
            }

        }
    }
}
