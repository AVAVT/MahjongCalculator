using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FuButtonController : MonoBehaviour
{
  public TMP_Text text;
  public Color defaultColor;
  public Color highlightedColor;
  private Button button;
  private int fu;
  private System.Action<int> onClick;
  private void Awake()
  {
    button = GetComponent<Button>();
    button.image.color = defaultColor;
  }

  public void Init(int fu, System.Action<int> callback)
  {
    this.fu = fu;
    text.text = $"{fu} Fu";
    onClick = callback;
  }

  public void SetActive(bool isActive)
  {
    button.image.color = isActive ? highlightedColor : defaultColor;
  }

  public void OnClickHandler()
  {
    onClick?.Invoke(fu);
  }
}
