using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


//useless scrip right now but can be used in future to manage the object pooling 
public class DropAndCollectionManager : MonoBehaviour
{
    public void CollectResources(GameObject _resource)
    {
        switch (_resource.GetComponent<Resource>().resource.resourceName)
        {
            case EResources.None: break;

            case EResources.Bone:
                {
                    GameManager.Instance.gameStats.bone += _resource.GetComponent<Resource>().amount;
                    //Destroy(_resource.gameObject);
                    GameManager.Instance.gameStats.boneText.text = ": " + GameManager.Instance.gameStats.bone;
                    break;
                }
            case EResources.Wood:
                {
                    GameManager.Instance.gameStats.wood += _resource.GetComponent<Resource>().amount;
                    //Destroy(_resource.gameObject);
                    GameManager.Instance.gameStats.woodText.text = ": " + GameManager.Instance.gameStats.wood;
                    break;
                }
            case EResources.Stone:
                {
                    GameManager.Instance.gameStats.stone += _resource.GetComponent<Resource>().amount;
                    //Destroy(_resource.gameObject);
                    GameManager.Instance.gameStats.stoneText.text = ": " + GameManager.Instance.gameStats.stone;
                    break;
                }
            case EResources.Iron:
                {
                    GameManager.Instance.gameStats.iron += _resource.GetComponent<Resource>().amount;
                    //Destroy(_resource.gameObject);
                    GameManager.Instance.gameStats.ironText.text = ": " + GameManager.Instance.gameStats.iron;
                    break;
                }
        }
        PoolManager.Instance.AddToPool(_resource.gameObject,EPool.Resource);
    }
}
