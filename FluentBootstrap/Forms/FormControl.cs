﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FluentBootstrap.Forms
{
    public interface IFormControl : ITag
    {
    }

    public interface IFormControlCreator<TModel> : IComponentCreator<TModel>
    {
    }

    public abstract class FormControl<TModel, TThis> : Tag<TModel, TThis>, IFormControl, FluentBootstrap.Grids.IHasGridColumnExtensions, IFormValidation, IHasDisabledAttribute
        where TThis : FormControl<TModel, TThis>
    {
        private FormGroup<TModel> _formGroup = null;
        private Label<TModel> _label = null;
        internal string Help { get; set; }

        protected FormControl(BootstrapHelper<TModel> helper, string tagName, params string[] cssClasses) 
            : base(helper, tagName, cssClasses)
        {
        }

        internal Label<TModel> Label
        {
            set
            {
                _label = value;

                // Need to remove this from the pending components since it's similar to a child and will be output from this form control
                PendingComponents.Remove(HtmlHelper, value);
            }
        }

        protected override void PreStart(TextWriter writer)
        {
            base.PreStart(writer);

            // Make sure we're in a form group
            FormGroup<TModel> formGroup = GetComponent<FormGroup<TModel>>();
            if (formGroup == null)
            {
                _formGroup = new FormGroup<TModel>(Helper);
                formGroup = _formGroup;
            }

            // Move any validation classes to the form group, but only if it's implicit for this control and doesn't already have any
            if (CssClasses.Any(x => x.StartsWith("has-")) && _formGroup != null && !formGroup.CssClasses.Any(x => x.StartsWith("has-")))
            {
                foreach (string formValidation in CssClasses.Where(x => x.StartsWith("has-")))
                {
                    formGroup.CssClasses.Add(formValidation);
                }
                CssClasses.RemoveWhere(x => x.StartsWith("has-"));
            }

            // Move any grid column classes to the form group, but only if it's implicit for this control and doesn't already have any
            if (CssClasses.Any(x => x.StartsWith("col-")) && _formGroup != null && !formGroup.CssClasses.Any(x => x.StartsWith("col-")))
            {
                foreach (string col in CssClasses.Where(x => x.StartsWith("col-")))
                {
                    formGroup.CssClasses.Add(col);
                }
                CssClasses.RemoveWhere(x => x.StartsWith("col-"));
            }

            // Add the label to the form group or write it
            if (_label != null)
            {
                // Set the label's for attribute to the input name
                string name = null;
                if (TagBuilder.Attributes.TryGetValue("name", out name) && !string.IsNullOrWhiteSpace(name))
                {
                    _label.For(name);
                }

                // Add or write the label
                if (_formGroup != null)
                {
                    formGroup.Label = _label;
                }
                else
                {
                    _label.StartAndFinish(writer);
                }
            }

            // Start the new form group if we created one
            if (_formGroup != null)
            {
                _formGroup.Start(writer, true);
            }
        }

        protected override void OnStart(TextWriter writer)
        {
            // Add the validation data
            string name = null;
            if (TagBuilder.Attributes.TryGetValue("name", out name) && !string.IsNullOrWhiteSpace(name))
            {
                // Set the id
                TagBuilder.GenerateId(name);

                // Set the validation class
                ModelState modelState;
                if (HtmlHelper.ViewData.ModelState.TryGetValue(name, out modelState) && modelState.Errors.Count > 0)
                {
                    CssClasses.Add(System.Web.Mvc.HtmlHelper.ValidationInputCssClassName);
                }

                // Add other validation attributes
                TagBuilder.MergeAttributes<string, object>(HtmlHelper.GetUnobtrusiveValidationAttributes(name, null));
            }

            base.OnStart(writer);
        }

        protected override void OnFinish(TextWriter writer)
        {
            // Add the help text
            if (!string.IsNullOrEmpty(Help))
            {
                new Tag<TModel>(Helper, "p", "help-block")
                    .AddChild(new Content<TModel>(Helper, Help))
                    .StartAndFinish(writer);
            }

            base.OnFinish(writer);
            Pop(_formGroup, writer);
        }
    }
}