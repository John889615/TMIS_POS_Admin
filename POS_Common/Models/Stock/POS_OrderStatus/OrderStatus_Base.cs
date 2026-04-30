using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Stock.POS_OrderStatus
{
  public abstract class OrderStatus_Base
  {
       #region Properties
       
      public int? OrderStatusID { get; set; }

      public string OrderStatus { get; set; }
       #endregion
  }
}
