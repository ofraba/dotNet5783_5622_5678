using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using DalApi;
using Dal;

namespace BlImplementation;

internal class BlCart:ICart
{
    public IDal Dal = new DalList();
}
