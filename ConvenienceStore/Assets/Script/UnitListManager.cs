using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitListManager : MonoBehaviour
{
    [Range(1,10)][SerializeField] private int ListSize;


    [SerializeField]
    List<GameObject> unitList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject obj)
    {
        unitList.Add(obj);
        //추가를 해주고 
        //여기서 타입 비교까지 할꺼임 

    }

    private void MatchType()
    {


    }
}
