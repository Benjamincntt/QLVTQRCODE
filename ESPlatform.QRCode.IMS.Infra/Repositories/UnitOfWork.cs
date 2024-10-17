using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _appDbContext;
    private IDbContextTransaction _dbTransaction;
    
    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _appDbContext.Database.BeginTransactionAsync();

    }

    public async Task CommitAsync()
    {
        try
        {
            await _dbTransaction.CommitAsync();
        }
        catch (Exception)
        {
            await _dbTransaction.RollbackAsync();
            throw;
        }
        finally
        {
            await _dbTransaction.DisposeAsync();
            _dbTransaction = null!;
        }
    }

    public async Task RollbackAsync()
    {
        await _dbTransaction.RollbackAsync();
        await _dbTransaction.DisposeAsync();
        _dbTransaction = null!;
    }

}