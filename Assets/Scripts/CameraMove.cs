using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
    public Transform camTransform;
    public bool kilitlen;
    public static CameraMove instance {get;private set;}
    public float mapMinX, mapMaxX,mapMinZ,MapMaxZ;
    [SerializeField] float moveSpeed;
    public Vector3 newPos;
    public Vector3 dragStartPos;
    public Vector3 dragCurrPos;
    public float zoomMiktari;
    public float maxZoom;
    public float minZoom;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        newPos = transform.position;
    }
    void Update()
    {
        if(!kilitlen)
            HandleMouseInput();
    }
    public void MoveTarget(Vector3 targetPos)
    {
        this.transform.DOMove(targetPos,.5f);
    }
    void HandleMouseInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float enrty;
            if(plane.Raycast(ray,out enrty))
            {
                dragStartPos = ray.GetPoint(enrty);
            }
        }
        if(Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float enrty;
            if(plane.Raycast(ray,out enrty))
            {
                dragCurrPos = ray.GetPoint(enrty);
                newPos = transform.position + dragStartPos - dragCurrPos;
            }
        }
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if(zoomMiktari < minZoom)
            {
                var zoom = 80f*Time.deltaTime;
                zoomMiktari += zoom;
                var vec3 = Vector3.zero;
                vec3.z += zoom;
                camTransform.Translate(vec3,Space.Self);
            }
            else
                zoomMiktari = minZoom;
        }
        if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if(zoomMiktari > maxZoom)
            {
                var zoom = 80f*Time.deltaTime;
                zoomMiktari -= zoom;
                var vec3 = Vector3.zero;
                vec3.z -= zoom;
                camTransform.Translate(vec3,Space.Self);
            }
            else
                zoomMiktari = maxZoom;
        }
        transform.position = Vector3.Lerp(transform.position,newPos,Time.deltaTime*moveSpeed);
        transform.position = ClampCam(this.transform.position);
    }
    private Vector3 ClampCam(Vector3 targetPos)
    {
        float minX = mapMinX;
        float maxX = mapMaxX;
        float minZ = mapMinZ;
        float maxZ = MapMaxZ;
        float newX = Mathf.Clamp(targetPos.x,minX,maxX);
        float newZ = Mathf.Clamp(targetPos.z,minZ,maxZ);
        return new Vector3(newX,targetPos.y,newZ);
    }
}
