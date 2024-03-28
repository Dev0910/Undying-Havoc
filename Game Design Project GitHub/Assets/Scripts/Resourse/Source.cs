using CustomPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{
    public Esource source;
    public EResources Resources;
    public ResourcesScriptableObject resourcesScriptableObject;
    //public GameObject resourcePrefab;
    public int health;

    // drop the resources according to the source
    public void DropResources(int _damageAmount)
    {
        if (health <= 0 || _damageAmount <= 0) { return; }

        GameStats gs = GameManager.Instance.gameStats;

        switch (source)
        {
            case Esource.None: break;

            case Esource.Tree:
                {
                    health -= _damageAmount;
                    gs.woodText.text = ": " + gs.wood;
                    SpawnResource(_damageAmount);

                }
                break;
            case Esource.Rock:
                {
                    health -= _damageAmount;
                    SpawnResource(_damageAmount);
                    gs.stoneText.text = ": " + gs.stone;
                }
                break;
            case Esource.IronOre:
                {
                    health -= _damageAmount;
                    gs.ironText.text = ": " + gs.iron;
                    SpawnResource(_damageAmount);
                }
                break;
        }
        if (health <= 0) 
        {
            //Destroy(gameObject);
            GameManager.Instance.resoursesSpawner.AddToList(gameObject, source);
        }
    }

    private void SpawnResource(int _amount)
    {
        //Instantiate(resourcePrefab, transform.position, resourcePrefab.transform.rotation);
        GameObject temp = PoolManager.Instance.TakeFromPool(EPool.Resource);
        temp.transform.position = this.transform.position;
        //temp.transform.parent = GameObject.Find("ResourcesHolder").transform;
        Resource resource = temp.GetComponent<Resource>();
        resource.resource = resourcesScriptableObject;
        resource.SetAmount(_amount);
        resource.ResetResource();
    }
}
