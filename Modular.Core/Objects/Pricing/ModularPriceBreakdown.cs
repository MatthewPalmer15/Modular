using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core.Objects
{
    public class PriceBreakdown
    {

        #region "  Constructors  "

        public PriceBreakdown() 
        { 
        }

        #endregion

        #region "  Enums  "

        public enum BreakdownType
        {
            Unknown = 0,
            Original = 1,
            Discount = 2,
            Total = 3,
            AmountToPay = 4,
        }

        #endregion

        #region "  Variables  "

        private decimal _StandardRate;

        private decimal _StandardRateVAT;

        private decimal _ZeroRate;

        #endregion

        #region "  Properties  "

        public decimal StandardRate
        {
            get
            {
                return _StandardRate;
            }
            set
            {
                if (_StandardRate != value)
                {
                    _StandardRate = value;
                }
            }
        }

        public decimal StandardRateVAT
        {
            get
            {
                return _StandardRateVAT;
            }
            set
            {
                if (_StandardRateVAT != value)
                {
                    _StandardRateVAT = value;
                }
            }
        }

        public decimal ZeroRate
        {
            get
            {
                return _ZeroRate;
            }
            set
            {
                if (ZeroRate != value)
                {
                    _ZeroRate = value;
                }
            }
        }

        public decimal Total
        {
            get
            {
                return _StandardRate + _ZeroRate;
            }
        }

        public decimal TotalVAT
        {
            get
            {
                return _StandardRateVAT;
            }
        }

        #endregion

        #region "  Static Methods  "

        public static PriceBreakdown Create(decimal StandardRate, decimal StandardRateVAT, decimal ZeroRate)
        {
            PriceBreakdown obj = new PriceBreakdown();
            obj.StandardRate = StandardRate;
            obj.StandardRateVAT = StandardRateVAT;
            obj.ZeroRate = ZeroRate;
            return obj;
        }

        #endregion

        #region "  Instance Methods  "

        public void SetPrice(decimal StandardRate, decimal StandardRateVAT, decimal ZeroRate)
        {
            this.StandardRate = StandardRate;
            this.StandardRateVAT = StandardRateVAT;
            this.ZeroRate = ZeroRate;
        }

        public void ZeroPrice()
        {
            this.StandardRate = 0;
            this.StandardRateVAT = 0;
            this.ZeroRate = 0;
        }

        public void AddPrice(PriceBreakdown PriceBreakdown)
        {
            this.StandardRate += PriceBreakdown.StandardRate;
            this.StandardRateVAT += PriceBreakdown.StandardRateVAT;
            this.ZeroRate += PriceBreakdown.ZeroRate;
        }

        public void SubtractPrice(PriceBreakdown PriceBreakdown)
        {
            this.StandardRate -= PriceBreakdown.StandardRate;
            this.StandardRateVAT -= PriceBreakdown.StandardRateVAT;
            this.ZeroRate -= PriceBreakdown.ZeroRate;
        }

        public void MultiplyPrice(PriceBreakdown PriceBreakdown)
        {
            this.StandardRate *= PriceBreakdown.StandardRate;
            this.StandardRateVAT *= PriceBreakdown.StandardRateVAT;
            this.ZeroRate *= PriceBreakdown.ZeroRate;
        }

        public void MultiplyPrice(decimal Value)
        {
            this.StandardRate *= Value;
            this.StandardRateVAT *= Value;
            this.ZeroRate *= Value;
        }

        public void DividePrice(PriceBreakdown PriceBreakdown)
        {
            this.StandardRate /= PriceBreakdown.StandardRate;
            this.StandardRateVAT /= PriceBreakdown.StandardRateVAT;
            this.ZeroRate /= PriceBreakdown.ZeroRate;
        }

        public void DividePrice(decimal Value)
        {
            this.StandardRate /= Value;
            this.StandardRateVAT /= Value;
            this.ZeroRate /= Value;
        }

        public void CopyPrice(PriceBreakdown PriceBreakdown)
        {
            this.StandardRate = PriceBreakdown.StandardRate;
            this.StandardRateVAT = PriceBreakdown.StandardRateVAT;
            this.ZeroRate = PriceBreakdown.ZeroRate;
        }

        #endregion

    }
}
