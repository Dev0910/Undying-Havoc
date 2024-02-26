using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{
    public Esource source;
    public EResources Resources;
    public ResourcesScriptableObject resourcesScriptableObject;
    public GameObject resourcePrefab;
    public int health;

    // drop the resources according to the source
    public void DropResources(int _amount)
    {
        if (health <= 0 || _amount <= 0) { return; }
        GameStats gs = GameManager.Instance.gameStats;
        switch (source)
        {
            case Esource.None: break;

            case Esource.Tree:
                {
                    health -= _amount;
                    //gs.wood += _amount;
                    gs.woodText.text = ": " + gs.wood;
                    SpawnResource(_amount);

                }
                break;
            case Esource.Rock:
                {
                    health -= _amount;
                    //gs.stone += _amount;
                    SpawnResource(_amount);
                    gs.stoneText.text = ": " + gs.stone;
                }
                break;
            case Esource.IronOre:
                {
                    health -= _amount;
                    //gs.iron += _amount;
                    gs.ironText.text = ": " + gs.iron;
                    SpawnResource(_amount);
                }
                break;
        }
        if (health <= 0) { Destroy(gameObject); }
    }

    private void SpawnResource(int _amount)
    {
        GameObject temp = Instantiate(resourcePrefab,transform.position,resourcePrefab.transform.rotation);
        temp.transform.parent = GameObject.Find("ResourcesHolder").transform;
        temp.GetComponent<Resource>().resource = resourcesScriptableObject;
        temp.GetComponent<Resource>().SetAmount(_amount);
        temp.GetComponent<Resource>().AddForce();
        temp.GetComponent<Resource>().ChangeSprite();
    }
}
