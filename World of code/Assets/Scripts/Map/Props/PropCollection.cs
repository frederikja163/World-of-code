using System;


[Serializable]
public class PropCollection
{

    public PropDefinition[] props;

    public PropDefinition FindProp(Predicate<PropDefinition> predicate)
    {
        for (int i = 0; i < props.Length; i++)
        {
            if (predicate.Invoke(props[i]))
            {
                return props[i];
            }
        }
        return null;
    }
}
