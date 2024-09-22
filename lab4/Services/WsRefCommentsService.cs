using lab3b_vd.Data;
using lab3b_vd.Models;
using Microsoft.EntityFrameworkCore;

namespace lab3b_vd.Services;

public class WsRefCommentsService
{
    private readonly AppDbContext _dbContext;

    public WsRefCommentsService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WsRegComment> AddCommentAsync(WsRegComment comment)
    {
        var wsRef = await _dbContext.WsRefs.FindAsync(comment.WsRefId);
        if (wsRef is null)
            return null;

        await _dbContext.WsRefComments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();

        return comment;
    }

    public async Task<IEnumerable<WsRegComment>> GetCommentsOfAsync(int wsRefId)
    {
        return await _dbContext.WsRefComments.Where(c => c.WsRefId == wsRefId).ToListAsync();
    }

    public async Task<WsRegComment?> GetCommentByIdAsync(int commentId)
    {
        return await _dbContext.WsRefComments.FirstOrDefaultAsync(c => c.Id == commentId);
    }

    public async Task<WsRegComment> UpdateCommentAsync(WsRegComment wsRegComment)
    {
        _dbContext.WsRefComments.Update(wsRegComment);
        await _dbContext.SaveChangesAsync();
        return wsRegComment;
    }

    public async Task<WsRegComment?> DeleteCommentAsync(int commentId)
    {
        var wsRegComment = await this.GetCommentByIdAsync(commentId);

        if (wsRegComment is null)
            return null;

        await Task.Run(() => _dbContext.WsRefComments.Remove(wsRegComment));
        await _dbContext.SaveChangesAsync();

        return wsRegComment;
    }
}
