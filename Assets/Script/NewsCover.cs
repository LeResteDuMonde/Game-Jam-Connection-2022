using TMPro;
using UnityEngine;

public class NewsCover : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	[SerializeField] private Sprite[] covers;
	[SerializeField] private int[] thresholds;
	[SerializeField] private TextMeshPro scoreText;
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();

		int score = Scoring.instance.Score();
		SetCover(score);
		SetScore(score);
	}

	private void SetCover(int score)
	{
		for (int i = 0; i < thresholds.Length; i++)
		{
			if (score < thresholds[i])
			{
				spriteRenderer.sprite = covers[i]; break;
			}
			spriteRenderer.sprite = covers[covers.Length - 1];
		}
	}

	private void SetScore(int score)
	{
		scoreText.text = score.ToString();
	}
}
