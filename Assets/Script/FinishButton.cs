using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class FinishButton : Button, IClicked, IHovered
{
	private TextMeshPro text;
	Color baseColor;
	private Sprite baseSprite;
	[SerializeField] private Sprite hoverSprite;
	[SerializeField] private SpriteRenderer sprite;
	private MouseControls mC;

	protected override void Start()
	{
		mC = MouseControls.instance;

		base.Start();
		gameObject.transform.GetChild(0).TryGetComponent(out TextMeshPro textMesh);
		gameObject.transform.GetChild(0).TryGetComponent(out SpriteRenderer spriteRenderer);
		text = textMesh;
		sprite = spriteRenderer;
		if (sprite != null) { baseSprite = sprite.sprite; Debug.Log("sprite button"); }
		if (text != null) { baseColor = text.color; }
	}

	public void onClicked()
	{
		base.onClick.Invoke();
	}

	public void onCancelClicked() { }

	public void onHover()
	{
		Debug.Log("hover");
		if (text != null) { text.color = base.colors.highlightedColor; }
		if (sprite != null) { sprite.sprite = hoverSprite; }

		mC.CursorHoverAnimation(true);
	}

	public void onUnhover()
	{
		if (text != null) { text.color = baseColor; }
		if (sprite != null) { sprite.sprite = baseSprite; }

		mC.CursorHoverAnimation(false);
	}
}

[CustomEditor(typeof(FinishButton))]
public class FinishButtonEditor : Editor
{
	public override void OnInspectorGUI()
	{
		FinishButton finishButton = (FinishButton)target;
		DrawDefaultInspector();
	}
}
