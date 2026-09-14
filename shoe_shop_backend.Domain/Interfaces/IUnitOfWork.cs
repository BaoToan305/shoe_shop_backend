namespace shoe_shop_backend.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICommonRepository<T> Repository<T>() where T : class;

        // ---------- SAVE CHANGES (case đơn giản: 1 entity, ko cần transaction) ----------
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        // ---------- TRANSACTION WRAPPER (case thêm/xóa/sửa nhiều bảng) ----------
        void ExecuteTransaction(Action action);
        Task ExecuteTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default);

        T ExecuteTransaction<T>(Func<T> action);
        Task<T> ExecuteTransactionAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default);
    }
}
