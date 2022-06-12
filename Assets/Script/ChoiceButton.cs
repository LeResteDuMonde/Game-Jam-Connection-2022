using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ChoiceButton : Button
{
    private TextMeshProUGUI text;
    Color baseColor;
    private MouseControls mC;
    // Start is called before the first frame update
    protected override void Start()
    {
        mC = MouseControls.instance;

        base.Start();
        text = gameObject.transform.GetComponentInChildren<TextMeshProUGUI>();
        baseColor = text.color;
    }

    public override void OnPointerEnter(PointerEventData data) {
        base.OnPointerEnter(data);
        text.color = base.colors.highlightedColor;

        mC.CursorHoverAnimation(true);
    }

    public override void OnPointerExit(PointerEventData data) {
        base.OnPointerEnter(data);
        text.color = baseColor;

        mC.CursorHoverAnimation(false);
    }
}
