namespace UtilityFramework.Infra.Core3.MongoDb.Data.Modelos
{
    public class Feed : ModelBase
    {
        public string ProfileId { get; set; }
        public int Type { get; set; }
        public string ReferenceId { get; set; }

        public override string CollectionName => nameof(Feed);
    }
}