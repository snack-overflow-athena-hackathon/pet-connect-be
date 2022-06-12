namespace pet.Models.DBEntities
{
    public class PetDbEntity
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public long TypeId { get; set; }
        public string? Breed { get; set; }
        public string PetName { get; set; }
        public string? Gender { get; set; }
        public string? Bio { get; set; }
        public string? PictureUrl { get; set; }
        public int ListOrder { get; set; }
        public bool Deactivated { get; set; }

        public string Animal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
