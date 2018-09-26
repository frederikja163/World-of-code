using System.Collections.Generic;

/// <summary>
/// Manages props and searches for the correct props
/// </summary>
public static class PropManager
{
    /// <summary>
    /// Collections of props, this collections stores the definition of all props
    /// </summary>
    private static List<PropCollection> collections = new List<PropCollection>();
    
    /// <summary>
    /// Add a new collection to the collection list
    /// </summary>
    /// <param name="propCollection">Collection to add to the list of collections</param>
    public static void AddCollection(PropCollection propCollection)
    {
        collections.Add(propCollection);
    }

    /// <summary>
    /// Gets a prop based on the name provided
    /// </summary>
    /// <param name="name">name of prop to search for</param>
    /// <returns>Found prop or null if none found</returns>
    public static PropDefinition GetProp(string name)
    {
        for (int i = 0; i < collections.Count; i++)
        {
            PropDefinition prop = collections[i].FindProp(x => x.name == name);
            if (prop != null)
            {
                return prop;
            }
        }
        return null;
    }
}
