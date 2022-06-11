namespace pet;

public class Pet
{
    private long Id { get; set; }
    private long OwnerId { get; set; }
    private long TypeId { get; set; }
    private string Breed { get; set; }
    private string Name { get; set; }
    private string Bio { get; set; }
    private string PictureUrl { get; set; }
    private int ListOrder { get; set; }

    public Pet(long id, long ownerId, long typeId, string breed, string name, string bio, string pictureUrl, int listOrder)
    {
        Id = id;
        OwnerId = ownerId;
        TypeId = typeId;
        Breed = breed;
        Name = name;
        Bio = bio;
        PictureUrl = pictureUrl;
        ListOrder = listOrder;
    }
}