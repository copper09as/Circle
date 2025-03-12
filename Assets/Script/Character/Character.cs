
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Range(0, 10000)]
    [SerializeField] private int maxHealth;

    private int _currentHealth;
    [SerializeField] private int currentHealth
    {
       
        get
        {
            return _currentHealth;
        }
        set
        {
            if(value < 0)
            {
                _currentHealth = 0;
            }
            else if(value > maxHealth)
            {
                _currentHealth = maxHealth;
            }
            else
            {
                _currentHealth = value;
            }
        }
    }

    [SerializeField] private SliderBar sliderBar;
    private void Start()
    {
        currentHealth = maxHealth;
        SetHealthBar();
    }
    public void SetHealthBar()
    {
        sliderBar.UpdateSliderBar(maxHealth, currentHealth, true);
    }
}
