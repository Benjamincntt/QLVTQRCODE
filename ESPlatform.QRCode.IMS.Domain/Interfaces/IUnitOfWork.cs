using Microsoft.EntityFrameworkCore.Storage;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}