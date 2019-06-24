using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HanButtonController : MonoBehaviour
{
  public TMP_Text text;
  public Color defaultColor;
  public Color highlightedColor;
  private Button button;
  private string han;
  private System.Action<string> onClick;

  private void Awake()
  {
    button = GetComponent<Button>();
    button.image.color = defaultColor;
  }

  public void Init(string han, System.Action<string> callback)
  {
    this.han = han;
    text.text = han;
    onClick = callback;
  }

  public void SetActive(bool isActive)
  {
    button.image.color = isActive ? highlightedColor : defaultColor;
  }

  public void OnClickHandler()
  {
    onClick?.Invoke(han);
  }
}
