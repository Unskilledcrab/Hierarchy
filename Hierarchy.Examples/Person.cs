public class Person
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; } = "";

    public override string ToString()
    {
        return $"Person: {Id} '{Name}'";
    }
}
