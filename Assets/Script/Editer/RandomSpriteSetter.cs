using UnityEngine;
using UnityEngine.UI;

public class RandomSpriteSetter : MonoBehaviour
{
    public Sprite[] sprites; // 사용할 스프라이트 배열

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();

        // 랜덤한 스프라이트 선택
        if (sprites.Length > 0)
        {
            int randomIndex = Random.Range(0, sprites.Length);
            Sprite randomSprite = sprites[randomIndex];

            // 선택한 스프라이트 설정
            image.sprite = randomSprite;
        }
        else
        {
            Debug.LogError("스프라이트 배열이 비어 있습니다.");
        }
    }
}