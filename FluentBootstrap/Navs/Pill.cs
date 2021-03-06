﻿using FluentBootstrap.Badges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentBootstrap.Navs
{
    public interface IPillCreator<THelper> : IComponentCreator<THelper>
        where THelper : BootstrapHelper<THelper>
    {
    }

    public class PillWrapper<THelper> : NavLinkWrapper<THelper>,
        IBadgeCreator<THelper>
        where THelper : BootstrapHelper<THelper>
    {
    }

    internal interface IPill : INavLink
    {
    }

    public class Pill<THelper> : NavLink<THelper, Pill<THelper>, PillWrapper<THelper>>, IPill
        where THelper : BootstrapHelper<THelper>
    {
        internal Pill(IComponentCreator<THelper> creator)
            : base(creator)
        {
        }
    }
}
