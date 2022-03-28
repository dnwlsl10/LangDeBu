using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<ITem> Items;
    private int capacity;
    public System.Action<ITem> onGetITem;
    public UIInventory uIInventory;

    private void Start()
    {
        this.Items = new List<ITem>();
    }

    public void GetItem(ITem item)
    {
        Debug.Log("아이템을 먹었다");
        item.transform.position = this.transform.position;
        Items.Add(item);
        onGetITem(item);
        uIInventory.AddItem(item);

    }
}
