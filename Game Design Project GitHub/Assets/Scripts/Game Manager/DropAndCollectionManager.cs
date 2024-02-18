using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DropAndCollectionManager : MonoBehaviour
{
    //[SerializeField] private GameObject coinPrefab;
    //static Queue<GameObject> qcoin;

    //private void Start()
    //{
    //    qcoin = new Queue<GameObject>();
    //}
    //public void DropGold(Vector2 spawnPosition, int amount)
    //{
    //    GameObject temperory;
    //    if(CheakQueue())
    //    {
    //        temperory = qcoin.Dequeue();
    //        temperory.SetActive(true);
    //        temperory.transform.position = spawnPosition;

    //    }
    //    else
    //    {
    //        temperory = Instantiate(coinPrefab , spawnPosition , coinPrefab.transform.rotation);
    //        temperory.transform.parent = GameObject.Find("CoinHolder").transform;
    //    }
    //    if(temperory != null)
    //    {
    //        temperory.GetComponent<Coin>().coinValue = amount;
    //    }

    //}

    //public void CollectGold(GameObject _coin)
    //{
    //    GameStats.currentGold += _coin.GetComponent<Coin>().coinValue;
    //    _coin.GetComponent<Coin>().coinValue = 0;
    //    _coin.SetActive(false);
    //    qcoin.Enqueue(_coin);
    //}

    public void CollectResources(GameObject _resource)
    {
        switch (_resource.GetComponent<Resource>().resource.resourceName)
        {
            case EResources.None: break;

            case EResources.Bone:
                {
                    GameManager.Instance.gameStats.bone += _resource.GetComponent<Resource>().amount;
                    Destroy(_resource.gameObject);
                    GameManager.Instance.gameStats.boneText.text = ": " + GameManager.Instance.gameStats.bone;
                    break;
                }
            case EResources.Wood:
                {
                    GameManager.Instance.gameStats.wood += _resource.GetComponent<Resource>().amount;
                    Destroy(_resource.gameObject);
                    GameManager.Instance.gameStats.woodText.text = ": " + GameManager.Instance.gameStats.wood;
                    break;
                }
            case EResources.Stone:
                {
                    GameManager.Instance.gameStats.stone += _resource.GetComponent<Resource>().amount;
                    Destroy(_resource.gameObject);
                    GameManager.Instance.gameStats.stoneText.text = ": " + GameManager.Instance.gameStats.stone;
                    break;
                }
            case EResources.Iron:
                {
                    GameManager.Instance.gameStats.iron += _resource.GetComponent<Resource>().amount;
                    Destroy(_resource.gameObject);
                    GameManager.Instance.gameStats.ironText.text = ": " + GameManager.Instance.gameStats.iron;
                    break;
                }
        }
    }

    //bool CheakQueue()
    //{
    //    if (qcoin.Count > 0)
    //    {
    //        return true;
    //    }
    //    return false;

    //}
}
