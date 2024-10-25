using System.ComponentModel.DataAnnotations;
public record BidDataDto (int Id, int HouseId, 
    [property: Required]string Bidder, int Amount);