using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void ScrambleDeck(List<string> list)
    {
        List<string> ScrambledList = new List<string>();
        
        while (0 < list.Count)
        {
            int SelectedItemNo = Random.Range(0, list.Count-1);
            AddToDeck(ScrambledList, list[SelectedItemNo]);
            list.RemoveAt(SelectedItemNo);
        }
        list=ScrambledList;
    }

    // Update is called once per frame
    void AddToDeck(List<string> list, string item)
    {
        int itemPosition = Random.Range(0, list.Count);
        list.Insert(itemPosition, item);
    }
}
