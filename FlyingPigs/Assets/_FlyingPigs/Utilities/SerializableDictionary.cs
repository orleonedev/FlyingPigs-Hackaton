using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    public int Count => keys.Count;

    public void Add(TKey key, TValue value)
    {
        if (!keys.Contains(key))
        {
            keys.Add(key);
            values.Add(value);
        }
        else
        {
            // If the key already exists, replace the value
            int index = keys.IndexOf(key);
            values[index] = value;
        }
    }

    public bool Remove(TKey key)
    {
        int index = keys.IndexOf(key);
        if (index >= 0)
        {
            keys.RemoveAt(index);
            values.RemoveAt(index);
            return true;
        }
        return false;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = keys.IndexOf(key);
        if (index >= 0)
        {
            value = values[index];
            return true;
        }

        value = default;
        return false;
    }

    public bool ContainsKey(TKey key)
    {
        return keys.Contains(key);
    }

    public TValue this[TKey key]
    {
        get
        {
            if (keys.Contains(key))
            {
                int index = keys.IndexOf(key);
                return values[index];
            }
            throw new KeyNotFoundException();
        }
        set
        {
            Add(key, value);
        }
    }

    public List<TKey> Keys => keys;
    public List<TValue> Values => values;

    // Implementation of IEnumerable
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            yield return new KeyValuePair<TKey, TValue>(keys[i], values[i]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
