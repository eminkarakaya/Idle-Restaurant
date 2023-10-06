using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GoldAnim : MonoBehaviour
{
    [SerializeField] private AudioClip [] goldClips;
    AudioSource audioSource;
    [SerializeField] GameObject goldAnimPrefab;
    [SerializeField] GameObject ovenAnimPrefab;
    private static GoldAnim _instance;
    public static GoldAnim Instance{get =>_instance; private set{}}
    [SerializeField] float distanceFactor,radius;
    [SerializeField] GameObject goldPrefab,goldinScene;
    [SerializeField] Ease ease;
    [SerializeField] Text goldText;
    [SerializeField] Transform parent;
    [SerializeField] Transform goldSpawnTransform;
    int gold;
    void Awake()
    {
        _instance = this;
    }
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        parent = GameManager.instance.transform.GetChild(1);
        goldinScene = GameManager.instance.transform.GetChild(1).GetChild(1).GetChild(1).transform.gameObject;
    }
    void SetGold(int count)
    {
        gold += count;
        goldText.text = gold.ToString();
    }
    public void EarnGoldAnim2(int earnedGold , Transform transform)
    {
        var pos = new Vector3(transform.position.x,transform.position.y+3,transform.position.z);
        var obj = Instantiate(goldAnimPrefab,new Vector3(transform.position.x,transform.position.y + 2,transform.position.z),Quaternion.identity);
        obj.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text = GameManager.CaclText(earnedGold);
        obj.transform.DOMove(pos,1f);
        Color color = new Color(255,255,255,0);
        //obj.GetComponent<SpriteRenderer>().DOColor(color,1);
        obj.transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().material.DOFade(0,1).OnComplete(()=> Destroy(obj)).OnComplete(()=>Destroy(obj.transform.GetChild(0).GetChild(1).gameObject));
        GameManager.instance.SetMoney(earnedGold);
    }
    public void OvenAnim(Transform transform)
    {
        var pos = new Vector3(transform.position.x,transform.position.y+3,transform.position.z);
        var obj = Instantiate(ovenAnimPrefab,new Vector3(transform.position.x,transform.position.y + 2,transform.position.z),Quaternion.identity);
        obj.transform.DOMove(pos,1f);
        Color color = new Color(255,255,255,0);
        //obj.GetComponent<SpriteRenderer>().DOColor(color,1);
        Destroy(obj,1);
        // obj.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.DOFade(0,1).OnComplete(()=> Destroy(obj)).OnComplete(()=>Destroy(obj.transform.GetChild(0).gameObject));
    }
    public IEnumerator EarnGoldAnim(int earnedGold , int count , Transform transform)
    {
        // GameManager.instance.idleMoneyCanvas.GetComponent<Canvas>().enabled = false;
        var earnedGold15 =  earnedGold / count;
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(goldPrefab,goldSpawnTransform.position,Quaternion.identity, parent);
            list.Add(obj);
        }
        for (int i = 0; i < count; i++)
        {
            var x = distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i*radius);
            var y = distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i*radius);
            var newPos = new Vector3(x,y,0) + goldSpawnTransform.position;

            list[i].transform.DOMove(newPos,.5f);
        }
        for (int i = 0; i < count; i++)
        {
            list[i].transform.DOMove(goldinScene.transform.position,.5f).SetEase(ease).OnComplete(()=> GameManager.instance.SetMoney(earnedGold15)).OnComplete(()=>
            {
                // if(!audioSource.isPlaying)
                    audioSource.PlayOneShot(GetRandomAudio());

            });//.OnComplete(()=> goldFlare.Play());
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < count; i++)
        {
            Destroy(list[i]);
        }
        // goldText.transform.DOScale(Vector3.one *1.5f,.4f).OnComplete(()=>goldText.transform.DOScale(Vector3.one,.4f));
    }
    private AudioClip GetRandomAudio()
    {
        return goldClips[Random.Range(0,goldClips.Length)];
    }
}
