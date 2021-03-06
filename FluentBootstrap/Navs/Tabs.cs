﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentBootstrap.Navs
{
    public interface ITabsCreator<THelper> : IComponentCreator<THelper>
        where THelper : BootstrapHelper<THelper>
    {
    }

    public class TabsWrapper<THelper> : NavWrapper<THelper>,
        ITabCreator<THelper>
        where THelper : BootstrapHelper<THelper>
    {
    }


    internal interface ITabs : INav
    {
    }

    public class Tabs<THelper> : Nav<THelper, Tabs<THelper>, TabsWrapper<THelper>>, ITabs
        where THelper : BootstrapHelper<THelper>
    {
        internal Tabs(IComponentCreator<THelper> creator)
            : base(creator, Css.Nav, Css.NavTabs)
        {
            this.MergeAttribute("role", "tablist");
        }
    }
}
