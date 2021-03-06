﻿using FluentBootstrap.Buttons;
using FluentBootstrap.Forms;
using FluentBootstrap.Html;
using FluentBootstrap.Links;
using FluentBootstrap.Navbars;
using FluentBootstrap.Navs;
using FluentBootstrap.Typography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentBootstrap.Dropdowns
{
    public interface IDropdownCreator<THelper> : IComponentCreator<THelper>
        where THelper : BootstrapHelper<THelper>
    {
    }

    public class DropdownWrapper<THelper> : TagWrapper<THelper>, 
        IDropdownDividerCreator<THelper>, 
        IDropdownHeaderCreator<THelper>,
        IDropdownLinkCreator<THelper>
        where THelper : BootstrapHelper<THelper>
    {
    }

    internal interface IDropdown : IButton
    {
    }

    public class Dropdown<THelper> : Tag<THelper, Dropdown<THelper>, DropdownWrapper<THelper>>, IDropdown,
        IHasButtonExtensions, IHasButtonStateExtensions, IHasTextContent
        where THelper : BootstrapHelper<THelper>
    {
        private bool _dropdownButton = false;
        private bool _caret = true;
        private bool _menuRight;
        private bool _menuLeft;
        private Component _toggle;
        private Typography.List<THelper> _list;

        internal bool Caret
        {
            set { _caret = value; }
        }

        internal bool MenuRight
        {
            set { _menuRight = value; }
        }

        internal bool MenuLeft
        {
            set { _menuLeft = value; }
        }

        internal Dropdown(IComponentCreator<THelper> creator)
            : base(creator, "div", Css.Dropdown, Css.BtnDefault)
        {
        }

        protected override void OnStart(TextWriter writer)
        {
            // Check if we're in a navbar, and if so, make sure we're in a navbar nav
            if (GetComponent<INavbar>() != null && GetComponent<INavbarNav>() == null)
            {
                new NavbarNav<THelper>(Helper).Start(writer);
            }

            // Check if we're in a nav
            if (GetComponent<INav>(true) != null || GetComponent<INavbarNav>(true) != null)
            {
                TagName = "li";
                Link<THelper> link = Helper.Link(null);
                link.AddCss(Css.DropdownToggle);
                link.AddAttribute("data-toggle", "dropdown");
                _toggle = link;
            }
            else
            {
                // Create a button and copy over any button classes and text
                Button<THelper> button = Helper.Button();
                button.RemoveCss(Css.BtnDefault);
                button.AddCss(Css.DropdownToggle);
                button.AddAttribute("data-toggle", "dropdown");
                foreach (string buttonClass in CssClasses.Where(x => x.StartsWith("btn")))
                {
                    button.CssClasses.Add(buttonClass);
                }
                _toggle = button;
            }
            CssClasses.RemoveWhere(x => x.StartsWith("btn"));

            // Add the text and caret
            if (!string.IsNullOrWhiteSpace(TextContent))
            {
                _toggle.AddChild(new Content<THelper>(Helper, TextContent + " "));
            }
            else
            {
                Element<THelper> element = new Element<THelper>(Helper, "span").AddCss(Css.SrOnly);
                element.AddChild(new Content<THelper>(Helper, "Toggle Dropdown"));
                _toggle.AddChild(element);
            }
            TextContent = null;
            if (_caret)
            {
                _toggle.AddChild(Helper.Caret());
            }

            // Check if we're in a IDropdownButton or IInputGroupButton, then
            // Check if we're in a button group, and if so change the outer CSS class
            // Do this after copying over the btn classes so this doesn't get copied
            if (GetComponent<IDropdownButton>(true) != null || GetComponent<IInputGroupButton>(true) != null)
            {
                _dropdownButton = true;
            }
            else if (GetComponent<IButtonGroup>(true) != null)
            {
                ToggleCss(Css.BtnGroup, true, Css.Dropdown);
            }

            // Create the list
            _list = Helper.List(ListType.Unordered);
            _list.AddCss(Css.DropdownMenu);
            _list.MergeAttribute("role", "menu");
            if (_menuRight)
            {
                _list.AddCss(Css.DropdownMenuRight);
            }
            if (_menuLeft)
            {
                _list.AddCss(Css.DropdownMenuLeft);
            }

            // Start this component
            base.OnStart(_dropdownButton ? new SuppressOutputWriter() : writer);

            // Output the button
            _toggle.StartAndFinish(writer);

            // Output the start of the list
            _list.Start(writer);
        }

        protected override void OnFinish(TextWriter writer)
        {
            _list.Finish(writer);
            base.OnFinish(_dropdownButton ? new SuppressOutputWriter() : writer);
        }
    }
}
