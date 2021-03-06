﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentBootstrap.Tests
{
    [TestFixture]
    public class MiscFixture
    {
        [Test]
        public void FullWidthJumbotronProducesCorrectHtml()
        {
            TestHelper.AssertHtml<FluentBootstrap.Tests.Web.Views.Tests.Misc>("test-fullwidth-jumbotron",
@"<div class=""jumbotron"">
  <div class=""container"">
   <h1>Heading</h1>
   <p>Some text.</p>
  </div>
 </div>");
        }

        [Test]
        public void JumbotronProducesCorrectHtml()
        {
            TestHelper.AssertHtml<FluentBootstrap.Tests.Web.Views.Tests.Misc>("test-jumbotron",
@"<div class=""jumbotron"">
   <h1>Heading</h1>
   <p>Some text.</p>
  </div>");
        }

        [Test]
        public void SimplePageHeaderProducesCorrectHtml()
        {
            TestHelper.AssertHtml<FluentBootstrap.Tests.Web.Views.Tests.Misc>("test-simple-pageheader",
@"<div class=""page-header"">
   <h1>Header
    <small>Small Text</small>
   </h1>
  </div>");
        }

        [Test]
        public void ComplexPageHeaderProducesCorrectHtml()
        {
            TestHelper.AssertHtml<FluentBootstrap.Tests.Web.Views.Tests.Misc>("test-complex-pageheader",
@"<div class=""page-header"">
   <h1>            Header

    <small>Some small text</small>
    <span class=""label-default label"">A label</span>
   </h1>
  </div>");
        }
    }
}
