using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    // 기존 작업
    public Inventory bag;
    public bool isColItem;
    public System.Action<ITem, bool> OnColItem;
    public System.Action OnGetItem;
    public Transform onHiDagem;
    public int maxHP = 100;
    int hp;

    public Slider sliderHP;
    public Slider sliderMP;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            sliderHP.value = hp;
        }
    }

    public int maxMP = 100;
    int mp;


    public int MP
    {
        get { return mp; }
        set
        {
            mp = value;
            sliderMP.value = mp;
        }
    }
    void Start()
    {
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    public void GetItem(ITem item)
    {
        bag.GetItem(item);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ITem")
        {
            ITem colItem =other.GetComponent<ITem>();
            OnColItem(colItem, isColItem);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ITem")
        {
            ITem colItem = other.GetComponent<ITem>();
            OnColItem(colItem, !isColItem);
        }
    }
}
