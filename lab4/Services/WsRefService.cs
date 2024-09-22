using lab3b_vd.Data;
using lab3b_vd.Models;
using Microsoft.EntityFrameworkCore;

namespace lab3b_vd.Services;

public class WsRefService
{
    private readonly AppDbContext _dbContext;

    public WsRefService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<WsRef>> GetWsRefsAsync()
    {
        return await _dbContext.WsRefs.ToListAsync();
    }

    public async Task<WsRef?> GetWsRefByIdAsync(int id)
    {
        return await _dbContext.WsRefs.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<WsRef> AddWsRefAsync(WsRef wsRef)
    {
        await _dbContext.WsRefs.AddAsync(wsRef);
        await _dbContext.SaveChangesAsync();
        return wsRef;
    }

    public async Task<WsRef> DeleteWsRefAsync(int id)
    {
        var wsRef = await _dbContext.WsRefs.FindAsync(id);
        if (wsRef is null)
            return null;

        await Task.Run(() => _dbContext.WsRefs.Remove(wsRef));
        await _dbContext.SaveChangesAsync();
        return wsRef;
    }

    public async Task<WsRef> UpdateWsRefAsync(WsRef wsRef)
    {
        _dbContext.WsRefs.Update(wsRef);
        await _dbContext.SaveChangesAsync();
        return wsRef;
    }

    public async Task<WsRef> PlusAsync(int id)
    {
        var wsRef = await _dbContext.WsRefs.FindAsync(id);
        if (wsRef is null)
            return null;

        wsRef.Plus++;
        await _dbContext.SaveChangesAsync();
        return wsRef;
    }

    public async Task<WsRef> MinusAsync(int id)
    {
        var wsRef = await _dbContext.WsRefs.FindAsync(id);
        if (wsRef is null)
            return null;

        wsRef.Minus++;
        await _dbContext.SaveChangesAsync();
        return wsRef;
    }
}
