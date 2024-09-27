namespace ShopApi2024.Interfaces
{
    public interface ISoftDelete
    {
        public bool IsDelete { get; set; }
        public DateTime? DeleteTime { get; set; }
        public void Undo()
        {
            IsDelete = false;
            DeleteTime = null;
        }
    }
}
