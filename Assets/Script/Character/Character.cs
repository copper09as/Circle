
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    [Range(0, 10000)]
    [SerializeField] private int maxHealth;

    private int _currentHealth;
    private float defeat = 20;
    [SerializeField] public int currentHealth
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
            SetHealthBar();
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
    public void TakeDamage(int damage)
    {
        currentHealth -= (int)(damage * ((100 - defeat) / 100));
        if (currentHealth < 0)
        {
            Debug.Log("dead");
        }
        SetHealthBar();
    }
}
