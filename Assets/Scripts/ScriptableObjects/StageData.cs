using UnityEngine;




[CreateAssetMenu(fileName = "Stage", menuName = "Scriptable Objects/StageData")]

public class StageData : ScriptableObject 
{
    
    // �ð� ����
    public float timeLimit;

    // ���������� Ŭ���� �ϱ� ���� �ʿ��� ���� ���
    public int requiredCoin;
    public int requiredFame;

    // �������� �̸�
    public StageType stageName;

    // �������� ������ ������ �մ� �󵵼�
    public int normalCustomerCount; // �Ϲ� �մ�
    public int troublemakerCustomerCount; // ���� �մ�
    public int thiefCustomerCount; // ���� �մ�
    public int specialCustomerCount; // Ư�� �մ�

    public int normalCustomerItemCount; // �Ϲ� �մ��� �����Ǿ��� �� ���� �������� ����

    public ItemCreatePattern[] itemCreatePatterns; // �������� ������ ����
    public PatternRules[] patternRules; // ���� ��Ģ

    //public Reward reward; // ������
}
