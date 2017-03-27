using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour {
  //layout
  public List<Button> przyciski;
  public List<GameObject> panele;
  public Text tytulSekcji;
  private GameObject obecnyPanel;

  /* Input fields calling recalculate in this MB.
   * This triggers recalcualtion of all the output fields referenced in this script.
   * Recalculation takes place here.  
   */

  //output fields
  

  private void UstawObecynPanel(GameObject panel)
  {
    if (obecnyPanel)
    {
      obecnyPanel.SetActive(false);
    }
    obecnyPanel = panel;
    panel.SetActive(true);
    tytulSekcji.text = panel.name;
  }


  public void UstawSekcjeNa(string s)
  {
    foreach(GameObject panel in panele)
    {
      if (panel.name.CompareTo(s) == 0)
      {
        UstawObecynPanel(panel);
        return;
      }
    }
    print("nie można znaleźć przycisku o tej nazwie");
  }

  void Awake()
  {
    foreach(GameObject panel in panele)
    {
      panel.SetActive(false);
    }
  }

  public void Recalculate()
  {
    
  }

  void Start()
  {
    int i = 0;
    foreach(GameObject go in panele)
    {
      Text tekstPrzycisku = przyciski[i].GetComponentInChildren<Text>();
      tekstPrzycisku.text = go.name;
      i++;
    }
    przyciski[0].onClick.Invoke();
  }
}
