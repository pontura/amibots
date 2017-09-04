using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICategoryButton : MonoBehaviour {
    
    public AmiScript.categories category;
    public UICategorySelector categorySelector;

    public void Init()
    {

    }
    public void Clicked()
    {
        print(category);
        categorySelector.Clicked(category);
    }
}