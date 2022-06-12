using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class FinishButton : Button, IClicked
{
    private TextMeshPro text;
    Color baseColor;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        text = gameObject.transform.Find("Text").GetComponent<TextMeshPro>();
        baseColor = text.color;
    }

    public override void OnPointerEnter(PointerEventData data) {
        base.OnPointerEnter(data);
        text.color = base.colors.highlightedColor;
    }

    public override void OnPointerExit(PointerEventData data) {
        base.OnPointerEnter(data);
        text.color = baseColor;
    }


	public void onClicked() {
        base.onClick.Invoke();
    }

    public void onCancelClicked() {}
}
