using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi.Domain
{
    public class TradeRequest
    {
        public int Id { get; set; }
        public Users User { get; set; }
        public string Date { get; set; }
        public string TokenToTrade { get; set; }
        public double   AmountToTrade { get; set; }
        public string TokenToReceive { get; set; }
        public double AmountToReceive { get; set; }
        public string Status { get; set; }  
    }
}
