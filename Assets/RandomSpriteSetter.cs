using UnityEngine;
using UnityEngine.UI;

public class RandomSpriteSetter : MonoBehaviour
{
    public Sprite[] sprites; // ����� ��������Ʈ �迭

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();

        // ������ ��������Ʈ ����
        if (sprites.Length > 0)
        {
            int randomIndex = Random.Range(0, sprites.Length);
            Sprite randomSprite = sprites[randomIndex];

            // ������ ��������Ʈ ����
            image.sprite = randomSprite;
        }
        else
        {
            Debug.LogError("��������Ʈ �迭�� ��� �ֽ��ϴ�.");
        }
    }
}