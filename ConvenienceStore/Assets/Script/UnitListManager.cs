using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

public class UnitListManager : MonoBehaviour
{
    [Range(1,10)][SerializeField] private int ListSize;

    private Transform player;

    [SerializeField]
    List<GameObject> unitList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void Interact(GameObject obj)
    {
        Unit unit = obj.GetComponent<Unit>();
        if (unit == null) return;

       
        if (unit.UType == UnitType.Boom)
        {
            if (unitList.Count > 0)
            {
                GameObject last = unitList[unitList.Count - 1];

                // Boom 유닛 자신이 마지막이 아니라면 제거
                if (last != obj)
                {
                    unitList.RemoveAt(unitList.Count - 1);
                    Destroy(last);
                    Debug.Log("Boom 꼬리 유닛 제거됨.");
                }
            }

            Destroy(obj); // Boom 유닛은 리스트에 추가되지 않고 사라짐
            return;
        }

        // 일반 유닛 처리
        if (!unitList.Contains(obj))
        {
            unitList.Add(obj);

            Transform target = (unitList.Count == 1) ? player : unitList[unitList.Count - 2].transform;
            unit.SetFollowTarget(target);

            MatchType();
        }
    }


    public void GameOver()
    {
        if (unitList.Count > ListSize)
        {
            Debug.Log("게임종료");
        }
    }



    private void MatchType()
    {
        if (unitList.Count < 3)
            return;

        int index = 0;
        MatchRecursive(index);
       
    }
    private void MatchRecursive(int index)
    {
        if (index > unitList.Count - 3)
            return;

        UnitType currentType = unitList[index].GetComponent<Unit>().UType;
        List<int> matchIndices = new List<int> { index };

        // 다음 인덱스를 재귀적으로 탐색
        int nextIndex = index + 1;
        while (nextIndex < unitList.Count && unitList[nextIndex].GetComponent<Unit>().UType == currentType)
        {
            matchIndices.Add(nextIndex);
            nextIndex++;
        }

        // 매칭된 유닛이 3개 이상이면 제거
        if (matchIndices.Count >= 3)
        {
            Debug.Log($"{currentType} 타입 {matchIndices.Count}개 매칭 → 제거");
            //score++
            // 뒤에서부터 제거
            for (int i = matchIndices.Count - 1; i >= 0; i--)
            {
                int removeIndex = matchIndices[i];
                Destroy(unitList[removeIndex]);
                unitList.RemoveAt(removeIndex);
            }

            // 제거 후 다시 처음부터 검사
            MatchRecursive(0);
        }
        else
        {
            // 다음 인덱스로 이동
            MatchRecursive(index + 1);
        }
    }
}


    