using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    enum UnitName 
    {
        Angle,
        Elf,
        BigZombie,
        Dwarf,
        Lizard

    }
    [SerializeField] protected float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    public GameObject Traget;

    protected Vector3 Pos = Vector3.zero; //자기 위치 

    protected void Follow(Transform target)
    {
        if (target == null)
        {
            Pos = this.transform.position;
           
        }

       this.transform.position =  Vector3.Lerp(this.transform.position, target.position, moveSpeed * Time.deltaTime); //  a  t  b


    }

    protected void Update()
    {
        Follow(Traget.transform);
    }




}
