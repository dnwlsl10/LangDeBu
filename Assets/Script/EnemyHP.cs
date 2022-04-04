using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public int maxHp = 5;
    int hp;

    public Slider SliderHP;

    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            SliderHP.value = hp;
        }
    }

    void Start()
    {
        SliderHP.maxValue = maxHp;
        HP = maxHp;
    }

    void Update()
    {

    }
}
