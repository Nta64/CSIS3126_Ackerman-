using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio.FlyoutMenuItemCS
{
    public class HomeFlyoutMenuItem
    {
        public HomeFlyoutMenuItem()
        {
            TargetType = typeof(MainPage);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}