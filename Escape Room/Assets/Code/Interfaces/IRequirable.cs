using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IRequirable
{
    Item RequiredItem { get; }

    void Give (Item item);
}
