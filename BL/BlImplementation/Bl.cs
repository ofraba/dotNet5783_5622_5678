﻿using BLApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

sealed public class Bl : IBl
{
    public IOrder Order => new BlOrder();
    public IProduct Product => new BlProduct();
    public ICart Cart => new BlCart();
}
