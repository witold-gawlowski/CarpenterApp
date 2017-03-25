using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour {
  public List<Button> przyciski;
  public List<GameObject> panele;
  public Text tytulSekcji;

  private GameObject obecnyPanel;

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
