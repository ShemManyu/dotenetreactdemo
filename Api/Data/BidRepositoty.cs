using Microsoft.EntityFrameworkCore;

public interface IBidRepository
{
    Task<List<BidDataDto>> Get(int houseId);
    Task<BidDataDto> Add(BidDataDto dto);
}

public class BidRepository : IBidRepository
{
    private readonly HouseDbContext context;
    public BidRepository(HouseDbContext context)
    {
        this.context = context;
    }
    private static void DtoToEntity(BidDataDto dto, BidEntity e)
    {
        e.HouseId = dto.HouseId;
        e.Bidder = dto.Bidder;
        e.Amount = dto.Amount;
    }
    private static BidDataDto EntityToDetailDto(BidEntity e)
    {
        return new BidDataDto(e.Id, e.HouseId, e.Bidder, e.Amount);
    }
    public async Task<List<BidDataDto>> Get(int houseId)
    {
        return await context.Bids.Where(b => b.HouseId == houseId)
            .Select(b => new BidDataDto(b.Id, b.HouseId, b.Bidder, b.Amount))
            .ToListAsync();
    }

    public async Task<BidDataDto> Add(BidDataDto dto)
    {
        var entity = new BidEntity();
        DtoToEntity(dto, entity);
        context.Bids.Add(entity);
        await context.SaveChangesAsync();
        return EntityToDetailDto(entity);
    }
}