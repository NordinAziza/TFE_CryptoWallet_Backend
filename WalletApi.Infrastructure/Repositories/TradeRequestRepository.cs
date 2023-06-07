using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.Framework;
using WalletApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace WalletApi.Infrastructure.Repositories
{
    public class TradeRequestRepository : ITradeRequestRepository
    {
        #region
        private readonly TradeRequestContext _context;
        #endregion
        #region Constructor
        public TradeRequestRepository(TradeRequestContext context)
        {
            _context = context;
        }
        #endregion
        #region Methods
        public ICollection<TradeRequest> GetAll()
        {
            return _context.TradeRequest.Include(tr => tr.User).ToList();
        }

        public TradeRequest AddTrade(TradeRequest tradeRequest)
        {
            var addedTradeRequest = _context.TradeRequest.Add(tradeRequest).Entity;
            _context.SaveChanges();
            return addedTradeRequest;
        }
        public TradeRequest UpdateStatus(int tradeRequestId, string newStatus)
        {
            var tradeRequest = _context.TradeRequest.FirstOrDefault(tr => tr.Id == tradeRequestId);
            if (tradeRequest != null)
            {
                tradeRequest.Status = newStatus;
                _context.SaveChanges();
            }
            return tradeRequest;
        }
        #endregion
        #region Properties
        public IUnitOfWork UnitOfWork => _context;
        #endregion
    }

}
