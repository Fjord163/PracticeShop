using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Classes
{
    public class CreateLoyaltyCard
    {
        public CreateLoyaltyCard() { }
        public CreateLoyaltyCard(int numberCard)
        {
            NumberCard = numberCard;
        }

        public int NumberCard { get; set; }
    }
}
