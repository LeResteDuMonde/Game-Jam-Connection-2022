using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ChoiceButton : Button
{
    private TextMeshProUGUI text;
    Color baseColor;
    private Sprite baseSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Image sprite;

    private MouseControls mC;

    protected override void Start()
    {
        mC = MouseControls.instance;

        base.Start();
        gameObject.transform.GetChild(0).TryGetComponent(out TextMeshProUGUI textMesh);
        gameObject.transform.GetChild(0).TryGetComponent(out Image spriteRenderer);
        text = textMesh;
        sprite = spriteRenderer;
        if (sprite != null) { baseSprite = sprite.sprite; }
        if (text != null) { baseColor = text.color; }
    }

    public override void OnPointerEnter(PointerEventData data) {
        base.OnPointerEnter(data);
        if (text != null) { text.color = base.colors.highlightedColor; }
        if (sprite != null) { sprite.sprite = hoverSprite; }

        mC.CursorHoverAnimation(true);
    }

    public override void OnPointerExit(PointerEventData data) {
        base.OnPointerEnter(data);
        if (text != null){ text.color = baseColor; }
        if (sprite != null) { sprite.sprite = baseSprite; }

        mC.CursorHoverAnimation(false);
    }
}