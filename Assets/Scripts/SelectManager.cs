using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectManager : MonoBehaviour
{
    public static SelectManager instance;
    bool isMoved;
    [SerializeField] private Button _backBtn;
    private GameObject _willBeClosedPanel;
    [SerializeField] private Department _selectedObject;
    [SerializeField] private Vector3 _oldPos;
    void Awake()
    {
        instance=this;
    }
    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (var item in results)
        {
            if(item.gameObject.TryGetComponent(out RectTransform rectTransform))
            {
                return true;
            
            }
        }
        return false;
}
    void Update()
    {
            
                
            Select();

    }
    
    public void Select()
    {
        if (Input.touchCount > 0 && _selectedObject == null)
        {
            if(CameraMove.instance.lockUp) return;
           Touch parmak = Input.GetTouch(0);
           if (parmak.phase == TouchPhase.Moved)
           {
               isMoved = true;
           }

            if (parmak.phase == TouchPhase.Ended && _selectedObject == null && !isMoved)
                // if (Input.GetMouseButton(0) && _selectedObject == null && !isMoved)
            {
                // PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                // eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                // List<RaycastResult> results = new List<RaycastResult>();
                // EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
                // Collider collider = null;
                // foreach (var item in results)
                // {
                //     Debug.Log(item.gameObject,item.gameObject);
                //     if(item.gameObject.TryGetComponent(out RectTransform rectTransform))
                //     {
                //         return;
                //     }
                //     if(item.gameObject.TryGetComponent(out Department department1))
                //     {
                //         collider = department1.GetComponent<Collider>();
                //     }
                // }
                // if(!EventSystem.current.IsPointerOverGameObject())
                // {
                //     return;
                // }
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
                        Debug.Log(department.dataPanel.GetComponent<Canvas>(),department.dataPanel.GetComponent<Canvas>());
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
