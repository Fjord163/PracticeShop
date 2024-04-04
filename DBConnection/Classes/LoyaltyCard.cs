using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection.Classes
{
    public class LoyaltyCard
    {
        public LoyaltyCard(int numberCard, int bonuses)
        {
            NumberCard = numberCard;
            Bonuses = bonuses;
        }

        public int NumberCard { get; set; }
        public int Bonuses { get; set; }
    }
}
