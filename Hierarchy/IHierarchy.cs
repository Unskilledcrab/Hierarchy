namespace Hierarchy
{
    public interface IHierarchy<TKey>
    {
        public TKey Id { get; set; }
        public TKey ParentId { get; set; }
    }

}
