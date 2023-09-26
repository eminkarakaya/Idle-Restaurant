using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectManager : MonoBehaviour
{
    public static SelectManager instance;
    bool isMoved;
    bool isPointerOverGameObject = false;
    [SerializeField] private GameObject _backBtn;
    private GameObject _willBeClosedPanel;
    [SerializeField] private Department _selectedObject;
    [SerializeField] private Vector3 _oldPos;
    void Awake()
    {
        instance=this;
    }
   
    void Update()
    {
        Select();
    }
    
    public void Select()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Debug.Log(EventSystem.current.currentSelectedGameObject,EventSystem.current.currentSelectedGameObject);
                isPointerOverGameObject = true;
                return;
            }
        }
        if (Input.touchCount > 0 && _selectedObject == null)
        {
            if(CameraMove.instance.lockUp) return;
            Touch parmak = Input.GetTouch(0);
            if (parmak.phase == TouchPhase.Moved)
            {
                isMoved = true;
            }
            
            if (parmak.phase == TouchPhase.Ended && _selectedObject == null && !isMoved)
            {
                if(isPointerOverGameObject == true)
                {
                    isPointerOverGameObject = false;
                    return;
                }
                if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    Debug.Log(EventSystem.current.currentSelectedGameObject + " 2");
                    return;
                }
                RaycastHit hit = CastRay();
                if(hit.collider == null)
                    return;
                if(hit.collider.TryGetComponent(out Department department))
                {
                    department.selectableCollider.enabled = false;
                        
                    if(department.isLocked)
                    {
                        department.lockedPanel.GetComponent<Canvas>().enabled = true;
                        _willBeClosedPanel = department.lockedPanel;
                    }
                    else
                    {
                        department.dataPanel.GetComponent<Canvas>().enabled =true;
                        _willBeClosedPanel = department.dataPanel;
                    }

                    CameraMove.instance.MoveTarget(department.camPlace.position);
                    department.oldCamPlace = CameraMove.instance.transform;
                    _selectedObject = department;
                    _backBtn.gameObject.SetActive(true);
                    _oldPos = department.oldCamPlace.position;
                    CameraMove.instance.lockUp = true;
                    _selectedObject.level.OffAllColliders();
                    
                }
                isMoved = true;
            }
            if (parmak.phase == TouchPhase.Ended)
               isMoved = false;
        }
    }
    public void BackButton()
    {
        CameraMove.instance.lockUp = false;
        _selectedObject.GetComponent<Collider>().enabled = true;
        CameraMove.instance.MoveTarget(_oldPos);
        _backBtn.gameObject.SetActive(false);
        _selectedObject.level.OnAllColliders();
        _selectedObject = null;
        _willBeClosedPanel.GetComponent<Canvas>().enabled = false;
        // _kapatilacakPanel.SetActive(false);
        
    }
    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane
        );
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane
        );
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear,worldMousePosFar - worldMousePosNear, out hit ,Mathf.Infinity);
        
        return hit;
    }
}
