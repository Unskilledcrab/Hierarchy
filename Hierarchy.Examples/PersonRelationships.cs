public class PersonRelationships
{
    public int Id { get; set; }
    public int[] KnownRelationshipIds { get; set; } = new int[0];
    public string Name { get; set; } = "";

    public override string ToString()
    {
        return $"Person: {Id} '{Name}'";
    }
}
