using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.Commands
{
    public class PathHistoryCache
    {
        public List<string> PathHistory { get; private set; }=new List<string>();
        private int _historyMark=-1;
        public int HistoryMark
        {
            get => _historyMark;
            set
            {
                if (value < -1 || value >= PathHistory.Count)
                    _historyMark = -1;
                _historyMark = value;
            }
        }
    }
}
