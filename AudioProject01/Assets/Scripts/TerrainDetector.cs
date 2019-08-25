using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Generate for detecting the terrain
/// </summary>
public class TerrainDetector : MonoBehaviour
{
    //for detecting the collision

    private CapsuleCollider capsuleCollider;
    //top and bot mid point of the capsule collider
    private Vector3 pointBottom, pointTop;
    //rad of the capsule collider result of func Physics.OverlapCapsule
    private float radius;
    //for storing 
    private Collider[] colliders;
    [HideInInspector]
    public bool isOnGround;
    public Collision curCollision;

    float colliderheight = 1.5f;
    [HideInInspector]
    public string OnGroundType;
    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        radius = capsuleCollider.radius;

    }
    //the first approach failed......
    //public bool OnGround()
    //{

    //    pointBottom = transform.position + transform.up * radius ;
    //    pointTop = transform.position + transform.up * capsuleCollider.height - transform.up * radius;
    //    LayerMask ignoreMask = ~(1 << 8);
    //    //Colliders touching or inside the capsule
    //    colliders = Physics.OverlapCapsule(pointBottom, pointTop, radius, ignoreMask);
    //    //Debug.DrawLine(pointBottom, pointTop, Color.green);
    //    if (colliders.Length != 0)
    //    {
    //        isOnGround = true;
    //        Debug.Log("onGround = true!");
    //        return true;
    //    }
    //    else
    //    {
    //        isOnGround = false;
    //        Debug.Log("onGround = false!");
    //        return false;
    //    }
    //}

    public bool OnGround()
    {
        LayerMask ignoreMask = ~(1 << 8);
        isOnGround = Physics.Raycast((transform.position), Vector3.down, 1f, ignoreMask);

        Debug.DrawRay(transform.position, Vector3.down, Color.cyan);
        return isOnGround;
    }


    public void Update()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);
        if (OnGround())
        {
            if (Physics.Raycast(transform.position,Vector3.down,out hit,colliderheight ))
            {
                OnGroundType = hit.collider.tag;
                //Debug.Log("cast ray collides with :"+hit.collider.gameObject.name);
            }
        }
    }
    
    
    private void OnCollisionEnter(Collision collision)
    {
        curCollision = collision;
        //Debug.Log("current collision with :" + collision.gameObject.name);
        //deprecated........
        //if (collision.gameObject.name == "prop_boardwalk_01")
        //{
        //    isOnGround = true;
        //}
        //else if (collision.gameObject.name == "terrain_near_01")
        //{
        //    isOnGround = true;
           
        //}
        //else if (collision.gameObject.name == "terrain_far_01")
        //{
        //    isOnGround = true;
            
        //}
        //else if (collision.gameObject.name == "Water")
        //{
        //    isOnGround = true;
        //}

    }
    private void OnCollisionExit(Collision collision)
    {
        curCollision = null;
        isOnGround = false;
        //Debug.Log("in the air!");
    }

}
