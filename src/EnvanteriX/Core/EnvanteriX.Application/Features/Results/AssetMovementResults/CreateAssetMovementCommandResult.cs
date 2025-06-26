namespace EnvanteriX.Application.Features.Results.AssetMovementResults
{
   public  class CreateAssetMovementCommandResult
    {
        public int Id { get; set; }                  // Oluşturulan hareketin ID'si
        public int AssetId { get; set; }             // Hangi varlık taşındı
        public int? FromUserId { get; set; }         // Gönderen kullanıcı
        public int? ToUserId { get; set; }           // Alan kullanıcı
        public int? FromLocationId { get; set; }     // Çıkış lokasyonu
        public int? ToLocationId { get; set; }       // Varış lokasyonu
        public string Note { get; set; }             // Not
        public DateTime CreatedDate { get; set; }    // Hareket tarihi
    }
}
