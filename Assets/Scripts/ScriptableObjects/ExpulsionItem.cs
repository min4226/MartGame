using UnityEngine;

[CreateAssetMenu(fileName = "ExpulsionItem", menuName = "Scriptable Objects/ExpulsionItem")]
public class ExpulsionItem : ScriptableObject
{
    public Sprite ExpulsionItemSprite; // ������ ��������Ʈ
    public string ExpulsionItemName; // ������ �̸�
    public float ExpulsionDamage; // �������� �ִ� ������
}
