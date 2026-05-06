using System.Collections;
using System.Threading.Tasks;
using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
public static class Extensions
{
    public static float normalized(this float target)
    {
        if (target > 0) return 1.0f;
        else if(target < 0) return -1.0f;
        else return 0.0f;
    }

    public static T TryAddComponent<T>(this GameObject target) where T : Component
    {
        T result = null;

        if (target == null) return result;
        result = target.GetComponent<T>();

        // if (result is null) result = target.AddComponent<T>();
        result ??= target.AddComponent<T>();

        return result;
    }
    public static T GetExtreme<T>(this IEnumerable targetList, float defaultScore,
        System.Func<T, float> Evalueator, System.Func<float, float, bool> Comparison)
    {
        T result = default;
        float firstScore = float.MinValue; // maxValue : 가장 큰 값

        foreach (T currentTarget in targetList)
        {
            float currentScore = Evalueator(currentTarget);

            if (currentScore > firstScore)
            {
                result = currentTarget;
                firstScore = currentScore;
            }
        }
        return result;

    }
    public static T GetMaximum<T>(this IEnumerable targetList, System.Func<T, float> Evalueator)
    => GetExtreme(targetList, float.MinValue, Evalueator, (a,b) => a > b);

    public static T GetMinimum<T>(this IEnumerable targetList, System.Func<T, float> Evalueator)
    => GetExtreme(targetList, float.MaxValue, Evalueator, (a, b) => a < b);
    

    public static T TryAddComponent<T>(this Component target) where T : Component
    {
        if (target == null) return null;
        else return target.gameObject.TryAddComponent<T>();
    }

    public static IEnumerator WaitForTask(this Task targetTask)
    {
        // waituntil : false�� ��쿡�� �۵� - true�� �� ������ ��ٸ�
        // waitwhile : true�� ��쿡�� �۵�
        yield return new WaitUntil(() => targetTask.IsCompleted);
        targetTask.Dispose();
    }

    public static float GetPenetratedDistance(float aHalf, float bHalf, float aPos, float bPos)
    {
        
        float absAHalf = Mathf.Abs(aHalf);
        float absBHalf = Mathf.Abs(bHalf);
        float xMinSpace = absAHalf + absBHalf;

        float xDistance = aPos - bPos;
        float xPenetration = xMinSpace - Mathf.Abs(xDistance);

        xPenetration *= Mathf.Sign(xDistance);
        return xPenetration;
        
    }

    public static Vector2 AABB(this Rect A, Rect B)
    {
        Vector2 result = Vector2.zero;
        Vector2 aMin = A.min;
        Vector2 aMax = A.max;
        Vector2 aHalf = A.size * .5f;
        Vector2 bMin = B.min;
        Vector2 bMax = B.max;
        Vector2 bHalf = B.size * .5f;

        if (aMax.x > bMin.x && bMax.x > aMin.x)
        {
            result.x = GetPenetratedDistance(aHalf.x, bHalf.x,A.position.x, B.position.x);
        }
        if (aMax.y > bMin.y && bMax.y > aMin.y)
        {
            result.y = GetPenetratedDistance(aHalf.y, bHalf.y, A.position.y, B.position.y);
        }
        return result;
    }

    public static float GetOutboundDistance(float inMin, float outMin, float inMax, float outMax)
    {
        float result = 0.0f;

        bool leftOut = inMin < outMin;
        bool rightOut = inMax > outMax;

        if (leftOut ^ rightOut)
        {
            if(leftOut) result = outMin - inMin;
            if(rightOut) result = outMax - inMax;
        } 
        return result;
        
    }
                                                       // �׵θ�
    public static Vector2 InversedAABB(this Rect target, Rect bound)
    {
        Vector2 result;
        result.x = GetOutboundDistance(target.xMin, bound.xMin, target.xMax, bound.xMax);
        result.y = GetOutboundDistance(target.yMin, bound.yMin, target.yMax, bound.yMax);

        return result;
    }
}
