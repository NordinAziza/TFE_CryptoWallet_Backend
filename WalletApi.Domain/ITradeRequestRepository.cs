using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.Framework;

namespace WalletApi.Domain
{
    public interface ITradeRequestRepository : IRepository
    {
        ICollection<TradeRequest> GetAll();
        TradeRequest AddTrade(TradeRequest tradeRequest);
        public TradeRequest UpdateStatus(int tradeRequestId, string newStatus);
    }
}
