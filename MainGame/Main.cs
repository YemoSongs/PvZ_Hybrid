using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    



    void Start()
    {
        Camera.main.transform.position = new Vector3(-15, 0, -10);
        Camera.main.transform.DOMoveX(25, 5).OnComplete(() => {
            UIMgr.Instance.ShowPanel<PlantCardsPanel>();
            UIMgr.Instance.ShowPanel<ChoosePlantsPanel>();
        });
    }

    
}
