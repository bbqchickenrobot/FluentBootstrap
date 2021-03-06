﻿using FluentBootstrap.Html;
using FluentBootstrap.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentBootstrap
{
    public static class MiscExtensions
    {
        // Jumbotron

        public static Jumbotron<THelper> Jumbotron<THelper>(this IJumbotronCreator<THelper> creator)
            where THelper : BootstrapHelper<THelper>
        {
            return new Jumbotron<THelper>(creator);
        }

        // PageHeader

        public static PageHeader<THelper> PageHeader<THelper>(this IPageHeaderCreator<THelper> creator, string text = null)
            where THelper : BootstrapHelper<THelper>
        {
            return new PageHeader<THelper>(creator).SetText(text);
        }

        // Elements

        public static Element<THelper> Clearfix<THelper>(this ITagCreator<THelper> creator)
            where THelper : BootstrapHelper<THelper>
        {
            return new Element<THelper>(creator, "div").AddCss(Css.Clearfix);
        }

        public static Element<THelper> CenterBlock<THelper>(this ITagCreator<THelper> creator)
            where THelper : BootstrapHelper<THelper>
        {
            return new Element<THelper>(creator, "div").AddCss(Css.CenterBlock);
        }

        public static Element<THelper> PullLeft<THelper>(this ITagCreator<THelper> creator)
            where THelper : BootstrapHelper<THelper>
        {
            return new Element<THelper>(creator, "div").AddCss(Css.PullLeft);
        }

        public static Element<THelper> PullRight<THelper>(this ITagCreator<THelper> creator)
            where THelper : BootstrapHelper<THelper>
        {
            return new Element<THelper>(creator, "div").AddCss(Css.PullRight);
        }

        public static Element<THelper> Caret<THelper>(this ITagCreator<THelper> creator)
            where THelper : BootstrapHelper<THelper>
        {
            return new Element<THelper>(creator, "span").AddCss(Css.Caret);
        }

        public static Element<THelper> Close<THelper>(this ITagCreator<THelper> creator)
            where THelper : BootstrapHelper<THelper>
        {
            return new Element<THelper>(creator, "button").AddCss(Css.Close)
                .AddAttribute("type", "button")
                .AddContent("<span aria-hidden=\"true\">&times;</span><span class=\"sr-only\">Close</span>");
        }
    }
}
