using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public GameObject UIItem;
    public Transform parent;

    void Open()
    {
        this.gameObject.SetActive(true);
    }

    void Close()
    {

        this.gameObject.SetActive(false);
    }
    public void AddItem(ITem item)
    {
        var obj = Instantiate(UIItem, this.parent);
        UIITem uiItem = obj.GetComponent<UIITem>();
        uiItem.Init(item);

    }


}
