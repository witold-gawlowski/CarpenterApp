using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour {
  public List<Button> przyciski;
  public List<GameObject> panele;
  public Text tytulSekcji; 
  void Start()
  {
    int i = 0;
    foreach(GameObject go in panele)
    {
      Text tekstPrzycisku = przyciski[i].GetComponentInChildren<Text>();
      tekstPrzycisku.text = go.name;
      i++;
    }
  }
}
