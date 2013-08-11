using Go.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go.Models
{
    class Chain : HashSet<StoneSpace>
    {
        public int GetLiberties()
        {
            ISet<StoneSpace> liberties = new HashSet<StoneSpace>();
            foreach (StoneSpace stone in this)
            {
                foreach (StoneSpace neighbor in stone.Neighbors())
                {
                    if (neighbor == StoneState.None)
                    {
                        liberties.Add(neighbor);
                    }
                }
            }
            return liberties.Count;

        }
    }
}
