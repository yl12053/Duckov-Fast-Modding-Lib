using Duckov.Utilities;

namespace FastModdingLib
{
    public static class ShopUtils
    {
        public static void AddGoods(ShopGoodsData data) {
            GameplayDataSettings.StockshopDatabase.GetMerchantProfile(data.merchantProfileID).entries.Add(new StockShopDatabase.ItemEntry
            {
                typeID = data.typeID,
                possibility = data.possibility,
                forceUnlock = data.forceUnlock,
                maxStock = data.maxStock,
                priceFactor = data.priceFactor,
                lockInDemo = false
            });
        }
    }
}
