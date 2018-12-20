using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapsible : MonoBehaviour
{
    [SerializeField] private GameObject collapsibleContent;
    [SerializeField] private GameObject contentToSpawn;
    private int currentIndex;
    private List<GameObject> extraElements;
    private bool isExpanded = false;
    
    private void Awake()
    {
        extraElements = new List<GameObject>();
        GetCurrentChildId();
    }

    private void GetCurrentChildId()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).name == gameObject.name)
            {
                currentIndex = i;
                break;
            }
        }
    }

    public void Collapse(bool expand = false)
    {
        isExpanded = !expand ? !isExpanded : true;

        if (extraElements.Count != 0)
        {
            foreach (GameObject item in extraElements)
            {
                item.SetActive(isExpanded);
            }
        }
        collapsibleContent.SetActive(isExpanded);
        
    }

    public void SpawnElement()
    {
        GetCurrentChildId();
        Transform instance = Instantiate(contentToSpawn).transform;
        instance.SetParent(transform.parent, false);
        instance.SetSiblingIndex(currentIndex + 1);
        extraElements.Add(instance.gameObject);
        Collapse(true);
    }

}
