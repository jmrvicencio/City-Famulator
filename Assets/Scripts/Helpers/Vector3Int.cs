using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Vector3Int : IEquatable<Vector3Int>
{
    public int x, y, z;

    public Vector3Int(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3Int(Vector3 position)
    {
        x = (int) position.x;
        y = (int) position.y;
        z = (int) position.z;
    }

    #region IEquatable implementation

    public bool Equals(Vector3Int other)
    {
        return other.x == this.x && other.y == this.y && other.z == this.z;
    }

    #endregion

    public override bool Equals(object obj)
    {
        if(obj == null || !(obj is Vector3Int))
        {
            return false;
        }

        return Equals((Vector3Int) obj);
    }

    public override int GetHashCode()
    {
        return x ^ (y << 10) ^ (z << 10);
    }

    public override string ToString()
    {
        return string.Format("(" + x + ", ", + y + ", " + z +")");
    }

    public static bool operator ==(Vector3Int a, Vector3Int b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Vector3Int a, Vector3Int b)
    {
        return !(a == b);
    }
}
