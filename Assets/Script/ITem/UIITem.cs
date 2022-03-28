using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIITem : MonoBehaviour
{

    public Text txtName;
    public Image ImgIcon;
    public void Init(ITem item)
    {
        this.txtName.text = item.name;

    }
}
