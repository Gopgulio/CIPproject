using UnityEngine;

public static class Utils
{
    public static float angleFromVector(Vector3 direction)
    {
        direction = direction.normalized;
        float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public static Vector3 vectorFromAngle(float angl)
    {
        float angleRad = angl * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static bool hasComponent<T>(this GameObject obj) where T : Component
    {
        return obj.GetComponent<T>() != null;
    }

    public static Transform getCurrentPlayerTransform()
    {
        return GameObject.Find("Prc").transform;
    }
}
