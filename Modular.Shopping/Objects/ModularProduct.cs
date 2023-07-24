using Modular.Core;

namespace Modular.Shopping
{
    public class Product : ModularBase
    {

        public enum ProductType
        {
            Physical,
            Downloadable,
            Subscription
        }

        public enum ProductStatus
        {
            Active,
            Inactive,
            Deleted
        }

        public enum ProductVisibility
        {
            Visible,
            Hidden
        }

        public enum ProductDuration
        {
            None = 0,
            Weekly = 1,
            Monthly = 2,
            Yearly = 3
        }


        public bool InStock { get; set; }
        public  bool IsSubscription { get; set; }

    }
}