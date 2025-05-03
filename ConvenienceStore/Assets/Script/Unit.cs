using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum UnitType 
    {
        Angle,
        Elf,
        BigZombie,
        Dwarf,
        Lizard,
        Boom

    }

    [SerializeField] private UnitType uType;
    public UnitType UType {get{return uType;}}
    [SerializeField] protected float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    public GameObject target = null;

    private bool IsContact = false;
    [SerializeField]
    private float followRange;
    protected Vector3 Pos = Vector3.zero; //�ڱ� ��ġ 

    protected void Follow(Transform target)
    {
        if (target == null)
        {
            Pos = this.transform.position;
           
        }

        
        
            
        this.transform.position =  Vector3.Lerp(this.transform.position, target.position, moveSpeed * Time.deltaTime); //  a  t  b
        

    }
    public void SetTarget(GameObject other)
    {

        target = other;
    }

/*
    private bool IsRange()
    {
        if(target == null)
        {
            return false;
        }
        float dx = Mathf.Abs(target.transform.position.x - this.transform.position.x);
        float dy = Mathf.Abs(target.transform.position.y - this.transform.position.y);

        float minfx = Mathf.Abs(this.transform.position.x - followRange);
        float maxfy = Mathf.Abs(this.transform.position.y - followRange);
        float maxfx = Mathf.Abs(this.transform.position.x + followRange);
        float minfy = Mathf.Abs(this.transform.position.y + followRange);
        if((minfx<=dx && maxfx>= dx) && (minfy<= dy && maxfy>=dy))
        {
            return true;
            

        }
        return false;
    }
    */
    private bool IsRange()
{
    if (target == null)
        return false;

    float distance = Vector2.Distance(transform.position, target.transform.position);
    return distance <= followRange;
}


    protected void Update()
    {
        IsContact = IsRange();
        if(IsContact == false && target != null)
        {

        Follow(target.transform);
        }

        
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SetTarget(other.gameObject);
        }
    }


 private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRange);
    }


}
