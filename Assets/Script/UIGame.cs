using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    public Player player;
    public EnemySpawner enemySpawner;
    private Enemy enemy;
    private ITem item;
    public Inventory inventory;


    public Text miniTitle;
    public float alpa;

    //UIColITem
    public GameObject colitemPopUp;
    public Button btnColItem;
    public Text txtItemName;

    //UIInvenState
    public GameObject inventoryStatePopUp;
    public Text txtInItemName;


    //UiGame
    public GameObject ingamePopup;
    public Text txtIngamemessage;
    void Start()
    {
        this.miniTitle.gameObject.SetActive(true);
        this.miniTitle.color = new Color(miniTitle.color.r, miniTitle.color.g, miniTitle.color.b, alpa);

        this.btnColItem.onClick.AddListener(() =>
        {
            Debug.Log("아이템을 먹다");
            this.player.GetItem(this.item);
        });
    }

    private void Update()
    {
        this.player.OnColItem = (item, isTrigger) =>
        {
            this.item = item;

            if (isTrigger)
            {
                this.colitemPopUp.gameObject.SetActive(true);
                this.txtItemName.text = item.name;
            }
            else
            {
                this.colitemPopUp.gameObject.SetActive(false);
            }
        };

        if(this.colitemPopUp.gameObject.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                this.player.GetItem(this.item);
            }   
        };


        this.inventory.onGetITem = (item) =>
        {

            //item.gameObject.SetActive(false);
            StopCoroutine(AniInventoryPopUp());
            StartCoroutine(AniInventoryPopUp());
        };

        this.enemySpawner.OnCreateEnemy = (obj) =>
        {
            this.enemy = obj;
            StopCoroutine(InGamePopUp());
            StartCoroutine(InGamePopUp());
        };
       
    }

    IEnumerator InGamePopUp()
    {

        this.ingamePopup.gameObject.SetActive(true);
        this.txtIngamemessage.text = "복수자 에드거에게 침입당했습니다";
        yield return new WaitForSeconds(3f);
        this.inventoryStatePopUp.SetActive(false);
    }

    IEnumerator AniInventoryPopUp()
    {
        this.inventoryStatePopUp.SetActive(true);
        this.txtInItemName.text = item.name + "을 흭득하였습니다.";
        yield return new WaitForSeconds(3f);
        this.inventoryStatePopUp.SetActive(false);
    }

    void OnMiniTitle()
    {
        if (alpa < 1)
        {
            this.miniTitle.color = new Color(miniTitle.color.r, miniTitle.color.g, miniTitle.color.b, alpa);
            this.alpa += 0.1f * Time.deltaTime;
        }
        else if (alpa > 1)
        {
            this.miniTitle.gameObject.SetActive(false);
        }
    }
}
