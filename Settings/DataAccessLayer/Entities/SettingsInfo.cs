namespace DataAccessLayer.Entities
{
    public class SettingsInfo:MongoBaseEntity
    {
        public string ThemeColor { get; set; }
        public string BackgroundPhotoSrc { get; set; }
    }
}
