using OptimizedRouteFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedRouteFinder.BasicComponents {
  class TournamentBranch {
    public List<I_OneRow> OneRowList { get; private set; }

    public TournamentBranch() {
      this.OneRowList = new List<I_OneRow>();
    }

    void Register(List<I_OneRow> onerow_list) {
      this.OneRowList = onerow_list;
    }

    double Run() {

    }
  }
}
